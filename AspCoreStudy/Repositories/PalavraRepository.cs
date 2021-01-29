using AspCoreStudy.Database;
using AspCoreStudy.Helpers;
using AspCoreStudy.Models;
using AspCoreStudy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy.Repositories
{
    public class PalavraRepository : IPalavraRepository
    {
        private readonly Contexto _banco;

        public PalavraRepository(Contexto banco)
        {
            _banco = banco;
        }


        public PaginationList<Palavra> ObterTodas(PalavraUrlQuery urlQuery)
        {
            var lista = new PaginationList<Palavra>();

            var query = _banco.Palavras.AsQueryable();
            if (urlQuery.data.HasValue)
                query = query.Where(x => x.dataCriacao.Date > urlQuery.data.Value.Date || x.dataAlteracao.Value.Date > urlQuery.data.Value);

            if (urlQuery.pagNum.HasValue)
            {
                var qtdeRegistrosTotalSemPaginacao = query.Count();

                query = query.Skip(((urlQuery.pagNum ?? 0) - 1) * (urlQuery.qtdePorPag ?? 10)).Take(urlQuery.qtdePorPag ?? 10);

                var paginacao = new Paginacao();
                paginacao.numeroPagina = urlQuery.pagNum ?? 0;
                paginacao.registrosPorPagina = urlQuery.qtdePorPag ?? 10;
                paginacao.totalRegistros = qtdeRegistrosTotalSemPaginacao;
                paginacao.totalPaginas = (int)Math.Ceiling((double)qtdeRegistrosTotalSemPaginacao / paginacao.registrosPorPagina);

                lista.Paginacao = paginacao;
            }

            lista.Results.AddRange(query.ToList());

            return lista;
        }

        public Palavra Obter(int id)
        {
            return _banco.Palavras.AsNoTracking().FirstOrDefault(x => x.id == id);
        }

        public void Cadastrar(Palavra palavra)
        {
            _banco.Add(palavra);
            _banco.SaveChanges();
        }

        public void Atualizar(Palavra palavra)
        {
            _banco.Update(palavra);
            _banco.SaveChanges();
        }


        public void Deletar(int id)
        {
            var obj = Obter(id);
            obj.ativo = false;
            _banco.Update(obj);
            _banco.SaveChanges();
        }


    }
}
