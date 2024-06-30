import React, { useEffect, useState } from "react";
import axios from "axios";
import { Livro } from "../interfaces/Livro";

function LivroListar() {
  const [livros, setLivros] = useState<Livro[]>([]);

  useEffect(() => {
    carregarLivros();
  }, []);

  function carregarLivros() {
    axios.get<Livro[]>("http://localhost:5108/api/livro/listar")
      .then((response) => {
        setLivros(response.data);
        console.table(response.data);
      })
      .catch((error) => {
        console.log("Erro ao carregar livros", error);
      });
  }

  return (
    <div className="container">
      <h1>Listar Livros</h1>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Gênero</th>
            <th>Número de Exemplares</th>
          </tr>
        </thead>
        <tbody>
          {livros.map((livro) => (
            <tr key={livro.livroId}>
              <td>{livro.livroId}</td>
              <td>{livro.titulo}</td>
              <td>{livro.genero}</td>
              <td>{livro.numeroExemplares}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default LivroListar;
