﻿using Microsoft.EntityFrameworkCore;

namespace Politics.Model
{
  public class PoliticsDbContext : DbContext
  {
    public PoliticsDbContext(DbContextOptions<PoliticsDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Politician> Politicians { get; set; }
  }
}
