// src/components/LivroCadastrar.tsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Livro } from '../interfaces/Livro';
import { Autor } from '../interfaces/Autor';

function LivroCadastrar() {
    const [titulo, setTitulo] = useState<string>('');
    const [genero, setGenero] = useState<string>('');
    const [autorId, setAutorId] = useState<number | null>(null);
    const [numeroExemplares, setNumeroExemplares] = useState<number>(1);
    const [autores, setAutores] = useState<Autor[]>([]);

    useEffect(() => {
        axios.get<Autor[]>("http://localhost:5108/api/autor/listar")
            .then((response) => setAutores(response.data))
            .catch((error) => console.error("Erro ao carregar autores", error));
    }, []);

    function handleSubmit(e: any) {
        e.preventDefault();

        const novoLivro: Livro = {
            titulo,
            genero,
            autorId: autorId!,
            numeroExemplares,
            emprestimos: [],
            reservas: []
        };

        axios
            .post<Livro>('http://localhost:5108/api/livro/cadastrar', novoLivro)
            .then((response) => {
                console.log("Livro cadastrado com sucesso", response.data);
                setTitulo('');
                setGenero('');
                setAutorId(null);
                setNumeroExemplares(1);
            })
            .catch((error) => {
                console.error("Erro ao cadastrar livro", error);
            });
    }

    return (
        <div>
            <h2>Cadastrar Novo Livro</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    Título:
                    <input type="text" value={titulo} onChange={e => setTitulo(e.target.value)} required />
                </label>
                <label>
                    Gênero:
                    <input type="text" value={genero} onChange={e => setGenero(e.target.value)} required />
                </label>
                <label>
                    Autor:
                    <select
                        value={autorId ?? ''}
                        onChange={(e) => setAutorId(parseInt(e.target.value))}
                        required
                    >
                        <option value="">Selecione um autor</option>
                        {autores.map((autor) => (
                            <option key={autor.autorId} value={autor.autorId}>
                                {autor.nome}
                            </option>
                        ))}
                    </select>
                </label>
                <label>
                    Número de Exemplares:
                    <input
                        type="number"
                        value={numeroExemplares}
                        onChange={(e) => setNumeroExemplares(parseInt(e.target.value))}
                        min="1"
                        required
                    />
                </label>
                <button type="submit">Cadastrar</button>
            </form>
        </div>
    );
}

export default LivroCadastrar;
