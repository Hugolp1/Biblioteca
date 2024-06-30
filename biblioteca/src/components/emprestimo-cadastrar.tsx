import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Usuario } from '../interfaces/Usuario';
import { Livro } from '../interfaces/Livro';
import { Emprestimo } from '../interfaces/Emprestimo';

function EmprestimoCadastrar() {
  const [livros, setLivros] = useState<Livro[]>([]);
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);
  const [livroId, setLivroId] = useState<string>('');
  const [usuarioId, setUsuarioId] = useState<number>(0);
  const [emprestimos, setEmprestimos] = useState<Emprestimo[]>([]);

  useEffect(() => {
    carregarLivros();
    carregarUsuarios();
  }, []);

  function carregarLivros() {
    axios.get<Livro[]>('http://localhost:5108/api/livro/listar')
      .then((response) => setLivros(response.data))
      .catch((error) => console.error('Erro ao carregar livros', error));
  }

  function carregarUsuarios() {
    axios.get<Usuario[]>('http://localhost:5108/api/usuario/listar')
      .then((response) => setUsuarios(response.data))
      .catch((error) => console.error('Erro ao carregar usuários', error));
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const novoEmprestimo: Emprestimo = {
        livroId,
        usuarioId,
        dataEmprestimo: new Date(),
        id: 0
    };

    axios.post<Emprestimo>('http://localhost:5108/api/emprestimo/cadastrar', novoEmprestimo)
      .then((response) => {
        console.log('Empréstimo cadastrado com sucesso', response.data);
        setLivroId('');
        setUsuarioId(0);
      })
      .catch((error) => {
        console.error('Erro ao cadastrar empréstimo', error);
      });
  }

  return (
    <div>
      <h2>Realizar Empréstimo</h2>
      <form onSubmit={handleSubmit}>
        <label>
          Livro:
          <select value={livroId} onChange={(e) => setLivroId(e.target.value)} required>
            <option value="">Selecione um livro</option>
            {livros.map((livro) => (
              <option key={livro.livroId} value={livro.livroId}>
                {livro.titulo}
              </option>
            ))}
          </select>
        </label>
        <label>
          Usuário:
          <select value={usuarioId} onChange={(e) => setUsuarioId(Number(e.target.value))} required>
            <option value={0}>Selecione um usuário</option>
            {usuarios.map((usuario) => (
              <option key={usuario.id} value={usuario.id}>
                {usuario.nome}
              </option>
            ))}
          </select>
        </label>
        <button type="submit">Realizar Empréstimo</button>
      </form>
    </div>
  );
}

export default EmprestimoCadastrar;
