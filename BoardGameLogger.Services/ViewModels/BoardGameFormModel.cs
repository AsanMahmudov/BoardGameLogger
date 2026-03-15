using BoardGameLogger.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace BoardGameLogger.Core.ViewModels
{

    public class BoardGameFormModel
    {
        [Required]
        [StringLength(128, MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [Required]
        [Range(1800, 2100)] 
        public int YearPublished { get; set; }

        [Range(1, 100)]
        public int MinPlayers { get; set; }

        [Range(1, 100)]
        public int MaxPlayers { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Please select a publisher.")]
        public int SelectedPublisherId { get; set; }

        public IEnumerable<SelectListItem>? Publishers { get; set; }
    }
}