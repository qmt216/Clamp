﻿using ShanDian.AddIns;
using ShanDian.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShanDian.SDK.Cmds
{
    public class INITIALIZE : CommandBase
    {
        protected override object DoHandle(string @params)
        {
            AddInManager.Initialize();

            return null;
        }
    }
}