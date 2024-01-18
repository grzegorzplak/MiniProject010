using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject010.Models
{
    [Table("MiniProject010_Colors")]
    public class Color
    {
        public int Id {  get; set; }
        public string Name { get; set; } = "";
        public string? HexValue { get; set; }
        public string? DecimalValue { get; set; }
    }
}
