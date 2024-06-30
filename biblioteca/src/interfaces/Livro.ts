import { Emprestimo } from "./Emprestimo";
import { Reserva } from "./Reserva";
import { Autor } from "./Autor";

export interface Livro {
    livroId?: string;
    titulo: string;
    genero: string;
    autorId: number;
    numeroExemplares: number;
    autor?: Autor;
    emprestimos: Emprestimo[];
    reservas: Reserva[];
}