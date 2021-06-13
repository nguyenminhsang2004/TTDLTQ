using System.Collections.Generic;

namespace ChartWeb.Models
{
    public class StoreModel
    {
        public string seriesname { get; set; }
        public List<ViewValue> data { get; set; }
    }
}