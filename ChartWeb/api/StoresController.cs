using ChartWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/stores")]
    public class StoresController : ApiController
    {
        BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();
        [Route("revenue")]
        public List<StoreModel> getRevenue(int month)
        {
            List<StoreModel> res = new List<StoreModel>();
            List<int> year = new List<int>();
            var date = context.Dim_Dates.Select(x => x.year).ToList();
            date.ForEach(x =>
            {
                if (year.Count == 0) year.Add((int)x);
                if (!year.Exists(t => t == x)) year.Add((int)x);
            });
            var stores = context.Dim_Stores.ToList();
            var model = context.Fact_Revenue_Sales.ToList();

            stores.ForEach(s =>
            {
                List<ViewValue> temp = new List<ViewValue>();
                year.ForEach(y =>
                {
                    double revenue = 0;
                    model.ForEach(x =>
                    {
                        if (s.store_id == x.store_id && x.Dim_Date.year == y)
                            revenue += (double)x.Revenue;
                    });
                    temp.Add(new ViewValue { value = revenue });
                });
                res.Add(new StoreModel { seriesname = s.store_name, data = temp });
            });
            return res;
        }

        [Route("sales")]
        public List<StoreModel> getSales(int month)
        {
            List<StoreModel> res = new List<StoreModel>();
            List<int> year = new List<int>();
            var date = context.Dim_Dates.Select(x => x.year).ToList();
            date.ForEach(x =>
            {
                if (year.Count == 0) year.Add((int)x);
                if (!year.Exists(t => t == x)) year.Add((int)x);
            });
            var stores = context.Dim_Stores.ToList();
            var model = context.Fact_Revenue_Sales.ToList();
            if(month == 0)
            {
                stores.ForEach(s =>
                {
                    List<ViewValue> temp = new List<ViewValue>();
                    year.ForEach(y =>
                    {
                        int sales = 0;
                        model.ForEach(x =>
                        {
                            if (s.store_id == x.store_id && x.Dim_Date.year == y)
                                sales += (int)x.Sales;
                        });
                        temp.Add(new ViewValue { value = sales });
                    });
                    res.Add(new StoreModel { seriesname = s.store_name, data = temp });
                });
            }
            else
            {
                stores.ForEach(s =>
                {
                    List<ViewValue> temp = new List<ViewValue>();
                    year.ForEach(y =>
                    {
                        int sales = 0;
                        model.ForEach(x =>
                        {
                            if (s.store_id == x.store_id && x.Dim_Date.year == y && x.Dim_Date.month == month)
                                sales += (int)x.Sales;
                        });
                        temp.Add(new ViewValue { value = sales });
                    });
                    res.Add(new StoreModel { seriesname = s.store_name, data = temp });
                });
            }
            return res;
        }
    }
}
