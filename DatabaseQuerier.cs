using EcommerceSqlSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSqlSolutions
{
    public static class DatabaseQuerier
    {
        public static void a(EcommerceContext context)
        {
            DateTime dateTime = new DateTime(2022, 2, 3);

            var query = from g in context.Goods
                        where !(from gp in context.GoodProperties
                                where dateTime >= gp.Bdate && dateTime <= gp.Edate
                                select gp.GoodId).Contains(g.Id)
                        select g;

            foreach(var g in query)
            {
                Console.WriteLine($"{g.Name}");
            }
        }

        public static void b(EcommerceContext context)
        {
            var baseData = from g in context.Goods
                           join gp in context.GoodProperties on g.Id equals gp.GoodId
                           select new
                           {
                               GoodId = g.Id,
                               GoodName = g.Name,
                               PropertyId = gp.Id,
                               Bdate = gp.Bdate,
                               Edate = gp.Edate
                           };

            var intersecting = from t1 in baseData
                               join t2 in baseData on t1.GoodId equals t2.GoodId
                               where t1.PropertyId < t2.PropertyId
                                   && (    (t2.Bdate <= t1.Edate && t1.Edate <= t2.Edate)
                                        || (t2.Bdate <= t1.Bdate && t1.Bdate <= t2.Edate)  )
                               select t1.GoodName;

            var distinctNames = intersecting.Distinct();

            foreach (var name in distinctNames)
            {
                Console.WriteLine($"{name}");
            }
        }
    }
}
