using ApiCatalogoLivros.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiCatalogoLivros.Repositories
{
    public class LivroSqlServerRepository : ILivroRepository
    {
        private readonly SqlConnection sqlConnection;

        public LivroSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Livro>> Obter(int pagina, int quantidade)
        {
            var livro = new List<Livro>();

            var comando = $"select * from Livro order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livro.Add(new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Editora = (string)sqlDataReader["Editora"],
                    Autor = (string)sqlDataReader["Autor"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return livro;
        }

        public async Task<Livro> Obter(Guid id)
        {
            Livro livro = null;

            var comando = $"select * from Livro where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livro = new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Editora = (string)sqlDataReader["Editora"],
                    Autor = (string)sqlDataReader["Autor"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return livro;
        }

        public async Task<List<Livro>> Obter(string nome, string editora, string autor)
        {
            var livro = new List<Livro>();

            var comando = $"select * from Livro where Nome = '{nome}' and Editora = '{editora}' and Editora = '{autor}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                livro.Add(new Livro
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Editora = (string)sqlDataReader["Editora"],
                    Autor = (string)sqlDataReader["Autor"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return livro;
        }

        public async Task Inserir(Livro livro)
        {
            var comando = $"insert Livro (Id, Nome, Editora,Autor , Preco) values ('{livro.Id}', '{livro.Nome}', '{livro.Editora}', '{livro.Autor}', {livro.Preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Livro livro)
        {
            var comando = $"update Livro set Nome = '{livro.Nome}', Editora = '{livro.Editora}', Autor = '{livro.Autor}', Preco = {livro.Preco.ToString().Replace(",", ".")} where Id = '{livro.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Livro where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
