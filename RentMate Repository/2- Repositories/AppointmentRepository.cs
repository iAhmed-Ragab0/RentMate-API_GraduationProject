using Microsoft.EntityFrameworkCore;
using RentMate_Domain;
using RentMate_Domain.Models;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository.IRepositories;
using RentMate_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository._2__Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentForOwner(string Ownerid)
        {
            var query = _Context.Appointments
                                .Include(p => p.Owner)
                                .Where(a => a.OwnerId == Ownerid);

            if (!query.Any())
            {
                return Enumerable.Empty<Appointment>();
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentForTenant
            (string tenantId)
        {
            var query = _Context.Appointments
                                .Include(p => p.Tenant)
                                .Where(a => a.TenantId == tenantId);

            if (!query.Any())
            {
                return Enumerable.Empty<Appointment>();
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}
