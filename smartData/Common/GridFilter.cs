using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Common
{
    /// <summary>
    /// Grid filters 
    /// </summary>
    public class GridFilter
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public string order { get; set; }
        public string sort { get; set; }
        public string search { get; set; }
    }
}