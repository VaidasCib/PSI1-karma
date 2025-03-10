﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;

namespace Karma.Services
{
    public class ObjectChecker : IObjectChecker
    {
        public bool IsAnyNullOrEmpty(object myObject)
        {
            return myObject.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => (string) pi.GetValue(myObject))
                .Any(value => string.IsNullOrEmpty(value));
        }
    }
}
