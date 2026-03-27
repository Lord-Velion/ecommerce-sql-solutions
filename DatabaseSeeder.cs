using EcommerceSqlSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSqlSolutions
{
    public static class DatabaseSeeder
    {
        public static void SeedAgents(EcommerceContext context)
        {
            if (!context.Agents.Any())
            {
                for (int i = 1; i <= 15; i++)
                {
                    context.Agents.Add(new Agent { Id = i, Name = $"Agent{i}" });
                }
                context.SaveChanges();
            }
        }

        public static void SeedGoods(EcommerceContext context)
        {
            if (!context.Goods.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    context.Goods.Add(new Good { Id = i, Name = $"Good{i}" });
                }
                context.SaveChanges();
            }
        }

        public static void SeedColors(EcommerceContext context)
        {
            if (!context.Colors.Any())
            {
                for (int i = 1; i <= 3; i++)
                {
                    context.Colors.Add(new Color { Id = i, Name = $"Color{i}" });
                }
                context.SaveChanges();
            }
        }

        private static DateTime GenerateDate(DateTime bDate, DateTime eDate)
        {
            Random rand = new Random();

            int year = rand.Next(bDate.Year, eDate.Year + 1);

            int month = 1;

            if (year == bDate.Year)
                month = rand.Next(bDate.Month, 13);
            else if (year == eDate.Year)
                month = rand.Next(1, eDate.Month + 1);
            else
                month = rand.Next(1, 13);

            int firstDay = 1;
            int lastDay = -1;

            if (year == bDate.Year && month == bDate.Month)
                firstDay = bDate.Day;
            else if (year == eDate.Year && month == eDate.Month)
                lastDay = eDate.Day;
                


            int day = 1;

            // високосный год содержит 366 дней
            // обычный год содержит 365 дней
            // 2020 - високосный год
            // 2021 - невисокосный год
            // 2022 - невисокосный год
            // 2023 - невисокосный год

            if (lastDay == -1)
            {
                switch (month)
                {
                    case 1 or 3 or 5 or 7 or 8 or 10 or 12:
                        day = rand.Next(firstDay, 32);
                        break;
                    case 4 or 6 or 9 or 11:
                        day = rand.Next(firstDay, 31);
                        break;
                    case 2:
                        day = (year == 2020) ? (rand.Next(firstDay, 30)) : (rand.Next(firstDay, 29));
                        break;
                }
            }
            else         
                day = rand.Next(firstDay, lastDay);

            return new DateTime(year, month, day);
        }

        public static void SeedOrders(EcommerceContext context)
        {
            if (!context.Orders.Any() && !context.OrderDetails.Any())
            {
                Random rand = new Random();
                for (int agentId = 1; agentId <= 15; agentId++)
                {
                    int ordersAmount = rand.Next(10, 41);
         
                    for (int orderIdx = 1; orderIdx <= ordersAmount; orderIdx++)
                    {
                        var order = new Order
                        {
                            AgentId = agentId,
                            CreateDate = GenerateDate(new DateTime(2020, 01, 01), new DateTime(2023, 03, 31))
                        };
                        context.Orders.Add(order);
                                           
                        int detailsCount = rand.Next(1, 6);

                        List<int> goodsIndices = new List<int>(Enumerable.Range(1, 10));

                        for (int d = 0; d < detailsCount; d++)
                        {
                            if (goodsIndices.Count == 0) break;

                            int index = rand.Next(0, goodsIndices.Count);
                            int goodId = goodsIndices[index];
                            goodsIndices.RemoveAt(index);

                            var detail = new OrderDetail
                            {
                                GoodId = goodId,
                                GoodCount = rand.Next(1, 11),
                                Order = order
                            };

                            context.OrderDetails.Add(detail);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public static void SeedGoodProperties(EcommerceContext context)
        {
            Random random = new Random();
            if (!context.GoodProperties.Any())
            {
                var goods = (from good in context.Goods
                            where good.Id <= 4
                            select good).ToArray();

                var colors = (from color in context.Colors
                             select color).ToArray();

                foreach(var good in goods)
                {
                    int propertiesCount = random.Next(2, 4);

                    for (int i = 0; i < propertiesCount; i++)
                    {
                        DateTime bDate = GenerateDate(new DateTime(2020, 01, 01), new DateTime(2023, 03, 31));
                        DateTime eDate = GenerateDate(bDate, new DateTime(2023, 03, 31));

                        var goodProperty = new GoodProperty
                        {
                            Good = good,
                            Color = colors[i],
                            Bdate = bDate,
                            Edate = eDate
                        };

                        context.GoodProperties.Add(goodProperty);
                    }
                }
                context.SaveChanges();
            } else
            {
                var query = from goodProperty in context.GoodProperties
                            select goodProperty;

                foreach (var gp in query)
                    Console.WriteLine($"Id: {gp.Id}, GoodId: {gp.GoodId}, ColorId: {gp.ColorId}, Bdate: {gp.Bdate}, Edate: {gp.Edate}");
            }
        }
    }
}
