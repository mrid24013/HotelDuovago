using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class ProductReport
    {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string Manufacturer { get; set; }
    }
}
