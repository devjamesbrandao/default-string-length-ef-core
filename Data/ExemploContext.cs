using EF_string.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EF_string.Data
{
    public class ExemploContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexao = "";
            optionsBuilder
                .UseSqlServer(conexao)
                .LogTo(Console.WriteLine, LogLevel.Information); // Log das queries no console da aplicação
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(
                var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string)))
            )
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.Entity<User>(builder => 
            {
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Name)
                       .IsRequired()
                       .HasColumnType("varchar(200)");

                builder.Property(p => p.CPF)
                       .IsRequired()
                       .HasColumnType("varchar(11)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}