using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Projeto;
using Projeto.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapGet("/", () => "Biblioteca de Livros!");

app.MapPost("/api/livro/cadastrar", ([FromBody] Livro livro, [FromServices] AppDbContext ctx) =>{
    var autor = ctx.Autores.FirstOrDefault(a => a.AutorId == livro.AutorId);

    if (autor == null)
    {
        return Results.NotFound("Autor não encontrado com o AutorId fornecido.");
    }

    livro.Autor = autor;

    ctx.Livros.Add(livro);

    ctx.SaveChanges();

    return Results.Created("", livro);
});

app.MapGet("/api/livro/listar", ([FromServices] AppDbContext ctx) => {
    if(ctx.Livros.Any()){
        return Results.Ok(ctx.Livros.ToList());
    }
    return Results.NotFound("Não existem livros cadastrados");
});

app.MapDelete("/api/livro/deletar/{id}", ([FromRoute]string id, [FromServices] AppDbContext ctx) =>{
    var livro = ctx.Livros.FirstOrDefault(x => x.LivroId == id);
    if (livro == null){
        return Results.NotFound("Livro não encontrado!");
    }

    ctx.Livros.Remove(livro);
    ctx.SaveChanges();

    return Results.Ok("Livro deletado!");
});

app.MapPut("api/livro/alterar/{id}", ([FromRoute]string id, Livro novoLivro, [FromServices] AppDbContext ctx) =>
{
    Livro? livro = ctx.Livros.FirstOrDefault(x => x.LivroId == id);
    if (livro == null)
    {
        return Results.NotFound("Livro não encontrado!");
    }

    livro.Titulo = novoLivro.Titulo;
    livro.Genero = novoLivro.Genero;
    livro.AutorId = novoLivro.AutorId;
    livro.NumeroExemplares = novoLivro.NumeroExemplares;

    ctx.SaveChanges();

    return Results.Ok("Livro alterado!");
});

app.MapPost("/api/autor/cadastrar", ([FromBody] Autor autor, [FromServices] AppDbContext ctx) =>{
    ctx.Autores.Add(autor);
    ctx.SaveChanges();
    return Results.Created("", autor);
});

app.MapGet("/api/autor/listar", ([FromServices] AppDbContext ctx) => {
    if(ctx.Autores.Any()){
        return Results.Ok(ctx.Autores.ToList());
    }
    return Results.NotFound("Não existem autores cadastrados");
});

app.Run();