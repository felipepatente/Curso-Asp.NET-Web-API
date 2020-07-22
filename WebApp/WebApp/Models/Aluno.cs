using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Aluno
    {
        public int Id { get; set; }        
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string data { get; set; }
        public int Ra { get; set; }


        public List<Aluno> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = File.ReadAllText(caminhoArquivo);
            var alunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return alunos;
        }

        public List<Aluno> ListarAlunosDB()
        {
            string stringConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\felip\OneDrive\Documentos\GitHub\Curso-Asp.NET-Web-API\WebApp\WebApp\App_Data\Database.mdf;Integrated Security=True";
            IDbConnection conexao;

            conexao = new SqlConnection(stringConexao);
            conexao.Open();

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

        public bool ReescreverArquivo(List<Aluno> alunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/app_Data/Base.json");
            var json = JsonConvert.SerializeObject(alunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public Aluno Inserir(Aluno aluno)
        {
            var alunos = ListarAlunos();
            var maxId = alunos.Max(p => p.Id);
            aluno.Id = maxId + 1;
            alunos.Add(aluno);

            ReescreverArquivo(alunos);

            return aluno;
        }

        public Aluno Atualizar(int id, Aluno aluno)
        {
            var alunos = ListarAlunos();
            var itemIndex = ListarAlunos().FindIndex(p => p.Id == aluno.Id);

            if (itemIndex >= 0)
            {
                aluno.Id = id;
                alunos[itemIndex] = aluno;
            }
            else
            {
                return null;
            }

            ReescreverArquivo(alunos);

            return aluno;
        }

        public bool Deletar(int id)
        {
            var alunos = ListarAlunos();
            var itemIndex = alunos.FindIndex(p => p.Id == id);

            if (itemIndex >= 0)
            {
                alunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(alunos);

            return true;
        }
    }
}