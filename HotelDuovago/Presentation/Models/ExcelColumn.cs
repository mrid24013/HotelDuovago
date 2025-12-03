using Presentation.Enums;

namespace Presentation.Models
{
    public class ExcelColumn
    {
        public required string Name { get; set; }
        public ColumnType Type { get; set; }
    }
}
