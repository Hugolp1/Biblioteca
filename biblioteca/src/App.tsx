import React from 'react';
import { BrowserRouter, Link, Routes, Route } from 'react-router-dom';
import AutorCadastrar from './components/autor-cadastrar';
import LivroCadastrar from './components/livro-cadastrar';
import LivroListar from './components/livro-listar';
import UsuarioCadastrar from './components/usuario-cadastrar';
import EmprestimoCadastrar from './components/emprestimo-cadastrar';
import LivroReservar from './components/livro-reservar';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <nav>
          <ul>
            <li>
              <Link to="/autores/cadastrar">Cadastrar Autor</Link>
            </li>
            <li>
              <Link to="/livros">Listar Livros</Link>
            </li>
            <li>
              <Link to="/livros/cadastrar">Cadastrar Livro</Link>
            </li>
            <li>
              <Link to="/usuario/cadastrar">Cadastrar Usuario</Link>
            </li>
            <li>
              <Link to="/emprestimo/cadastrar">Fazer um empréstimo</Link>
            </li>
            <li>
              <Link to="/reserva/cadastrar">Fazer uma Reserva</Link>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/autores/cadastrar" element={<AutorCadastrar />} />
          <Route path="/livros" element={<LivroListar />} />
          <Route path="/livros/cadastrar" element={<LivroCadastrar />} />
          <Route path="/usuario/cadastrar" element={<UsuarioCadastrar />} />
          <Route path="/emprestimo/cadastrar" element={<EmprestimoCadastrar />} />
          <Route path="/reserva/cadastrar" element={<LivroReservar />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
