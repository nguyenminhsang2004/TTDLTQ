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
    }
}