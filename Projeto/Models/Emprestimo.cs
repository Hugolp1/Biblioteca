using System;
using Projeto.Models;
namespace Projeto.Models;
public class Emprestimo{
        public int Id { get; set; }
        public String? LivroId { get; set; }
        public Livro? Livro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
}