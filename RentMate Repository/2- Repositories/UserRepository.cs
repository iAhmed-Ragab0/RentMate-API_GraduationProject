using Microsoft.EntityFrameworkCore;
using RentMate_Domain;
using RentMate_Domain.Models;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository._2__Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<User>> GetAllAsync(params Expression<Func<User, object>>[] includes)
        {
            var query = _Context.Users.Where(a=>a.IsDeleted == false );
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);

            }
            return await query.ToListAsync();
        }

        public async Task<User> SoftDeleteUserByIdAsync(string id) 
         {
            var query = await _Context.Users.FirstOrDefaultAsync(a=>a.Id == id);

            if (query != null)
            {
                query.IsDeleted= true;
                await _Context.SaveChangesAsync();
            }

            return query;
        }

        public async Task<string> AddProfileImgForUser(string id , string imgUrl)
        {
            var query = await _Context.Users.FirstOrDefaultAsync(a => a.Id == id);

            if (query == null)
            {
                return null;
            }
            else
            {
                query.ProfileImg = imgUrl;
                await _Context.SaveChangesAsync();
            }

            return query.ProfileImg;
        }



    }
}
