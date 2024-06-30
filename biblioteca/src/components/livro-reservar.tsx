import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Livro } from '../interfaces/Livro';
import { Reserva } from '../interfaces/Reserva';
import { Usuario } from '../interfaces/Usuario'; // Importe a interface de Usuário

function LivroReservar() {
  const [livros, setLivros] = useState<Livro[]>([]);
  const [livroId, setLivroId] = useState<string>('');
  const [usuarios, setUsuarios] = useState<Usuario[]>([]); // Estado para armazenar a lista de usuários
  const [usuarioId, setUsuarioId] = useState<number>(0); // Estado para armazenar o ID do usuário selecionado
  const [reservas, setReservas] = useState<Reserva[]>([]);

  useEffect(() => {
    carregarLivros();
    carregarUsuarios(); // Carrega a lista de usuários disponíveis ao montar o componente
  }, []);

  function carregarLivros() {
    axios.get<Livro[]>('http://localhost:5108/api/livro/listar')
      .then((response) => setLivros(response.data))
      .catch((error) => console.error('Erro ao carregar livros', error));
  }

  function carregarUsuarios() {
    axios.get<Usuario[]>('http://localhost:5108/api/usuario/listar') // Endpoint para listar usuários
      .then((response) => setUsuarios(response.data))
      .catch((error) => console.error('Erro ao carregar usuários', error));
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const novaReserva: Reserva = {
      livroId,
      dataReserva: new Date(),
      dataExpiracao: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000),
      reservaId: 0,
      usuarioId
    };

    axios.post<Reserva>('http://localhost:5108/api/reserva/cadastrar', novaReserva)
      .then((response) => {
        console.log('Reserva realizada com sucesso', response.data);
        setLivroId('');
        setUsuarioId(0);
      })
      .catch((error) => {
        console.error('Erro ao realizar reserva', error);
      });
  }

  return (
    <div>
      <h2>Reservar Livro</h2>
      <form onSubmit={handleSubmit}>
        <label>
          Livro:
          <select value={livroId} onChange={(e) => setLivroId(e.target.value)} required>
            <option value="">Selecione um livro disponível para reserva</option>
            {livros.filter(livro => !reservas.some(reserva => reserva.livroId === livro.livroId)).map((livro) => (
              <option key={livro.livroId} value={livro.livroId}>
                {livro.titulo}
              </option>
            ))}
          </select>
        </label>
        <label>
          Usuário:
          <select value={usuarioId} onChange={(e) => setUsuarioId(Number(e.target.value))} required>
            <option value="0">Selecione um usuário</option>
            {usuarios.map((usuario) => (
              <option key={usuario.id} value={usuario.id}>
                {usuario.nome}
              </option>
            ))}
          </select>
        </label>
        <button type="submit">Reservar</button>
      </form>
    </div>
  );
}

export default LivroReservar;
