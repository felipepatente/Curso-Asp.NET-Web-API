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
    }
}