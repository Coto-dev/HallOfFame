using HallOfFame.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.DAL.Data;

public class BackendDbContext : DbContext{
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Skill> Skills { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Person>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Skill>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) {
    }
}