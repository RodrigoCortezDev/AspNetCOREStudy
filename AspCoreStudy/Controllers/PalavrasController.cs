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


        [HttpGet("{id}", Name = "ObterTodasPalavras")]
        public ActionResult ObterTodas([FromQuery] PalavraUrlQuery urlQuery)
        {
            var lista = _repo.ObterTodas(urlQuery);
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(lista.Paginacao));

            if (lista.Paginacao.numeroPagina > lista.Paginacao.totalPaginas)
                return NotFound();

            return Ok(lista.ToList());
        }


        [HttpGet("{id}",Name ="ObterPalavra")]
        public ActionResult Obter(int id)
        {
            var obj = _repo.Obter(id);
            if (obj == null)
                return NotFound();

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(obj);
            palavraDTO.Link = new List<LinkDTO>();
            palavraDTO.Link.Add(new LinkDTO("Self", Url.Link("ObterPalavra", new { id = palavraDTO.id }), "GET"));
            palavraDTO.Link.Add(new LinkDTO("Update", Url.Link("AtualizarPalavra", new { id = palavraDTO.id }), "PUT"));
            palavraDTO.Link.Add(new LinkDTO("Delete", Url.Link("DeletarPalavra", new { id = palavraDTO.id }), "DELETE"));

            return Ok(palavraDTO);
        }


        [HttpPost("",Name ="CadastrarPalavra")]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _repo.Cadastrar(palavra);            
            return Created($"/api/pessoas/{palavra.id}",palavra);
        }


        [HttpPut("{id}", Name = "AtualizarPalavra")]
        public ActionResult Atualizar(int id, [FromBody] Palavra palavra)
        {
            var obj = _repo.Obter(id);
            if (obj == null)
                return NotFound(); 

            _repo.Atualizar(palavra);
            return Ok(palavra);
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
