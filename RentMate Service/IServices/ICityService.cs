using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface ICityService  
    {
        public Task<IEnumerable<string>> GetAllCitiesForGovernrate(int GovId);

    }
}
