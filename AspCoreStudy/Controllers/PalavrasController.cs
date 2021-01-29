using AspCoreStudy.Database;
using AspCoreStudy.Helpers;
using AspCoreStudy.Models;
using AspCoreStudy.Models.DTO;
using AspCoreStudy.Repositories;
using AspCoreStudy.Repositories.Interfaces;
using AutoMapper;
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
        private readonly IPalavraRepository _repo;
        private readonly IMapper _mapper;

        public PalavrasController(IPalavraRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet("", Name = "ObterTodasPalavras")]
        public ActionResult ObterTodas([FromQuery] PalavraUrlQuery urlQuery)
        {
            //recuperando dados
            var lista = _repo.ObterTodas(urlQuery);

            //Fazendoo map e adicionando linl
            var final = _mapper.Map<PaginationList<Palavra>, PaginationList<PalavraDTO>>(lista);
            foreach (var palavra in final.Results)
                palavra.Link.Add(new LinkDTO("Self", Url.Link("ObterPalavra", new { id = palavra.id }), "GET"));

            //Adicionando link a lista completa
            final.Links.Add(new LinkDTO("Self", Url.Link("ObterTodasPalavras", urlQuery), "GET"));

            //Se tiver paginação prepara os links de next e previous
            if (lista.Paginacao != null)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(lista.Paginacao));
                if (lista.Paginacao.numeroPagina > lista.Paginacao.totalPaginas)
                    return NotFound();

                if (final.Paginacao.numeroPagina + 1 <= final.Paginacao.totalPaginas)
                {
                    var newQueryString = new PalavraUrlQuery() { data = urlQuery.data, pagNum = urlQuery.pagNum + 1, qtdePorPag = urlQuery.pagNum };
                    final.Links.Add(new LinkDTO("Next", Url.Link("ObterTodasPalavras", newQueryString), "GET"));
                }
                if (final.Paginacao.numeroPagina - 1 >= 1)
                {
                    var newQueryString = new PalavraUrlQuery() { data = urlQuery.data, pagNum = urlQuery.pagNum - 1, qtdePorPag = urlQuery.pagNum };
                    final.Links.Add(new LinkDTO("Prev", Url.Link("ObterTodasPalavras", newQueryString), "GET"));
                }
            }

            return Ok(final);
        }


        [HttpGet("{id}", Name = "ObterPalavra")]
        public ActionResult Obter(int id)
        {
            var obj = _repo.Obter(id);
            if (obj == null)
                return NotFound();

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(obj);
            palavraDTO.Link.Add(new LinkDTO("Self", Url.Link("ObterPalavra", new { id = palavraDTO.id }), "GET"));
            palavraDTO.Link.Add(new LinkDTO("Update", Url.Link("AtualizarPalavra", new { id = palavraDTO.id }), "PUT"));
            palavraDTO.Link.Add(new LinkDTO("Delete", Url.Link("DeletarPalavra", new { id = palavraDTO.id }), "DELETE"));

            return Ok(palavraDTO);
        }


        [HttpPost("", Name = "CadastrarPalavra")]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            if (palavra == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity();

            palavra.dataCriacao = DateTime.Now;
            palavra.ativo = true;
            _repo.Cadastrar(palavra);

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(palavra);
            palavraDTO.Link.Add(new LinkDTO("Self", Url.Link("ObterPalavra", new { id = palavraDTO.id }), "GET"));

            return Created($"/api/pessoas/{palavra.id}", palavraDTO);
        }


        [HttpPut("{id}", Name = "AtualizarPalavra")]
        public ActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {
            if (palavra == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity();

            var obj = _repo.Obter(id);
            if (obj == null)
                return NotFound();

            palavra.id = id;
            palavra.ativo = obj.ativo;
            palavra.dataCriacao = obj.dataCriacao;
            palavra.dataAlteracao = obj.dataAlteracao;
            _repo.Atualizar(palavra);

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(palavra);
            palavraDTO.Link.Add(new LinkDTO("Self", Url.Link("ObterPalavra", new { id = palavraDTO.id }), "GET"));

            return Ok(palavraDTO);
        }


        [HttpDelete("{id}", Name = "DeletarPalavra")]
        public ActionResult Deletar(int id)
        {
            var obj = _repo.Obter(id);
            if (obj == null)
                return NotFound();

            _repo.Deletar(id);

            return NoContent();
        }
    }
}
