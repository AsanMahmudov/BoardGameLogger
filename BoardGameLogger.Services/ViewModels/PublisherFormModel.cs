using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.ViewModels
{
    public class PublisherFormModel
    {
            [Required]
            [StringLength(256, MinimumLength = 2)] 
            public string Name { get; set; } = null!;

            [StringLength(100)] 
            public string? Country { get; set; }

    }
}
