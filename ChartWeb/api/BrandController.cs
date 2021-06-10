using ChartWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/brands")]
    public class BrandController : ApiController
    {
        private BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();
        [Route("label")]
        public List<ViewModel> getAllBrand()
        {
            List<ViewModel> res = new List<ViewModel>();
            res.Add(new ViewModel { id = 0, name = "None" });
            var model = context.Dim_Brands.ToList();
            model.ForEach(x =>
            {
                res.Add(new ViewModel { id = x.brand_id, name = x.brand_name });
            });
            return res;
        }
    }
}