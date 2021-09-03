using ApiCatalogoLivros.Entities;
using ApiCatalogoLivros.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoLivros.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private static Dictionary<Guid, Livro> livros = new Dictionary<Guid, Livro>()
        {
            {Guid.Parse("80231fdf-c1ad-4ca4-9e61-20351ad8226b"), new Livro{ Id = Guid.Parse("80231fdf-c1ad-4ca4-9e61-20351ad8226b"), Nome = "A Cabana", Editora = "Arqueiro",Autor="William Paul Young", Preco = 80} },
            {Guid.Parse("6f0d4c46-b49f-4875-970c-ea33e16456bc"), new Livro{ Id = Guid.Parse("6f0d4c46-b49f-4875-970c-ea33e16456bc"), Nome = "A ARCA DE NOÉ", Editora = "Editora Companhia das Letrinhas", Autor="Vinícius de Moraes", Preco = 45} },
            {Guid.Parse("67b98b28-9f28-48fd-a5c0-1b4013a8e698"), new Livro{ Id = Guid.Parse("67b98b28-9f28-48fd-a5c0-1b4013a8e698"), Nome = "A RAINHA DA NEVE", Editora = "Moderna", Autor="Hans Christian Andersen", Preco = 30} },
            {Guid.Parse("a12d047c-af4e-42fd-89d3-5fb20b61e73e"), new Livro{ Id = Guid.Parse("a12d047c-af4e-42fd-89d3-5fb20b61e73e"), Nome = "CACHINHOS DE OURO", Editora = "FTD", Autor="Ana Maria Machado", Preco = 90} },
            {Guid.Parse("afa5b81c-1d68-4fa1-b644-fa6334485f1b"), new Livro{ Id = Guid.Parse("afa5b81c-1d68-4fa1-b644-fa6334485f1b"), Nome = "CHAPEUZINHO E O LOBO MAU", Editora = "Moderna", Autor="r Pedro Bandeira", Preco = 20} },
            {Guid.Parse("d751bd83-422c-4df1-b290-acd90acb30a9"), new Livro{ Id = Guid.Parse("d751bd83-422c-4df1-b290-acd90acb30a9"), Nome = "CONTOS DE PERRAULT", Editora = "Salamandra", Autor="Ruth Rocha", Preco = 85} }
        };

        public Task<List<Livro>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(livros.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Livro> Obter(Guid id)
        {
            if (!livros.ContainsKey(id))
                return Task.FromResult<Livro>(null);

            return Task.FromResult(livros[id]);
        }

        public Task<List<Livro>> Obter(string nome, string editora, string autor)
        {
            return Task.FromResult(livros.Values.Where(livro => livro.Nome.Equals(nome) && livro.Editora.Equals(editora) && livro.Autor.Equals(autor)).ToList());
        }

        public Task<List<Livro>> ObterSemLambda(string nome, string editora, string autor)
        {
            var retorno = new List<Livro>();

            foreach(var livro in livros.Values)
            {
                if (livro.Nome.Equals(nome) && livro.Editora.Equals(editora) && livro.Autor.Equals(autor))
                    retorno.Add(livro);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Livro livro)
        {
            livros.Add(livro.Id, livro);
            return Task.CompletedTask;
        }

        public Task Atualizar(Livro livro)
        {
            livros[livro.Id] = livro;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            livros.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
