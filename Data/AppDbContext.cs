using Microsoft.EntityFrameworkCore;
using FinanceControl.Models;

namespace FinanceControl.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura precisão do decimal no banco (18 dígitos, 2 casas decimais)
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);

        // Seed: categorias iniciais para não começar com banco vazio
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Salário", Description = "Receitas de trabalho" },
            new Category { Id = 2, Name = "Alimentação", Description = "Gastos com comida" },
            new Category { Id = 3, Name = "Transporte", Description = "Gastos com transporte" },
            new Category { Id = 4, Name = "Saúde", Description = "Gastos com saúde" },
            new Category { Id = 5, Name = "Lazer", Description = "Gastos com entretenimento" }
        );
    }
}