﻿using Clamp.Common.Pipelines;
using Clamp.SDK.Framework.Advices;
using Clamp.SDK.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clamp.SDK.Framework.Services
{
    public interface IWinFormService : IService
    {
        void Setup(NamedPipeServer<string> namedPipeServer);

        bool IsConnected(string name);

        void DisplayNotice(Notice notice);

        void ActiviteSDShell();

        void Upgrade(string versionCode);

        List<LocalPrinterInfo> GetLocalPrinterInfos();
    }
}
