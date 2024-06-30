import React, { useState } from 'react';
import axios from 'axios';

function UsuarioCadastrar() {
  const [nome, setNome] = useState<string>('');
  const [email, setEmail] = useState<string>('');

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const novoUsuario = {
      nome,
      email
    };

    axios
      .post('http://localhost:5108/api/usuario/cadastrar', novoUsuario)
      .then((response) => {
        console.log('Usuário cadastrado com sucesso', response.data);
        setNome('');
        setEmail('');
      })
      .catch((error) => {
        console.error('Erro ao cadastrar usuário', error);
      });
  }

  return (
    <div>
      <h2>Cadastrar Novo Usuário</h2>
      <form onSubmit={handleSubmit}>
        <label>
          Nome:
          <input
            type="text"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            required
          />
        </label>
        <label>
          Email:
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </label>
        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
}

export default UsuarioCadastrar;