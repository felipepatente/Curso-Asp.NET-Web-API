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

        public List<Aluno> ListarAlunosDB()
        {
            var listaAlunos = new List<Aluno>();

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Alunos";

            IDataReader resultado = selectCmd.ExecuteReader();
            while (resultado.Read())
            {
                var alu = new Aluno();

                alu.Id = Convert.ToInt32(resultado["Id"]);
                alu.Nome = Convert.ToString(resultado["Nome"]);
                alu.Sobrenome = Convert.ToString(resultado["Sobrenome"]);
                alu.Telefone = Convert.ToString(resultado["Telefone"]);
                alu.Ra = Convert.ToInt32(resultado["Ra"]);

                listaAlunos.Add(alu);
            }

            conexao.Close();

            return listaAlunos;
        }

        public void InsertAlunoDB(Aluno aluno)
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

        public void AtualizarAlunoDB(Aluno aluno)
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

        public void DeletarAlunoDB(int id)
        {
            IDbCommand deleteCmd = conexao.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM Alunos WHERE Id = @Id";

            IDbDataParameter paramId = new SqlParameter("Id", id);
            deleteCmd.Parameters.Add(paramId);

            deleteCmd.ExecuteNonQuery();
        }
    }
}