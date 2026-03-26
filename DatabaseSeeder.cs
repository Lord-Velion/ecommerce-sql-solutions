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
            else
            {
                for (int i = 1; i <= 15; i++)
                {
                    var agent = context.Agents.Find(i);
                    Console.WriteLine($"Id: {agent.Id}, Name: {agent.Name}");
                }
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
            else
            {
                for (int i = 1; i <= 10; i++)
                {
                    var good = context.Goods.Find(i);
                    Console.WriteLine($"Id: {good.Id}, Name: {good.Name}");
                }
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
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    var color = context.Colors.Find(i);
                    Console.WriteLine($"Id: {color.Id}, Name: {color.Name}");
                }
            }
        }
    }
}
