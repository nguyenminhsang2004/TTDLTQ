using ChartWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/dates")]
    public class DateController : ApiController
    {
        private BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();

        [Route("getall")]
        public List<string> getAllYear()
        {

            List<string> res = new List<string>();
            res.Add("None");
            var date = context.Dim_Dates.Select(x => x.year.ToString()).ToList();
            date.ForEach(x =>
            {
                if (res.Count == 0) res.Add(x);
                if (!res.Exists(t => t == x)) res.Add(x);
            });
            return res;
        }
        [Route("year")]
        public List<ViewLabel> getAll()
        {
            List<ViewLabel> res = new List<ViewLabel>();
            List<string> temp = new List<string>();
            var date = context.Dim_Dates.Select(x => x.year.ToString()).ToList();
            date.ForEach(x =>
            {
                if (temp.Count == 0) temp.Add(x);
                if (!temp.Exists(t => t == x)) temp.Add(x);
            });

            temp.ForEach(x => { res.Add(new ViewLabel { label = x }); });
            return res;
        }
    }
}