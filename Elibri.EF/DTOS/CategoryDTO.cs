using Elibri.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.EF.DTOS
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

    }
}
