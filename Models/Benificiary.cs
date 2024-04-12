using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopUpService.Models;

namespace TopUpService
{
    public class Beneficiary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BenificiaryId { get; set; }

        [Required, MaxLength(150)]
        public string? BenificiaryName { get; set; }

        [Required, MaxLength(20)]
        public string? NickName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Status Status { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User? User { get; set; }

    }
}