using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.ViewModels
{
    public class LoanGameViewModel
    {
        public int Id { get; set; }
        public string BorrowerName { get; set; } = null!;
        public DateTime LoanDate { get; set; }
        public string BoardGameTitle { get; set; } = null!;
        public int BoardGameId { get; set; }
    }
}
