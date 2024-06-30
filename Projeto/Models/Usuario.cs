using System.Collections.Generic;
using Projeto.Models;
namespace Projeto.Models;
public class Usuario
    {
        public Usuario()
        {
            Emprestimos = new List<Emprestimo>();
            Reservas = new List<Reserva>();
        }

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }
        public List<Reserva> Reservas { get; set; } // Adicionado
}