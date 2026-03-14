using BoardGameLogger.Core.Interfaces;
using BoardGameLogger.Core.ViewModels;
using BoardGameLogger.Data;
using BoardGameLogger.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameLogger.Core.Services
{
    public class PublisherService : IPublisherService
    {
        private BoardGameLoggerDbContext _Dbcontext;

        public PublisherService(BoardGameLoggerDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        public async Task AddPublisherAsync(PublisherFormModel model)
        {
            bool publisherExists = await _Dbcontext.Publishers
                .AnyAsync(p=> p.Name == model.Name && p.Country == model.Country);


            if (publisherExists)
            {
                throw new InvalidOperationException("Publisher is already in library.");
            }

            Publisher newPublisher = new Publisher
            {
                Name = model.Name,
                Country = model.Country
            };

            await _Dbcontext.Publishers.AddAsync(newPublisher);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(int id)
        {
            Publisher? publisherToDelete = await _Dbcontext.Publishers.FindAsync(id);

            if (publisherToDelete == null)
            {
                throw new InvalidOperationException("Publisher not found.");
            }

            _Dbcontext.Publishers.Remove(publisherToDelete);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PublisherViewModel>> GetAllPublishersAsync()
        {
            List<PublisherViewModel> publishers = await _Dbcontext.Publishers
                .Select(p => new PublisherViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country
                })
                .ToListAsync();

            return publishers;
        }

        public async Task<PublisherViewModel?> GetByIdAsync(int id)
        {
           var publisher = await _Dbcontext.Publishers
                .Where(p => p.Id == id)
                .Select(p => new PublisherViewModel
                {
                    Name = p.Name,
                    Country = p.Country
                })
                .FirstOrDefaultAsync();

            return publisher;
        }
    }
}
