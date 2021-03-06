﻿using System;

namespace Clamp.UIShell.Framework.Shortcut
{
    internal class HotkeyPressedEventArgs : EventArgs
    {
        internal Hotkey Hotkey { get; private set; }

        internal HotkeyPressedEventArgs(Hotkey hotkey)
        {
            Hotkey = hotkey;
        }
    }
}