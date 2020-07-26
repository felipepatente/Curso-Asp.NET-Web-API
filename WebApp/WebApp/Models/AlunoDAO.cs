using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Models
{
    public class AlunoDAO
    {
        //public string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<AlunoDTO> ListarAlunosDB(int? id)
        {
            try
            {
                var listaAlunos = new List<AlunoDTO>();
                IDbCommand selectCmd = conexao.CreateCommand();

                if (id == null)
                {
                    selectCmd.CommandText = "SELECT * FROM Alunos";
                }
                else
                {
                    selectCmd.CommandText = $"SELECT * FROM Alunos where id = {id}";
                }

                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var alu = new AlunoDTO()
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = Convert.ToString(resultado["Nome"]),
                        Sobrenome = Convert.ToString(resultado["Sobrenome"]),
                        Telefone = Convert.ToString(resultado["Telefone"]),
                        Ra = Convert.ToInt32(resultado["Ra"])
                    };

                    listaAlunos.Add(alu);
                }

                return listaAlunos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally{
                conexao.Close();
            }
        }

        public void InsertAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Alunos (Nome, Sobrenome, Telefone, Ra) Values (@Nome, @Sobrenome, @Telefone, @Ra)";

                IDbDataParameter paramNome = new SqlParameter("Nome", aluno.Nome);
                insertCmd.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("Sobrenome", aluno.Sobrenome);
                insertCmd.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("Telefone", aluno.Telefone);
                insertCmd.Parameters.Add(paramTelefone);

                IDbDataParameter paramRa = new SqlParameter("Ra", aluno.Ra);
                insertCmd.Parameters.Add(paramRa);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }

        public void AtualizarAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "UPDATE Alunos SET Nome = @Nome, Sobrenome = @Sobrenome, Telefone = @Telefone, Ra = @Ra WHERE Id = @Id";

                IDbDataParameter paramNome = new SqlParameter("Nome", aluno.Nome);
                updateCmd.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("Sobrenome", aluno.Sobrenome);
                updateCmd.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("Telefone", aluno.Telefone);
                updateCmd.Parameters.Add(paramTelefone);

                IDbDataParameter paramRa = new SqlParameter("Ra", aluno.Ra);
                updateCmd.Parameters.Add(paramRa);

                IDbDataParameter paramId = new SqlParameter("Id", aluno.Id);
                updateCmd.Parameters.Add(paramId);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }

        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM Alunos WHERE Id = @Id";

                IDbDataParameter paramId = new SqlParameter("Id", id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }
    }
}