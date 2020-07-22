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
                alu.Ra = Convert.ToInt32(resultado["Id"]);

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
    }
}