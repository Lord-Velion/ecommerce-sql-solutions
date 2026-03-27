using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EcommerceSqlSolutions;
using EcommerceSqlSolutions.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new EcommerceContext())
{
    context.Database.EnsureCreated();

    DatabaseSeeder.SeedAgents(context);
    DatabaseSeeder.SeedGoods(context);
    DatabaseSeeder.SeedColors(context);
    DatabaseSeeder.SeedOrders(context);
}

