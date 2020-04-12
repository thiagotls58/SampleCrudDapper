using Dapper;
using Microsoft.Extensions.Configuration;
using SampleCrudDapper.IRepository;
using SampleCrudDapper.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SampleCrudDapper.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        IConfiguration _configuration;

        public ProdutosRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Add(Produtos produto)
        {
            string strConnection = this.GetStringConnection();
            int count = 0;
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                try
                {
                    connection.Open();
                    string query = @"insert into Produtos(Nome, Estoque, Preco) values(@Nome, @Estoque, @Preco);
                                      select cast(SCOPE_IDENTITY() as INT);";
                    count = connection.Execute(query, produto);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return count;
        }

        public int Delete(int id)
        {
            string strConnection = this.GetStringConnection();
            int count = 0;
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                try
                {
                    connection.Open();
                    string query = $@"delete from Produtos where ProdutoId = @Id";
                    count = connection.Execute(query, new { Id = id });
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return count;
        }

        public int Edit(Produtos produto)
        {
            string strConnection = this.GetStringConnection();
            int count = 0;
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                try
                {
                    connection.Open();
                    string query = "update Produtos set Nome = @Nome, Estoque = @Estoque, Preco = @Preco where ProdutoId = @ProdutoId";
                    count = connection.Execute(query, produto);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return count;
        }

        public Produtos Get(int id)
        {
            string strConnection = this.GetStringConnection();
            Produtos produto = new Produtos();
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                try
                {
                    connection.Open();
                    string query = "select * from Produtos where ProdutoId = @Id";
                    produto = connection.Query<Produtos>(query, new { Id = id }).FirstOrDefault();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return produto;
        }

        public IEnumerable<Produtos> GetAll()
        {
            string strConnection = this.GetStringConnection();
            IEnumerable<Produtos> produtos;
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                try
                {
                    connection.Open();
                    string query = "select * from Produtos";
                    produtos = connection.Query<Produtos>(query).ToList();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
            return produtos;
        }

        public string GetStringConnection()
        {
            string connection = _configuration.GetConnectionString("SqlServer");
            return connection;
        }
    }
}
