using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.ViewModels
{
    public class BoardGameLoanInfoViewModel
    {
        public string BorrowerName { get; set; } = null!;
        public DateTime LoanDate { get; set; }
    }
}
