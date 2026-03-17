using System.ComponentModel.DataAnnotations;

namespace BoardGameLogger.Core.ViewModels
{
    public class LoanGameFormModel
    {
        public int BoardGameId { get; set; }
        public string BoardGameTitle { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the name of the person borrowing the game.")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "Borrower name must be between 2 and 256 characters.")]
        public string BorrowerName { get; set; } = null!;

        [Required(ErrorMessage = "The loan date is required.")]
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; } = DateTime.Now;
    }
}