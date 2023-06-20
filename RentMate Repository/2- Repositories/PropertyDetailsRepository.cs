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
    public class PropertyDetailsRepository : GenericRepository<PropertyDetails>, IPropertyDetailsReopsitory
    {
        public PropertyDetailsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
