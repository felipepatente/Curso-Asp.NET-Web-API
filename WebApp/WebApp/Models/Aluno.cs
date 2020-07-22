using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            try
            {
                var alunoDB = new AlunoDAO();
                return alunoDB.ListarAlunosDB();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao listar Alunos: Erro => {ex.Message}");
            }
        }

        public bool ReescreverArquivo(List<Aluno> alunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/app_Data/Base.json");
            var json = JsonConvert.SerializeObject(alunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public void Inserir(Aluno aluno)
        {            
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.InsertAlunoDB(aluno);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao inserir Aluno: Erro => {ex.Message}");
            }
        }

        public void Atualizar(Aluno aluno)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.AtualizarAlunoDB(aluno);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao atualiar Aluno: Erro => {ex.Message}");
            }
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

        public void Deletar(int id)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.DeletarAlunoDB(id);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao deletar Aluno: Erro => {ex.Message}");
            }
        }
    }
}