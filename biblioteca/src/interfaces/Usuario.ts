import { Emprestimo } from "./Emprestimo";
import { Reserva } from "./Reserva";

export interface Usuario {
    id: number;
    nome: string;
    email: string;
    emprestimos: Emprestimo[];
    reservas: Reserva[];
}