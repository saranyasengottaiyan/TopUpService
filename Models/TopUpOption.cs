using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopUpService
{
    public class TopUpOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OptionId { get; set; }

        [Required]
        public string? OptionName { set; get; }

        [Required]
        public int Amount { set; get; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}