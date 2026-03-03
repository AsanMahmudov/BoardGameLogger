using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Data.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string? Country { get; set; }

        public ICollection<BoardGame> BoardGames { get; set; } = new List<BoardGame>();
    }
}
