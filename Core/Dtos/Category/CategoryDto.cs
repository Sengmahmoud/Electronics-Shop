using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Category
{
    public record CategoryDto:DtoBase<int>
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
