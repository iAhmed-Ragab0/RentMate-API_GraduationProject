using RentMate_Domain.Models;
using RentMate_Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository._1__IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public  Task<User> SoftDeleteUserByIdAsync(string id);
        public Task<string> AddProfileImgForUser(string id,string imgUrl);


    }
}
