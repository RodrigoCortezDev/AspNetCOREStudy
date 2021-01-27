using AspCoreStudy.Database;
using AspCoreStudy.Helpers;
using AspCoreStudy.Models;
using AspCoreStudy.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy.Controllers
{
    [Route("api/Palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly Contexto _banco;

        public PalavrasController(Contexto banco)
        {
            _banco = banco;
        }


        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas([FromQuery] PalavraUrlQuery urlQuery)
        {
            var palavraRepository = new PalavraRepository(_banco);
            var lista = palavraRepository.ObterTodas(urlQuery);
            
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginacao));

            //if (paginacao.numeroPagina > paginacao.totalPaginas)
            //    return NotFound();

            return Ok(lista.ToList());
        }


        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var palavra = _banco.Palavras.Find(id);
            if (palavra == null)
                return NotFound();

            return Ok(palavra);
        }


        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _banco.Add(palavra);
            _banco.SaveChanges();

            return Created($"/api/pessoas/{palavra.id}",palavra);
        }


        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {

            var palavraCtx = _banco.Palavras.AsNoTracking().FirstOrDefault(a => a.id == id);
            if (palavraCtx == null)
                return NotFound();

            palavra.id = id;
            _banco.Update(palavra);
            _banco.SaveChanges();

            return Ok(palavra);
        }


        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _banco.Palavras.Find(id);
            if (palavra == null)
                return NotFound();

            palavra.ativo = false;
            _banco.Update(palavra);
            _banco.SaveChanges();

            return Ok();
        }
    }
}
