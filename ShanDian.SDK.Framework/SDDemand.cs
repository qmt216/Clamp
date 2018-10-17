﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShanDian.SDK.Framework
{
    /// <summary>
    /// 软件需要的凭证对象
    /// </summary>
    public class SDDemand
    {
        public const string DemandFileName = "Demand.json";

        public string PCID { set; get; }
        public string AppId { set; get; }

        public string SecureKey { set; get; }

        public string MikeRestId { set; get; }

        public string RunMode { set; get; }

        public string Server { set; get; }
    }
}
