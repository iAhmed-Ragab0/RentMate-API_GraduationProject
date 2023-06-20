using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.PropertyDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IPropertyDetailsService
    {
        public Task<bool> UpdateProertyServicesByIdAsync    
                                (int id, PropertyDetailsDTO ServicesDTO);
    }
}
