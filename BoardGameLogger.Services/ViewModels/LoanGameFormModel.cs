using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.ViewModels
{
    public class LoanGameFormModel
    {
        public int BoardGameId { get; set; }
        public string BoardGameTitle { get; set; } = null!;

         [StringLength(256)]
        public string BorrowerName { get; set; } = null!;

        [Required]
        public DateTime LoanDate { get; set; } = DateTime.Now;
    }
}
