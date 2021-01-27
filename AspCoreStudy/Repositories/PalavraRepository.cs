using AspCoreStudy.Database;
using AspCoreStudy.Helpers;
using AspCoreStudy.Models;
using AspCoreStudy.Repositories.Interfaces;
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


        public List<Palavra> ObterTodas(PalavraUrlQuery urlQuery)
        {
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
            }

            return query.ToList();
        }

        public Palavra Obter(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Palavra palavra)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Palavra palavra)
        {
            throw new NotImplementedException();
        }


        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }


    }
}
