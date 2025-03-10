﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Karma.Models;
using Karma.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace Karma.Pages
{
    public partial class ManageIndividualVolunteer
    {
        [Parameter]
        public Guid Id { get; set; }
        [Inject]
        public IJSRuntime m_jsRuntime { get; set; }
        [Inject]
        public IDBServiceProvider m_DBServiceProvider { get; set; }

        public Volunteer volunteer;
        private KarmaContext m_karmaContext;
        private string FilterValue { get; set; } = "";
        public string CurrentUserId { get; set; }
        public string EquipmentName { get; set; }
        public bool panelOpenState1 = true;
        public bool panelOpenState2 = true;
        public bool panelOpenState3 = true;

        private void UpdateVolunteerData()
        {
            int result = m_DBServiceProvider.UpdateEntityInDB(volunteer);
            if (result == 0)
                m_uriHelper.NavigateTo("/volunteers");
            else if (result == -1)
                m_notificationTransmitter.ShowMessage("An error occured while trying to update Volunteer", MatToastType.Danger);
        }

        protected override void OnInitialized()
        {
            m_karmaContext = m_DBServiceProvider.karmaContext;
            volunteer = m_karmaContext.Volunteers.Include(p => p.Events).Where(p => p.Id == Id).FirstOrDefault();
            ClaimsPrincipal principal = m_httpContextAccessor.HttpContext.User;
            CurrentUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IEnumerable<ICharityEvent> GetEventsOfThisVolunteer()
        {
            return m_karmaContext.Volunteers.Include(p => p.Events).Where(p => p.Id == Id).Select(p => p.Events).SelectMany(p => p);
        }

        public IEnumerable<ICharityEvent> GetEventsNotOfThisVolunteer()
        {
            return m_karmaContext.Events.Include(p => p.Volunteers).Where(p => !p.Volunteers.Contains(volunteer) && p.ManagerId == CurrentUserId && p.MaxVolunteers > p.Volunteers.Count);
        }

        public IEnumerable<ICharityEvent> GetHighPriorityEvents()
        {
            IEnumerable<ICharityEvent> allEvents = GetEventsNotOfThisVolunteer();
            return allEvents.Aggregate(new List<ICharityEvent>(), (list, currentEvent) =>
            {
                if ((currentEvent.MaxVolunteers / 2) > currentEvent.Volunteers.Count)
                {
                    list.Add(currentEvent);
                }
                return list;
            });
        }

        public IEnumerable<ICharityEvent> GetRestOfEvents()
        {
            return GetEventsNotOfThisVolunteer().Except(GetHighPriorityEvents());
        }

        private void AddEventToVolunteerList(Guid id)
        {
            volunteer.Events.Add(m_karmaContext.Events.Where(p => p.Id == id).FirstOrDefault());
            m_karmaContext.SaveChanges();
        }

        private void RemoveEventFromList(Guid id)
        {
            volunteer.Events.Remove(m_karmaContext.Events.Where(p => p.Id == id).FirstOrDefault());
            m_karmaContext.SaveChanges();
        }

        public IEnumerable<ISpecialEquipment> GetEquipmentOfThisVolunteer()
        {
            return m_karmaContext.SpecialEquipment.Include(p => p.Owner).Where(p => p.Owner.Id == Id);
        }

        private void RemoveEquipment(Guid id)
        {
            m_karmaContext.Remove(m_karmaContext.SpecialEquipment.Where(p => p.Id == id).FirstOrDefault());
            m_karmaContext.SaveChanges();
        }

        private void AddNewEquipmentToVolunteer()
        {
            var equipment = new Models.SpecialEquipment(Guid.NewGuid(), EquipmentName, volunteer);
            m_karmaContext.SpecialEquipment.Add(equipment);
            EquipmentName = string.Empty;
            m_karmaContext.SaveChanges();
        }
    }
}
