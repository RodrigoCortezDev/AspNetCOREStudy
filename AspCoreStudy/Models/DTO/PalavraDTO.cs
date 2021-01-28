using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy.Models.DTO
{
    public class PalavraDTO : BaseDTO
    {
        public int id { get; set; }

        public string nome { get; set; }

        public int pontuacao { get; set; }

        public bool ativo { get; set; }

        public DateTime dataCriacao { get; set; }

        public DateTime? dataAlteracao { get; set; }
    }
}
