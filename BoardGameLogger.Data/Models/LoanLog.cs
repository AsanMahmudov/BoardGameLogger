using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Data.Models
{
    public class LoanLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string BorrowerName { get; set; } = null!;

        [Required]
        public DateTime LoanDate { get; set; }

        [Required]
        public int BoardGameId { get; set; }

        [ForeignKey(nameof(BoardGameId))]
        public BoardGame BoardGame { get; set; } = null!;
    }
}
