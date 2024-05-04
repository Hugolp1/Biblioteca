using System;
using Microsoft.EntityFrameworkCore.Query;

namespace Projeto.Models;

public class Livro{
    public Livro()
    {
        LivroId = Guid.NewGuid().ToString(); 
    }

    public Livro(string titulo, string genero)
    {
        LivroId = Guid.NewGuid().ToString();
        Titulo = titulo;
        Genero = genero;
    }

    public string LivroId { get; set; }
    public string? Titulo { get; set; }
    public string? Genero { get; set; }
    public int AutorId { get; set; }
    public int NumeroExemplares { get; set; }

    public Autor? Autor { get; set; }
}
