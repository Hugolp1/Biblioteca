using System;
using System.Collections.Generic;

namespace Projeto.Models;
    public class Livro
    {
        public Livro()
        {
            LivroId = Guid.NewGuid().ToString();
            Emprestimos = new List<Emprestimo>();
            Reservas = new List<Reserva>();
        }

        public Livro(string titulo, string genero)
        {
            LivroId = Guid.NewGuid().ToString();
            Titulo = titulo;
            Genero = genero;
            Emprestimos = new List<Emprestimo>();
            Reservas = new List<Reserva>();
        }

        public string LivroId { get; set; }
        public string? Titulo { get; set; }
        public string? Genero { get; set; }
        public int AutorId { get; set; }
        public int NumeroExemplares { get; set; }
        public Autor? Autor { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }
        public List<Reserva> Reservas { get; set; } // Adicionado
    }