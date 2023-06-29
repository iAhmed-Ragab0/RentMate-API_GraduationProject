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
    public class CityRepository : GenericRepository<City> ,ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<string>> GetAllCitiesForGovernrate(int GovernrateId)
        {
            var query = _Context.Cities
                                .Where(a => a.governorate_id == GovernrateId);

            if (!query.Any())
            {
                return Enumerable.Empty<string>();
            }


            var cityNames = await query.Select(a => a.city_name_en).ToListAsync().ConfigureAwait(false);
            return cityNames;
        }
    }
}
