﻿namespace Clamp.Webwork.Conventions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Collection class for static culture conventions
    /// </summary>
    public class CultureConventions : IEnumerable<Func<WebworkContext, CultureInfo>>
    {
        private readonly IEnumerable<Func<WebworkContext, CultureInfo>> conventions;

        /// <summary>
        /// Creates a new instance of CultureConventions
        /// </summary>
        /// <param name="conventions"></param>
        public CultureConventions(IEnumerable<Func<WebworkContext, CultureInfo>> conventions)
        {
            this.conventions = conventions;
        }

        public IEnumerator<Func<WebworkContext, CultureInfo>> GetEnumerator()
        {
            return conventions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
