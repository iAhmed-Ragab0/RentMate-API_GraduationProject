using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Review>> GetAllReviewsForProperty(int id)
        {
            var query = _Context.Reviews
                                .Where(a => a.PropertyId == id);

            if (!query.Any())
            {
                return Enumerable.Empty<Review>();
            }


            return await query.ToListAsync().ConfigureAwait(false);
        }

        public double GetAvgRatingForProperty(int id)
        {
            var exists = _Context.Reviews.FirstOrDefault(b => b.PropertyId == id);
            if (exists == null)
            {
                return 0;
            }
            else
            {
                var avgRating = _Context.Reviews.Where(r => r.PropertyId == id).Average(a => a.Rating);
                return avgRating;
            }

            //var query = _Context.Reviews.Where(r => r.PropertyId == id).Average(a => a.Rating);
            //if(query.)

            //return query;
        }

    }
}
