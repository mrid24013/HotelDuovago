using Presentation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class ExcelColumn
    {
        public required string Name { get; set; }
        public ColumnType Type { get; set; }
    }
}
