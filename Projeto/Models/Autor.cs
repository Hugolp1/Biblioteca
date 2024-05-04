using Projeto.Models;

namespace Projeto;

public class Autor{

    private static int ultimoId = 0;
    public Autor()
    {
        if (AutorId == 0)
        {
            AutorId = ++ultimoId;
        }
    }

    public Autor(string nome, string nacionalidade)
    {
        if (AutorId == 0)
        {
            AutorId = ++ultimoId;
        }
        Nome = nome;
        Nacionalidade = nacionalidade;
    }

    public int AutorId { get; set; }
    public string? Nome { get; set; }
    public string? Nacionalidade { get; set; }
    public DateTime DataNascimento { get; set; }
}
