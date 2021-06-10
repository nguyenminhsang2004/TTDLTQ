using ChartWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChartWeb.api
{
    [RoutePrefix("api/staffs")]
    public class StaffController : ApiController
    {
        BikeStoresDWHDataContext context = new BikeStoresDWHDataContext();
        [Route("revenue")]
        public List<Revenue> getRevenue(int year, int month)
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
            var categories = context.Dim_Staffs.Select(x => new { x.staff_id, x.first_name,x.last_name }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.staff_id, x.Revenue, x.Dim_Date.year, x.Dim_Date.month }).ToList();
            switch (temp)
            {
                case 0:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id && t.year == year && t.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
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
                                if (t.staff_id == x.staff_id && t.year == year)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
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
                                if (t.staff_id == x.staff_id)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
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
                                if (t.staff_id == x.staff_id && t.month == month)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
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
            int sales = 0;
            List<Revenue> res = new List<Revenue>();
            var categories = context.Dim_Staffs.Select(x => new { x.staff_id, x.first_name, x.last_name }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.staff_id, x.Sales, x.Dim_Date.year, x.Dim_Date.month }).ToList();
            switch (temp)
            {
                case 0:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id && t.year == year && t.month == month)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = sales });
                            sales = 0;
                        });
                    }
                    break;

                case 1:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id && t.year == year)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = sales });
                            sales = 0;
                        });
                    }
                    break;

                case 2:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = sales });
                            sales = 0;
                        });
                    }
                    break;
                case 3:
                    {
                        categories.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id && t.month == month)
                                {
                                    sales += (int)t.Sales;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = sales });
                            sales = 0;
                        });
                    }
                    break;
            }
            return res;
        }
        [Route("topstaff")]
        public List<Revenue> getRevenueStaff(int top, int quarter, int year)
        {
            int temp = 0;
            if (year == 0 && quarter == 0)
                temp = 2;
            if (year != 0 && quarter == 0)
                temp = 1;
            if (year == 0 && quarter != 0)
                temp = 3;
            if (top == 0) top = 10;
            double revenue = 0;
            List<Revenue> res = new List<Revenue>();
            var staff = context.Dim_Staffs.Select(x => new { x.staff_id, x.first_name, x.last_name }).ToList();
            var model = context.Fact_Revenue_Sales.Select(x => new { x.staff_id, x.Revenue, x.Dim_Date.year, x.Dim_Date.quarter }).ToList();
            switch (temp)
            {
                case 0:
                    {
                        staff.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id && t.year == year && t.quarter == quarter)
                                {
                                    revenue += (double)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 1:
                    {
                        staff.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id && t.year == year)
                                {
                                    revenue += (int)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;

                case 2:
                    {
                        staff.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id)
                                {
                                    revenue += (int)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
                case 3:
                    {
                        staff.ForEach(x =>
                        {
                            model.ForEach(t =>
                            {
                                if (t.staff_id == x.staff_id && t.quarter == quarter)
                                {
                                    revenue += (int)t.Revenue;
                                }
                            });
                            res.Add(new Revenue { label = (x.first_name + " " + x.last_name).ToString(), value = revenue });
                            revenue = 0;
                        });
                    }
                    break;
            }
            return res.OrderByDescending(x => x.value).Take(top).ToList();
        }

    }
}
