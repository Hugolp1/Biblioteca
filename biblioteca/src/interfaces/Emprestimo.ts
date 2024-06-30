import { Livro } from "./Livro";
import { Usuario } from "./Usuario";

export interface Emprestimo {
    id: number;
    livroId: string;
    livro?: Livro;
    usuarioId: number;
    usuario?: Usuario;
    dataEmprestimo: Date;
}