﻿using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class CustomBSPopoverBase : ToggleableComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        protected string Classname =>
        new CssBuilder("popover")
            .AddClass($"bs-popover-{Placement.ToDescriptionString()}")
            .AddClass("show", (IsOpen ?? false))
            .AddClass(Class)
        .Build();

        protected ElementReference Arrow { get; set; }

        protected override void OnAfterRender(bool firstrun)
        {
            if (IsOpen ?? false)
            {
                var placement = Placement.ToDescriptionString();
                new BlazorStrapInterop(JSRuntime).Popper(Target, Id, Arrow, placement);
            }
        }
        protected string Id => Target + "-popover";

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Target { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool DismissOnNextClick { get; set; } = true;


        protected void OnClick()
        {
            if (DismissOnNextClick)
            {
                Hide();
            }
        }
    }
}
