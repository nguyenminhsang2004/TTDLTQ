using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartWeb.Models
{
    public class StoreModel
    {
        public string seriesname { get; set; }
        public List<ViewValue> data { get; set; }
    }
}