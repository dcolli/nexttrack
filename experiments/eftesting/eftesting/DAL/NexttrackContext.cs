using System;
using eftesting.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace eftesting.DAL
{
    public class NexttrackContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet <Stream> Streams { get; set; }
    }
}