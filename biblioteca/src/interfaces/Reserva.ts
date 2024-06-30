import { Livro } from "./Livro";
import { Usuario } from "./Usuario";

export interface Reserva {
    reservaId: number;
    livroId: string;
    livro?: Livro;
    usuarioId: number;
    usuario?: Usuario;
    dataReserva: Date;
    dataExpiracao: Date;
}