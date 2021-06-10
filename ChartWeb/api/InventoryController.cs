using ChartWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/inventory")]
    public class InventoryController : ApiController
    {
        BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();
        [Route("categories")]
        public List<ViewLabel> getCategoryLabel()
        {
            List<ViewLabel> res = new List<ViewLabel>();
            var model = context.Dim_Categories.Select(x => new {x.category_name }).ToList();
            model.ForEach(x =>
            {
                res.Add(new ViewLabel { label = x.category_name.ToString() });
            });
            return res;
        }
        [Route("salesbycategories")]
        public List<ViewValue> getSalesCategories()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var categories = context.Dim_Categories.Select(x => new { x.category_id }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.category_id, x.Sales }).ToList();
            categories.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.category_id == x.category_id)
                        sales += (int)t.Sales;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }
        [Route("inventorybycategories")]
        public List<ViewValue> getInventoryCategories()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var categories = context.Dim_Categories.Select(x => new { x.category_id }).ToList();
            var model = context.Fact_Inventories.Select(x => new { x.quantity, x.Dim_Product.category_id }).ToList();
            categories.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.category_id == x.category_id)
                        sales += (int)t.quantity;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }

        [Route("products")]
        public List<ViewLabel> getProductLabel()
        {
            List<ViewLabel> res = new List<ViewLabel>();
            var model = context.Dim_Products.Select(x => new { x.product_name }).ToList();
            model.ForEach(x =>
            {
                res.Add(new ViewLabel { label = x.product_name.ToString() });
            });
            return res;
        }

        [Route("salesbyproducts")]
        public List<ViewValue> getSalesProduct()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var products = context.Dim_Products.Select(x => new { x.product_id }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.product_id, x.Sales }).ToList();
            products.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.product_id == x.product_id)
                        sales += (int)t.Sales;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }

        [Route("inventorybyproducts")]
        public List<ViewValue> getInventoryProducts()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var categories = context.Dim_Products.Select(x => new { x.product_id }).ToList();
            var model = context.Fact_Inventories.Select(x => new { x.quantity, x.product_id }).ToList();
            categories.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.product_id == x.product_id)
                        sales += (int)t.quantity;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }

        [Route("brands")]
        public List<ViewLabel> getProductBrand()
        {
            List<ViewLabel> res = new List<ViewLabel>();
            var model = context.Dim_Brands.Select(x => new { x.brand_name }).ToList();
            model.ForEach(x =>
            {
                res.Add(new ViewLabel { label = x.brand_name.ToString() });
            });
            return res;
        }

        [Route("salesbybrands")]
        public List<ViewValue> getSalesBrands()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var products = context.Dim_Brands.Select(x => new { x.brand_id }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.brand_id, x.Sales }).ToList();
            products.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.brand_id == x.brand_id)
                        sales += (int)t.Sales;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }

        [Route("inventorybybrands")]
        public List<ViewValue> getInventoryBrands()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var categories = context.Dim_Brands.Select(x => new { x.brand_id }).ToList();
            var model = context.Fact_Inventories.Select(x => new { x.quantity, x.Dim_Product.brand_id }).ToList();
            categories.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.brand_id == x.brand_id)
                        sales += (int)t.quantity;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }

        [Route("stores")]
        public List<ViewLabel> getProductStores()
        {
            List<ViewLabel> res = new List<ViewLabel>();
            var model = context.Dim_Stores.Select(x => new { x.store_name }).ToList();
            model.ForEach(x =>
            {
                res.Add(new ViewLabel { label = x.store_name.ToString() });
            });
            return res;
        }

        [Route("salesbystores")]
        public List<ViewValue> getSalesStores()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var products = context.Dim_Stores.Select(x => new { x.store_id }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.store_id, x.Sales }).ToList();
            products.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.store_id == x.store_id)
                        sales += (int)t.Sales;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }
        [Route("inventorybystores")]
        public List<ViewValue> getInventoryStores()
        {
            List<ViewValue> res = new List<ViewValue>();
            int sales = 0;
            var categories = context.Dim_Stores.Select(x => new { x.store_id }).ToList();
            var model = context.Fact_Inventories.Select(x => new { x.quantity, x.Dim_Store.store_id }).ToList();
            categories.ForEach(x =>
            {
                model.ForEach(t =>
                {
                    if (t.store_id == x.store_id)
                        sales += (int)t.quantity;
                });
                res.Add(new ViewValue { value = sales });
                sales = 0;
            });

            return res;
        }
    }
}
