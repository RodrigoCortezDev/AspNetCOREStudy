using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy.Helpers
{
    public class Paginacao
    {
        public int numeroPagina { get; set; }
        public int registrosPorPagina { get; set; }
        public int totalRegistros { get; set; }
        public int totalPaginas { get; set; }
    }
}
