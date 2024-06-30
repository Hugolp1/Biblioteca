using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Projeto.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

app.MapGet("/", () => "Biblioteca de Livros!");

app.MapPost("/api/livro/cadastrar", ([FromBody] Livro livro, [FromServices] AppDbContext ctx) =>
{
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
app.MapGet("/api/emprestimo/listar", (AppDbContext ctx) =>
{
    if(ctx.Emprestimos.Any()){
        return Results.Ok(ctx.Emprestimos.Include(e=>e.Livro).Include(e=>e.Usuario).ToList());
    }
    return Results.NotFound("Não existem emprestimos cadastrados");
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

app.MapPost("/api/reserva/cadastrar", async (Reserva reserva, AppDbContext ctx) =>
{
    ctx.Reservas.Add(reserva);
    await ctx.SaveChangesAsync();
    return Results.Created("", reserva);
});

app.UseCors("AllowAll");
app.Run();