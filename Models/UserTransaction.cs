using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopUpService.Models
{
    public class UserTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }

        [ForeignKey("Beneficiary")]
        public Guid BeneficiaryId { get; set; }

        public Beneficiary? Beneficiary { get; set; }


        [ForeignKey("TopUpOption")]
        public Guid OptionId { get; set; }

        public TopUpOption? TopUpOption { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public string? TransactionDetails { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
