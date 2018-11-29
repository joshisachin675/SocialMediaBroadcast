using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Models
{
    public class ReportsGraph
    {
        public string Date { get; set; }
        public string TrackerName { get; set; }
        public int Count { get; set; }
        public string ToolTip { get; set; }
        public string sourcename { get; set; }
    }




    public class ChartInfo
    {
        public string Date { get; set; }
        public int Count { get; set; }
        public string ToolTip { get; set; }
    }


    public class ChartTest
    {
        public string SourceName { get; set; }
        public List<Data> DataList { get; set; }
    }
    public class Data
    {
        public int Count { get; set; }
        public string Date { get; set; }
        public string ToolTip { get; set; }
        public bool isShow { get; set; }
    }

    public class DahboardChart
    {
        public string CompanyName { get; set; }
        public List<Data> DataList { get; set; }
    }
}
