using AspCoreStudy.Helpers;
using AspCoreStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy.Repositories.Interfaces
{
    public interface IPalavraRepository
    {
        PaginationList<Palavra> ObterTodas(PalavraUrlQuery urlQuery);

        Palavra Obter(int id);

        void Cadastrar(Palavra palavra);

        void Atualizar(Palavra palavra);

        void Deletar(int id);
    }
}
