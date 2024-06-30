using System;

namespace Projeto.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataReserva { get; set; }
        public DateTime? DataExpiracao { get; set; }

        public Reserva()
        {
            DataReserva = DateTime.Now;
            DataExpiracao = DataReserva.AddDays(7); // Expira em 7 dias se não for atendida
        }
    }
}
