using ChartWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();

        [Route("label")]
        public List<ViewLabel> getAllProductLabel(int brand)
        {
            List<ViewLabel> res = new List<ViewLabel>();

            if (brand == 0)
            {
                var name = context.Dim_Products.Select(x => new { x.product_name }).ToList();
                name.ForEach(x =>
                {
                    res.Add(new ViewLabel { label = x.product_name.ToString() });
                });
            }
            else
            {
                var name = context.Dim_Products.Select(x => new { x.product_name, x.brand_id }).Where(x => x.brand_id == brand).ToList();
                name.ForEach(x =>
                {
                    res.Add(new ViewLabel { label = x.product_name.ToString() });
                });
            }

            return res;
        }

        [Route("revenue")]
        public List<ViewValue> getAllProductRevenue(int brand, int year, int month)
        {
            int temp = 0;
            if (brand == 0)
            {
                if (year == 0 && month == 0)
                    temp = 1;
                if (year != 0 && month == 0)
                    temp = 2;
                if (year == 0 && month != 0)
                    temp = 3;
                if (year != 0 && month != 0)
                    temp = 4;
            }
            else
            {
                if (year == 0 && month == 0)
                    temp = 5;
                if (year != 0 && month == 0)
                    temp = 6;
                if (year == 0 && month != 0)
                    temp = 7;
            }
            double revenue = 0;
            List<ViewValue> res = new List<ViewValue>();
            var model = context.Fact_Revenue_Sales
                .Select(x => new { x.product_id, x.brand_id, x.Dim_Date.year, x.Dim_Date.month, x.Revenue }).ToList();

            switch (temp)
            {
                case 0:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id, x.brand_id }).Where(x => x.brand_id == brand).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id && t.year == year && t.month == month && t.brand_id == brand)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 1:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id }).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 2:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id }).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id && t.year == year)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 3:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id }).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id && t.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 4:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id }).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id && t.year == year && t.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 5:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id, x.brand_id }).Where(x => x.brand_id == brand).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id && t.brand_id == brand)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 6:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id, x.brand_id }).Where(x => x.brand_id == brand).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id && t.year == year && t.brand_id == brand)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 7:
                    {
                        var products = context.Dim_Products.Select(x => new { x.product_id, x.brand_id }).Where(x => x.brand_id == brand).ToList();
                        products.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.product_id == x.product_id && t.month == month && t.brand_id == brand)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new ViewValue { value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
            }

            return res;
        }
    }
}