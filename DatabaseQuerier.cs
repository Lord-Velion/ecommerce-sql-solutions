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
    }
}
