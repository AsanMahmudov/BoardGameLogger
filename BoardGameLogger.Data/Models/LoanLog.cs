using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Data.Models
{
    public class LoanLog
    {
        int id { get; set; }

        [Required]
        [MaxLength(256)]
        string BorrowerName { get; set; } = null!;

        [Required]
        DateTime LoanDate { get; set; }

        [Required]
        int BoardGameId { get; set; }
    }
}
