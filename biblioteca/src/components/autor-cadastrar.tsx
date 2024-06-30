// src/components/AutorCadastrar.tsx
import React, { useState } from 'react';
import axios from 'axios';
import { Autor } from '../interfaces/Autor';

function AutorCadastrar() {
    const [nome, setNome] = useState<string>('');
    const [nacionalidade, setNacionalidade] = useState<string>('');
    const [dataNascimento, setDataNascimento] = useState<string>('');

    function handleSubmit(e: any) {
        e.preventDefault();

        const novoAutor: Autor = {
            nome,
            nacionalidade,
            dataNascimento: new Date()
        };

        axios
            .post<Autor>('http://localhost:5108/api/autor/cadastrar', novoAutor)
            .then((response) => {
                console.log("Autor cadastrado com sucesso", response.data);
                setNome('');
                setNacionalidade('');
                setDataNascimento('');
            })
            .catch((error) => {
                console.error("Erro ao cadastrar autor", error);
            });
    }

    return (
        <div>
            <h2>Cadastrar Novo Autor</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    Nome:
                    <input type="text" value={nome} onChange={e => setNome(e.target.value)} required />
                </label>
                <label>
                    Nacionalidade:
                    <input type="text" value={nacionalidade} onChange={e => setNacionalidade(e.target.value)} required />
                </label>
                <label>
                    Data de Nascimento:
                    <input type="date" value={dataNascimento} onChange={e => setDataNascimento(e.target.value)} required />
                </label>
                <button type="submit">Cadastrar</button>
            </form>
        </div>
    );
}

export default AutorCadastrar;