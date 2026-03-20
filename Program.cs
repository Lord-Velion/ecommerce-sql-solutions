using System;
using System.Linq;
using EcommerceSqlSolutions.Models;
using Microsoft.EntityFrameworkCore;

using var db = new EcommerceContext();

Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new agent");
db.Add(new Agent { Name = "Ivan"});
await db.SaveChangesAsync();

// Read
Console.WriteLine("Querying for an agent");
var agent = await db.Agents
    .OrderBy(a => a.Id)
    .FirstAsync();

Console.WriteLine($"Id: {agent.Id}, Name: {agent.Name}");

// Delete
Console.WriteLine("Delete the agent");
db.Remove(agent);
await db.SaveChangesAsync();


