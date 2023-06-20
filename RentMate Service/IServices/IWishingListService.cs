using RentMate_Service.DTOs.Appointment;
using RentMate_Service.DTOs.WishingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IWishingListService
    {
        public Task<IEnumerable<WishingListDTO_Get>> GetWishListByUserIdAsync(string Id);
        public Task<WishingList> DeleteWishByIdAsync(int id);
        public Task<WishingListDTO_Post> AddWish(WishingListDTO_Post newWish);


    }
}
