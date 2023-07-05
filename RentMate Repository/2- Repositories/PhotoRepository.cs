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
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Photo>> GetPhotosByPropId(int id)
        {
            var query = _Context.Photos
                                .AsNoTracking()
                                .Where(a => a.PropertyId == id);

            if (!query.Any())
            {
                return Enumerable.Empty<Photo>();
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        //public async Task<bool> SavePhotoUrl(int id)
        //{
        //    var query = _Context.Photos
        //                        .AsNoTracking()
        //                        .Where(a => a.PropertyId == id);

        //    if (!query.Any())
        //    {
        //        return Enumerable.Empty<Photo>();
        //    }

        //    return await query.ToListAsync().ConfigureAwait(false);
        //}
    }
}
