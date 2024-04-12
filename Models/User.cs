using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopUpService.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? DOB { get; set; }
        public string? Address { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }

        public ICollection<Beneficiary>? Beneficiaries { get; set; }

    }
}
