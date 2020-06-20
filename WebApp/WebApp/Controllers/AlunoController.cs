using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        public IEnumerable<string> Get()
        {
            return new string[] { "Felipe", "Patente" };
        }

        // GET: api/Aluno/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Aluno
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Aluno/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
        }
    }
}
