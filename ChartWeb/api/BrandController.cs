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
        [Route("getlabel")]
        public List<ViewLabel> getAllLabelBrand()
        {
            List<ViewLabel> res = new List<ViewLabel>();
            var model = context.Dim_Brands.ToList();
            model.ForEach(x =>
            {
                res.Add(new ViewLabel { label = x.brand_name });
            });
            return res;
        }
        [Route("revenue")]
        public List<Revenue> getRevenue(int year,int month)
        {
            int temp = 0;
            if (year == 0 && month == 0)
                temp = 2;
            if (year != 0 && month == 0)
                temp = 1;
            if (year == 0 && month != 0)
                temp = 3;
            List<Revenue> res = new List<Revenue>();
            var model = context.Fact_Revenue_Sales.ToList();
            double revenue = 0;
            switch (temp)
            {
                case 0:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id && t.Dim_Date.year == year && t.Dim_Date.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue {label = x.brand_name, value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
                case 1:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id && t.Dim_Date.year == year)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = x.brand_name, value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
                case 2:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = x.brand_name, value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
                case 3:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id && t.Dim_Date.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = x.brand_name, value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
            }

            return res;
        }
        [Route("sales")]
        public List<Revenue> getSales(int year, int month)
        {
            int temp = 0;
            if (year == 0 && month == 0)
                temp = 2;
            if (year != 0 && month == 0)
                temp = 1;
            if (year == 0 && month != 0)
                temp = 3;
            List<Revenue> res = new List<Revenue>();
            var model = context.Fact_Revenue_Sales.ToList();
            int sales = 0;
            switch (temp)
            {
                case 0:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id && t.Dim_Date.year == year && t.Dim_Date.month == month)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = x.brand_name, value = sales });
                            sales = 0;
                        });
                    }
                    break;
                case 1:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id && t.Dim_Date.year == year)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = x.brand_name, value = sales });
                            sales = 0;
                        });
                    }
                    break;
                case 2:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = x.brand_name, value = sales });
                            sales = 0;
                        });
                    }
                    break;
                case 3:
                    {
                        var modelbrand = context.Dim_Brands.ToList();
                        modelbrand.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.brand_id == x.brand_id && t.Dim_Date.month == month)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = x.brand_name, value = sales });
                            sales = 0;
                        });
                    }
                    break;
            }

            return res;
        }
    }
}