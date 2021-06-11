using ChartWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();
        [Route("revenue")]
        public string getRevenue()
        {
            double revenue = 0;
            var model = context.Fact_Revenue_Sales.Select(x => new { x.Revenue }).ToList();
            model.ForEach(x =>
            {
                revenue += (double)x.Revenue;
            });
            return revenue.ToString("##,#");
        }
        [Route("sales")]
        public string getSales()
        {
            int sales = 0;
            var model = context.Fact_Revenue_Sales.Select(x => new { x.Sales }).ToList();
            model.ForEach(x =>
            {
                sales += (int)x.Sales;
            });
            return sales.ToString("##,#");
        }
        [Route("brands")]
        public int getBrands()
        {
            return context.Dim_Brands.Count();
        }
        [Route("categories")]
        public int getCategories()
        {
            return context.Dim_Categories.Count();
        }
        [Route("products")]
        public int getProducts()
        {
            return context.Dim_Products.Count();
        }
        [Route("revenuebyyear")]
        public List<Revenue> getAllByYear()
        {
            List<int> dates = new List<int>();
            var datemodel = context.Dim_Dates.Select(x => x.year).ToList();
            datemodel.ForEach(x =>
            {
                if (dates.Count == 0) dates.Add((int)x);
                if (!dates.Exists(t => t == x)) dates.Add((int)x);
            });
            List<Revenue> res = new List<Revenue>();
            double revenue = 0;
            var model = context.Fact_Revenue_Sales.Select(x => new { x.Revenue, x.Dim_Date.year }).ToList();
            var date = context.Dim_Dates.Select(x => x.year).ToList();
            dates.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.year == x)
                        revenue += (double)t.Revenue;
                });
                res.Add(new Revenue { label = x.ToString(), value = revenue });
                revenue = 0;
            });
            return res;

        }
        [Route("revenuetop")]
        public List<Revenue> getProduct()
        {
            double revenue = 0;
            List<Revenue> res = new List<Revenue>();
            var products = context.Dim_Products.Select(x => new { x.product_id, x.product_name }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.product_id, x.Revenue }).ToList();
            products.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.product_id == x.product_id)
                        revenue += (double)t.Revenue;
                });
                res.Add(new Revenue { label = x.product_name, value = revenue });
                revenue = 0;
            });

            return res.OrderByDescending(x => x.value).Take(10).ToList();
        }
        [Route("salescategories")]
        public List<Revenue> getSalesCategories()
        {
            List<Revenue> res = new List<Revenue>();
            int sales = 0;
            var categories = context.Dim_Categories.ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.category_id, x.Sales }).ToList();
            categories.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (x.category_id == t.category_id)
                        sales += (int)t.Sales;
                });
                res.Add(new Revenue { label = x.category_name, value = sales });
                sales = 0;
            });

            return res;
        }
        [Route("inventory")]
        public List<Revenue> getInventory()
        {
            List<int> year = new List<int>();
            var date = context.Dim_Dates.Select(x => x.year).ToList();
            date.ForEach(x =>
            {
                if (year.Count == 0) year.Add((int)x);
                if (!year.Exists(t => t == x)) year.Add((int)x);
            });
            List<Revenue> res = new List<Revenue>();
            int inventory = 0;
            var model = context.Fact_Inventories.Select(x => new { x.Dim_Product.model_year, x.quantity }).ToList();
            year.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.model_year == x)
                        inventory += (int)t.quantity;
                });
                res.Add(new Revenue { label = x.ToString(), value = inventory });
                inventory = 0;
            });

            return res;
        }

        [Route("worldcharts")]
        public List<ViewWorldChart> getWorldChart()
        {
            List<ViewWorldChart> res = new List<ViewWorldChart>();
            var model = context.Fact_Revenue_Sales.ToList();
            var store = context.Dim_Stores.ToList();
            double revenue = 0;
            double temp = 0;
            model.ForEach(x =>
            {
                revenue += (double)x.Revenue;
            });
            store.ForEach(s =>
            {
                string id = "SA";
                double value = 0;
                model.ForEach(t =>
                {
                    if (t.Dim_Store.state == s.state)
                        value += (double)t.Revenue;
                });
                if (s.state == "NY") id = "NA";
                if (id == "SA") temp += value/revenue;
                res.Add(new ViewWorldChart { id = id, value = (value/revenue).ToString(), showLabel = "1" });
            });

            res.ForEach(x =>
            {
                if (x.id == "SA") x.value = temp.ToString();
            });
            return res;
        }

    }
}
