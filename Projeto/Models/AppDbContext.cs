using Microsoft.EntityFrameworkCore;

namespace Projeto.Models;
public class AppDbContext : DbContext
{
    public DbSet<Livro> Livros { get; set; }

    public DbSet<Autor> Autores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=biblioteca.db");
    }
}