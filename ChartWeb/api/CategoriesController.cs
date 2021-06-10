using ChartWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();
        [Route("getall")]
        public List<ViewModel> getAllCategories()
        {
            List<ViewModel> res = new List<ViewModel>();
            res.Add(new ViewModel { id = 0, name = "None" });
            var model = context.Dim_Categories.ToList();
            model.ForEach(x =>
            {
                res.Add(new ViewModel { id = x.category_id, name = x.category_name });
            });

            return res;
        }
        [Route("revenue")]
        public List<Revenue> getRevenueCategories(int year, int month)
        {
            int temp = 0;
            if (year == 0 && month == 0)
                temp = 2;
            if (year != 0 && month == 0)
                temp = 1;
            if (year == 0 && month != 0)
                temp = 3;
            double revenue = 0;
            List<Revenue> res = new List<Revenue>();
            var categories = context.Dim_Categories.Select(x => new { x.category_id, x.category_name }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.category_id, x.Revenue, x.Dim_Date.year, x.Dim_Date.month }).ToList();
            switch (temp)
            {
                case 0:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.category_id == x.category_id && t.year == year && t.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = x.category_name.ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 1:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.category_id == x.category_id && t.year == year)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = x.category_name.ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 2:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.category_id == x.category_id)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = x.category_name.ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
                case 3:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.category_id == x.category_id && t.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = x.category_name.ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
            }
            return res;
        }
    }
}