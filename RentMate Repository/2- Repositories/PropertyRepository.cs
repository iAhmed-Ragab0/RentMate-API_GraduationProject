using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentMate_Domain;
using RentMate_Domain.Models;
using RentMate_Repository.IRepositories;
using RentMate_Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository.Repositories
{
    public class PropertyRepository : GenericRepository<Propertyy>, IPropertyRepository
    {
        public PropertyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Propertyy>> GetPropertiesByOwnerIdAsync(string id)
        {
            var query = _Context.Properties
                                .AsNoTracking()
                                .Include(p => p.Owner)
                                .Include(p => p.Photos)
                                .Include(p => p.City)
                                .Include(p => p.City.Governorate)
                                .Where(a => a.OwnerId == id);

            if (!query.Any())
            {
                return Enumerable.Empty<Propertyy>();
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }


    }
}
