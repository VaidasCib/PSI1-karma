using System;
using System.Linq;
using System.Security.Claims;
using Karma.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Karma.Pages
{
    public partial class VolunteerRegistration
    {
        public CharityEvent charityEvent;

        [Inject]
        private IDbContextFactory<KarmaContext> m_contextFactory { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        private string Name { get; set; } = "";

        private string Surname { get; set; } = "";

        private string PhoneNumber { get; set; } = "";

        private KarmaContext m_karmaContext;

        public string CurrentUserId { get; set; }

        protected override void OnInitialized()
        {
            m_karmaContext = m_contextFactory.CreateDbContext();
            charityEvent = m_karmaContext.Events.Where(p => p.Id == Id).FirstOrDefault();
            ClaimsPrincipal principal = m_httpContextAccessor.HttpContext.User;
            CurrentUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private void CreatePendingVolunteer()
        {
            var volunteer = new PendingVolunteer(Name, Surname, PhoneNumber, Guid.NewGuid());
            m_karmaContext.PendingVolunteers.Add(volunteer);
            m_karmaContext.SaveChanges();
            m_notificationTransmitter.ShowMessage("Registration Succesful", MatToastType.Success);
            Name = "";
            Surname = "";
            PhoneNumber = "";
            m_uriHelper.NavigateTo("/");
        }

        private void RegisterToEvent()
        {
            var volunteer = new Volunteer(Name, Surname, Guid.NewGuid());
            m_karmaContext.Volunteers.Add(volunteer);
            m_karmaContext.SaveChanges();
            var charityEventVolunteer = new CharityEventVolunteers();
            charityEventVolunteer.Id = Guid.NewGuid();
            charityEventVolunteer.Volunteer = volunteer;
            charityEventVolunteer.CharityEvent = charityEvent;
            m_karmaContext.CharityEventVolunteers.Add(charityEventVolunteer);
            charityEvent.Volunteers.Add(m_karmaContext.Volunteers.Where(p => p.Id == volunteer.Id).FirstOrDefault());
            m_karmaContext.SaveChanges();
            Name = "";
            Surname = "";
            PhoneNumber = "";
            m_uriHelper.NavigateTo("/");
        }
    }
}
