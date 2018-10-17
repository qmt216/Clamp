﻿using CefSharp;
using ShanDian.UIShell.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShanDian.UIShell.Brower
{
    public class RenderProcessMessageHandler : IRenderProcessMessageHandler
    {
        void IRenderProcessMessageHandler.OnFocusedNodeChanged(IWebBrowser browserControl, IBrowser browser, IFrame frame, IDomNode node)
        {

        }

        void IRenderProcessMessageHandler.OnContextCreated(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            DebugHelper.WriteLine("OnContextCreated");
        }
    }
}
