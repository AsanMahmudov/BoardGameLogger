using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Data.Models
{
    public class BoardGame
    {
        int Id { get; set; } 

        [MaxLength(128)]
        string Title { get; set; } = null!;

        [Required]
        string YearPublished { get; set; } = null!;

        int MinPlayers { get; set; }

        int MaxPlayers { get; set; }

        [MaxLength(1500)]
        [Required]
        string Description { get; set; } = null!;

        int publisherId { get; set; }
    }
}
