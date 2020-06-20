﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        public IEnumerable<Aluno> Get()
        {
            Aluno alunos = new Aluno();
            return alunos.ListaAlunos();
        }

        // GET: api/Aluno/5
        public Aluno Get(int id)
        {
            return new Aluno().ListaAlunos().Where(x => x.Id == id).SingleOrDefault();
        }

        // POST: api/Aluno
        public List<Aluno> Post(Aluno aluno)
        {
            List<Aluno> alunos = new List<Aluno>();
            alunos.Add(aluno);

            return alunos;
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
