using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EcommerceSqlSolutions.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new EcommerceContext())
{
    context.Database.EnsureCreated();

    // check if there are agents in the database
    // if not then execute code to add 15 agents
    if (!context.Agents.Any())
    {
        for (int i = 1; i <= 15; i++)
        {
            context.Agents.Add(new Agent
            {
                Id = i,
                Name = $"Agent{i}"
            });
        }

        context.SaveChanges();
    }
    else
    {
        for (int i = 1; i <= 15; i++)
        {
            Agent agent = context.Agents.Find(i);
            Console.WriteLine($"Id: {agent.Id}, Name: {agent.Name}");
        }
    }

    if (!context.Goods.Any())
    {
        for (int i = 1; i <= 10; i++)
        {
            context.Goods.Add(new Good
            {
                Id = i,
                Name = $"Good{i}"
            });
        }

        context.SaveChanges();
    } 
    else
    {
        for (int i = 1; i <= 10; i++)
        {
            Good good = context.Goods.Find(i);
            Console.WriteLine($"Id: {good.Id}, Name: {good.Name}");
        }
    }

    if (!context.Colors.Any())
    {
        for (int i = 1; i <= 3; i++)
        {
            context.Colors.Add(new Color
            {
                Id = i,
                Name = $"Color{i}"
            });
        }

        context.SaveChanges();
    }
    else
    {
        for (int i = 1; i <= 3; i++)
        {
            Color color = context.Colors.Find(i);
            Console.WriteLine($"Id: {color.Id}, Name: {color.Name}");
        }
    }
}

