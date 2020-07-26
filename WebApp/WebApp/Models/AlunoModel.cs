using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class AlunoModel
    {

        public List<AlunoDTO> ListarAlunos(int? id = null)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                return alunoDB.ListarAlunosDB(id);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao listar Alunos: Erro => {ex.Message}");
            }
        }

        public void Inserir(AlunoDTO aluno)
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

        public void Atualizar(AlunoDTO aluno)
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

        public AlunoDTO Atualizar(int id, AlunoDTO aluno)
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