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
            var palavraRepository = new PalavraRepository(_banco);
            var palavra = palavraRepository.Obter(id);
            if (palavra == null)
                return NotFound();

            return Ok(palavra);
        }


        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            try
            {
                var palavraRepository = new PalavraRepository(_banco);
                palavraRepository.Cadastrar(palavra);
                return Created($"/api/pessoas/{palavra.id}",palavra);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar([FromBody] Palavra palavra)
        {
            try
            {
                var palavraRepository = new PalavraRepository(_banco);
                palavraRepository.Atualizar(palavra);
                return Ok(palavra);
            }
            catch
            {
                return NotFound();
            }

        }


        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            try
            {
                var palavraRepository = new PalavraRepository(_banco);
                palavraRepository.Deletar(id);
                return Ok();
            }
            catch 
            {
                return NotFound();
            }
            
        }
    }
}
