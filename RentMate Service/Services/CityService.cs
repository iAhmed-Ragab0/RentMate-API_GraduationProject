using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.City;
using RentMate_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class CityService :ICityService
    {

        private IUnitOfWork _IUnitOfWork;
        private ICityRepository _CityRepository;




        public CityService(IUnitOfWork iUnitOfWork,
            ICityRepository cityRepository
            )
        {
            _IUnitOfWork = iUnitOfWork;
            _CityRepository = cityRepository;

        }


        //get all cities for a governrate
        public async Task<IEnumerable<CityDTO_Get>> GetAllCitiesForGovernrate(int GovId)
        {

            var CitiesinGov = await _CityRepository.GetAllCitiesForGovernrate(GovId);


            if (CitiesinGov.Any())
            {
                List<CityDTO_Get> CitiesList = new List<CityDTO_Get>();

                foreach (var city in CitiesinGov)
                {
                    CityDTO_Get City = new CityDTO_Get() 
                    {
                        CityId = city.Id,
                        CityName = city.city_name_en
                    };


                    CitiesList.Add(City);
                }

                return CitiesList;
            }
            else
            {
                return Enumerable.Empty<CityDTO_Get>();
            }

        }

    }
}
