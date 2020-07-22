using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*","*","*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno alunos = new Aluno();
                return  Ok(alunos.ListarAlunos());
            }
            catch (System.Exception ex)
            {

                return InternalServerError(ex);
            }
            
        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route("Recuperar/{id:int}/{nome?}/{sobrenome?}")]
        public Aluno Get(int id, string nome = null, string sobrenome = null)
        {
            return new Aluno().ListarAlunos(id).FirstOrDefault();
        }

        [HttpGet]
        [Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(4)}")]
        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                Aluno aluno = new Aluno();

                IEnumerable<Aluno> alunos = aluno.ListarAlunos().Where(x => x.data == data || x.Nome == nome);

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
        public IHttpActionResult Post(Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
                _aluno.Inserir(aluno);

                return Ok (_aluno.ListarAlunos());
            }
            catch (System.Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
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
                Aluno _aluno = new Aluno();
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
