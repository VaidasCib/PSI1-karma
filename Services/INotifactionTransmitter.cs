﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using MatBlazor;
using static Karma.Services.NotificationTransmitter;

namespace Karma.Services
{
    public interface INotifactionTransmitter
    {
        void ShowMessage(string message, MatToastType notificationType);
        //event NotificationEventHandler NotificationEvent; //Alternative events
        event EventHandler<NotificationEventArgs> NotificationEvent;
    }
}
