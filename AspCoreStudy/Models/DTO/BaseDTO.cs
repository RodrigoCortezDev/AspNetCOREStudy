using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreStudy.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Link { get; set; } = new List<LinkDTO>();
    }
}
