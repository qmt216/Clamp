﻿using System.Collections.Generic;
using ShanDian.Machine.Dto;

namespace ShanDian.Machine.Model
{
    public class CUseArray: VMInput
    {
        /// <summary>
        /// 用户Id列
        /// </summary>
        public List<int> Users { get; set; }
    }
}