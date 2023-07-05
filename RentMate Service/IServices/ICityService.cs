using RentMate_Service.DTOs.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface ICityService  
    {
        public Task<IEnumerable<CityDTO_Get>> GetAllCitiesForGovernrate(int GovId);

    }
}
