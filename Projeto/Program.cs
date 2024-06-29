using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Projeto.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapGet("/", () => "Biblioteca de Livros!");

app.MapPost("/api/livro/cadastrar", ([FromBody] Livro livro, [FromServices] AppDbContext ctx) =>
{
    var autor = ctx.Autores.FirstOrDefault(a => a.Id == livro.AutorId);

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

app.MapDelete("/api/livro/deletar/{id}", ([FromRoute]int id, [FromServices] AppDbContext ctx) =>{
    var livro = ctx.Livros.FirstOrDefault(x => x.Id == id);
    if (livro == null){
        return Results.NotFound("Livro não encontrado!");
    }

    ctx.Livros.Remove(livro);
    ctx.SaveChanges();

    return Results.Ok("Livro deletado!");
});

app.MapPut("api/livro/alterar/{id}", ([FromRoute]int id, Livro novoLivro, [FromServices] AppDbContext ctx) =>
{
    Livro? livro = ctx.Livros.FirstOrDefault(x => x.Id == id);
    if (livro == null)
    {
        return Results.NotFound("Livro não encontrado!");
    }

    livro.Titulo = novoLivro.Titulo;
    livro.AutorId = novoLivro.AutorId;

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

// CRUD para Usuario
app.MapPost("/api/usuario/cadastrar", ([FromBody] Usuario usuario, [FromServices] AppDbContext ctx) => {
    ctx.Usuarios.Add(usuario);
    ctx.SaveChanges();
    return Results.Created("", usuario);
});

app.MapGet("/api/usuario/listar", ([FromServices] AppDbContext ctx) => {
    if (ctx.Usuarios.Any()) {
        return Results.Ok(ctx.Usuarios.ToList());
    }
    return Results.NotFound("Não existem usuários cadastrados");
});

app.MapPut("/api/usuario/alterar/{id}", ([FromRoute]int id, Usuario novoUsuario, [FromServices] AppDbContext ctx) => {
    Usuario? usuario = ctx.Usuarios.FirstOrDefault(x => x.Id == id);
    if (usuario == null) {
        return Results.NotFound("Usuário não encontrado!");
    }

    usuario.Nome = novoUsuario.Nome;
    usuario.Email = novoUsuario.Email;

    ctx.SaveChanges();

    return Results.Ok("Usuário alterado!");
});

app.MapDelete("/api/usuario/deletar/{id}", ([FromRoute]int id, [FromServices] AppDbContext ctx) => {
    var usuario = ctx.Usuarios.FirstOrDefault(x => x.Id == id);
    if (usuario == null) {
        return Results.NotFound("Usuário não encontrado!");
    }

    ctx.Usuarios.Remove(usuario);
    ctx.SaveChanges();

    return Results.Ok("Usuário deletado!");
});

// CRUD para Emprestimo
app.MapGet("/api/emprestimo/listar", async (AppDbContext ctx) =>
{
    return await ctx.Emprestimos
        .Include(e => e.Livro)
        .Include(e => e.Usuario)
        .ToListAsync();
});

app.MapGet("/api/emprestimo/{id}", async (int id, AppDbContext ctx) =>
{
    var emprestimo = await ctx.Emprestimos
        .Include(e => e.Livro)
        .Include(e => e.Usuario)
        .FirstOrDefaultAsync(e => e.Id == id);

    return emprestimo is not null ? Results.Ok(emprestimo) : Results.NotFound();
});

app.MapPost("/api/emprestimo/cadastrar", async (Emprestimo emprestimo, AppDbContext ctx) =>
{
    ctx.Emprestimos.Add(emprestimo);
    await ctx.SaveChangesAsync();
    return Results.Created($"/api/emprestimo/{emprestimo.Id}", emprestimo);
});

app.MapPut("/api/emprestimo/alterar/{id}", async (int id, Emprestimo emprestimo, AppDbContext ctx) =>
{
    var emprestimoExistente = await ctx.Emprestimos.FindAsync(id);
    if (emprestimoExistente is null) return Results.NotFound();

    emprestimoExistente.LivroId = emprestimo.LivroId;
    emprestimoExistente.UsuarioId = emprestimo.UsuarioId;
    emprestimoExistente.DataEmprestimo = emprestimo.DataEmprestimo;
    emprestimoExistente.DataDevolucao = emprestimo.DataDevolucao;

    await ctx.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/emprestimo/deletar/{id}", async (int id, AppDbContext ctx) =>
{
    var emprestimo = await ctx.Emprestimos.FindAsync(id);
    if (emprestimo is null) return Results.NotFound();

    ctx.Emprestimos.Remove(emprestimo);
    await ctx.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
