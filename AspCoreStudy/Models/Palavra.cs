using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy.Models
{
    public class Palavra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int id { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        public int pontuacao { get; set; }

        public bool ativo { get; set; }

        public DateTime dataCriacao { get; set; }

        public DateTime? dataAlteracao { get; set; }
    }
}
