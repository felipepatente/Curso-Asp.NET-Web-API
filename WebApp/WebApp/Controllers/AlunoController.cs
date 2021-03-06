﻿using App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*","*","*")]
    [RoutePrefix("api/Aluno")]
    [Authorize (Roles = Funcao.Professor)]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                AlunoModel alunos = new AlunoModel();
                return  Ok(alunos.ListarAlunos());
            }
            catch (System.Exception ex)
            {

                return InternalServerError(ex);
            }
            
        }

        [HttpGet]
        [Route("Recuperar/{id:int}/{nome?}/{sobrenome?}")]
        public IHttpActionResult RecuperPorId(int id, string nome = null, string sobrenome = null)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();

                return Ok(aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        [HttpGet]
        [Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(4)}")]
        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();

                IEnumerable<AlunoDTO> alunos = aluno.ListarAlunos().Where(x => x.data == data || x.Nome == nome);

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);
                
            }
            catch (System.Exception ex)
            {

                return InternalServerError(ex);
            }            
        }

        [HttpPost]
        public IHttpActionResult Post(AlunoDTO aluno)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                AlunoModel _aluno = new AlunoModel();
                _aluno.Inserir(aluno);

                return Ok (_aluno.ListarAlunos());
            }
            catch (System.Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] AlunoDTO aluno)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();
                aluno.Id = id;

                _aluno.Atualizar(aluno);

                return Ok (_aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();
                _aluno.Deletar(id);

                return Ok("Deletado com sucesso");
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
