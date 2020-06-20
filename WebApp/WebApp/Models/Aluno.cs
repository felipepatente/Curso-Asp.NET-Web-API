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


        public List<Aluno> ListaAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = File.ReadAllText(caminhoArquivo);
            var alunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return alunos;
        }
    }
}