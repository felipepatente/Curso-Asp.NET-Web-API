using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json;

namespace WebApp.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public int Ra { get; set; }


        public List<Aluno> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = File.ReadAllText(caminhoArquivo);
            var alunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return alunos;
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