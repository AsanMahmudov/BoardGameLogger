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
        int Id { get; set; }

        [MaxLength(256)]
        [Required]
        string Name { get; set; } = null!;

        [MaxLength(100)]
        string? Country { get; set; }
    }
}
