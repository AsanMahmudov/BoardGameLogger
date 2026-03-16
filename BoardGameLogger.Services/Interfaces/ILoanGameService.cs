using BoardGameLogger.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.Interfaces
{
    public interface ILoanGameService
    {
        Task<LoanGameFormModel?> GetLoanFormAsync(int id);

        Task AddLoanAsync(LoanGameFormModel model);

        Task ReturnGameAsync(int loanId);

        Task<IEnumerable<LoanGameViewModel>> GetAllLoansAsync();
    }
}
