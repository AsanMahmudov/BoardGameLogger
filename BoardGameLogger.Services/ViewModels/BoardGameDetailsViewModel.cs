using BoardGameLogger.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.ViewModels
{
    public class BoardGameDetailsViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int YearPublished { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string Description { get; set; } = null!;
        public string PublisherName { get; set; }

        public List<BoardGameLoanInfoViewModel> LoanLogs { get; set; } = new List<BoardGameLoanInfoViewModel>();
    }
}
