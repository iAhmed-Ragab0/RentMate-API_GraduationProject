using RentMate_Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO_GetAll>> GetAllUsers();
        public Task<User> GetUserByIdAsync(string id);
        public Task<User> HardDeleteUserByIdAsync(string id);
        public Task<User> SoftDeleteUserByIdAsync(string id);
        public Task<bool> UpdateUserByIdAsync(string id, UserDTO_Update UserDTO);
        public Task<bool> AddProfileImgForUserindb(string id, string imgUrl);
        public Task<List<UserPropertiesDTO_Getall>> GetUserProperties(string userId);


        




    }
}
