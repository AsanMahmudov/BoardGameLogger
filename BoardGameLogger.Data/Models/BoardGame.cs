using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Data.Models
{
    public class BoardGame
    {
        [Key]
        public int Id { get; set; } 

        [MaxLength(128)]
        public string Title { get; set; } = null!;

        [Required]
        public int YearPublished { get; set; };

        public int MinPlayers { get; set; }

        [MaxLength(30)]
        public int MaxPlayers { get; set; }

        [MaxLength(1500)]
        [Required]
        public string Description { get; set; } = null!;

        public int PublisherId { get; set; }
                
        [ForeignKey(nameof(PublisherId))]
        public Publisher Publisher { get; set; } = null!;

        public ICollection<LoanLog> LoanLogs { get; set; } = new List<LoanLog>();

    }
}
