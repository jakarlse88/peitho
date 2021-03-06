﻿using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Peitho.Pages.Admin.Shared
{
    /// <summary>
    /// https://chrissainty.com/building-custom-input-components-for-blazor-using-inputbase/
    /// </summary>
    public class CustomInputTextBase : InputBase<string>
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Placeholder { get; set; }
        [Parameter] public bool Required { get; set; }
        [Parameter] public Expression<Func<string>> ValidationFor { get; set; }
        
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}