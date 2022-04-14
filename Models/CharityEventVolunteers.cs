// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karma.Models
{
    public class CharityEventVolunteers
    {
        public Guid Id { get; set; }

        public CharityEvent CharityEvent { get; set; }

        public Volunteer Volunteer { get; set; }

        public bool Participated { get; set; }

        public string AdditionalInfo { get; set; }

    }
}
