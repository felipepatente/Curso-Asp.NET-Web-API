using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            List<Aluno> alunos = new List<Aluno>();

            alunos.Add(new Aluno(){ 
                Id = 2,
                Nome = "Marta",
                SobreNome = "Will",
                Telefone = "1195838756",
                Ra = 0001                
            });

            alunos.Add(new Aluno()
            {
                Id = 1,
                Nome = "João",
                SobreNome = "Santos",
                Telefone = "114662201",
                Ra = 0002
            });

            return alunos;
        }
    }
}