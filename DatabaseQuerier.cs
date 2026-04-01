using EcommerceSqlSolutions.Models;
using Microsoft.EntityFrameworkCore;
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

        public static void c(EcommerceContext context)
        {
            var query = from o in context.Orders
                        join a in context.Agents on o.AgentId equals a.Id
                        where o.CreateDate.Year == 2022
                        group o by a.Name into g
                        where g.Count() > 10
                        select new { AgentName = g.Key };

            foreach (var q in query)
            {
                Console.WriteLine($"{q.AgentName}");
            }
        }

        public class AgentGoodsAmount
        {
            public string AgentName { get; set; }
            public int GoodsAmount { get; set; }
        }

        public static void d(EcommerceContext context)
        {
            var query = context.Database.SqlQueryRaw<AgentGoodsAmount>(@"
            WITH LastOrders AS (
                SELECT 
                    Id, 
                    AgentId, 
                    CreateDate
                FROM (
                    SELECT 
                        Id, 
                        AgentId, 
                        CreateDate,
                        ROW_NUMBER() OVER (PARTITION BY AgentId ORDER BY CreateDate DESC) AS rn
                    FROM Orders
                ) ranked
                WHERE rn = 1
            )
            SELECT
                Agents.Name AS AgentName,
                SUM(OrderDetails.GoodCount) AS GoodsAmount
            FROM LastOrders
                JOIN Agents ON LastOrders.AgentId = Agents.Id
                LEFT JOIN OrderDetails ON LastOrders.Id = OrderDetails.OrderId
            WHERE NOT EXISTS (
                SELECT 1
                FROM OrderDetails
                JOIN Goods ON OrderDetails.GoodId = Goods.Id
                JOIN GoodProperties ON OrderDetails.GoodId = GoodProperties.GoodId
                JOIN Colors ON GoodProperties.ColorId = Colors.Id
                WHERE OrderDetails.OrderId = LastOrders.ID
                    AND Goods.Name = 'Good1'
                    AND Colors.Name = 'Color2'
            )
            GROUP BY Agents.Name");

            var result = query.ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.AgentName}, {item.GoodsAmount}");
            }
        }
    }
}
