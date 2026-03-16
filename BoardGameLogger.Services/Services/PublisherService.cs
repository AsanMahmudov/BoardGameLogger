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

        //injeting our DB context, as usual
        public PublisherService(BoardGameLoggerDbContext dbcontext) => _Dbcontext = dbcontext;
        public async Task AddPublisherAsync(PublisherFormModel model)
        {

            //checking if our publisher is in the DB already 
            bool publisherExists = await _Dbcontext.Publishers
                .AnyAsync(p=> p.Name.ToLower() == model.Name.ToLower() && p.Country == model.Country);


            if (publisherExists)
                throw new InvalidOperationException("Publisher is already in library.");

            //mapping to EF model 
            Publisher newPublisher = new Publisher
            {
                Name = model.Name,
                Country = model.Country
            };

            //saving to DB
            await _Dbcontext.Publishers.AddAsync(newPublisher);
            await _Dbcontext.SaveChangesAsync();
        }
        public async Task DeletePublisherAsync(int id)
        {
            // Eager loading the BoardGames so we can check if the list is empty
            var publisherToDelete = await _Dbcontext.Publishers
                .Include(p => p.BoardGames)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (publisherToDelete == null)
            {
                throw new InvalidOperationException("Publisher not found.");
            }

            // We don't want to delete a publisher if they still have games assigned to them
            if (publisherToDelete.BoardGames.Any())
            {
                throw new InvalidOperationException("Cannot delete publisher: They still have games assigned to them.");
            }

            _Dbcontext.Publishers.Remove(publisherToDelete);
            await _Dbcontext.SaveChangesAsync();
        }

        //necessary for our publisher Index Page and our board game dropdown to select publisher
        public async Task<IEnumerable<PublisherViewModel>> GetAllPublishersAsync()
        {
            //mapping to view model
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

        //necessary for our publisher delete function -
        //the view passes the ID to the controller , the controller passes it here
        public async Task<PublisherViewModel?> GetByIdAsync(int id)
        {
           var publisher = await _Dbcontext.Publishers
                .Where(p => p.Id == id)
                .Select(p => new PublisherViewModel
                {
                    Id = id,
                    Name = p.Name,
                    Country = p.Country
                })
                .FirstOrDefaultAsync();

            return publisher;
        }
    }
}
