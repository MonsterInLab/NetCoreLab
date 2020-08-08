namespace Data.EFCCore31.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Command")]
    public partial class Command
    {
        public int Id { get; set; }
        [StringLength(500)]
        public string HowTo { get; set; }
        [StringLength(500)]
        public string Line { get; set; }
        [StringLength(500)]
        public string Platform { get; set; }
    }
}
