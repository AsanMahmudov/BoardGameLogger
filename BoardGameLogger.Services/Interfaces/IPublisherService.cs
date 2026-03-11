using BoardGameLogger.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.Interfaces
{

    public interface IPublisherService
    {
        Task<IEnumerable<PublisherViewModel>> GetAllPublishersAsync();

        Task AddPublisherAsync(PublisherFormModel model);

        Task DeletePublisherAsync(int id);

        Task<PublisherViewModel?> GetByIdAsync(int id);
    }
}
