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

        private static DateOnly GenerateDate()
        {
            Random rand = new Random();
            int year = rand.Next(2020, 2024);
            int month = rand.Next(1, 13);
            int day = 1;

            // високосный год содержит 366 дней
            // обычный год содержит 365 дней
            // 2020 - високосный год
            // 2021 - невисокосный год
            // 2022 - невисокосный год
            // 2023 - невисокосный год
            
            switch (month)
            {
                case 1 or 3 or 5 or 7 or 8 or 10 or 12:
                    day = rand.Next(1, 32);
                    break;
                case 4 or 6 or 9 or 11:
                    day = rand.Next(1, 31);
                    break;
                case 2:
                    day = (year == 2020) ? (rand.Next(1, 30)) : (rand.Next(1, 29));
                    break;                
            }

            return new DateOnly(year, month, day);
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
                            CreateDate = new DateTime(GenerateDate(), new TimeOnly(0))
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
            else
            {
                var query1 = from order in context.Orders
                            select order;
                foreach (var order in query1)
                    Console.WriteLine($"Id: {order.Id}, AgentId: {order.AgentId}, CreateDate: {order.CreateDate}");

                var query2 = from orderDetail in context.OrderDetails
                        select orderDetail;

                foreach (var orderDetail in query2)
                    Console.WriteLine($"Id: {orderDetail.Id}, OrderId: {orderDetail.OrderId}, GoodId: {orderDetail.GoodId}, GoodCount: {orderDetail.GoodCount}");

            }
        }
    }
}
