using Microsoft.EntityFrameworkCore;
using RentMate_Domain;
using RentMate_Domain.Models;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository._2__Repositories
{
    public class WishingListRepository : GenericRepository<WishingList>, IWishingListRepository
    {
        public WishingListRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WishingList>> GetWishListByUserId(string id) 
        {
            var query = _Context.WishingList
                               .Include(p => p.Property)
                               .Include(p => p.Property.Photos)
                               .Include(p => p.Property.Owner)
                               .Include(p => p.Property.City)
                               .Include(p => p.Property.City.Governorate)
                               .Where(a => a.UserId == id);

            if (!query.Any())
            {
                return Enumerable.Empty<WishingList>();
            }

            return await query.ToListAsync().ConfigureAwait(false);

        }
    }
}
