using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
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
        public async Task<IEnumerable<string>> GetAllCitiesForGovernrate(int GovId)
        {

            var CitiesinGov = await _CityRepository.GetAllCitiesForGovernrate(GovId);


            if (CitiesinGov.Any())
            {
                //List<string> CitiesList = new List<string>();

                //foreach (var city in CitiesinGov)
                //{
                //    var CityName = ;


                //    PropertyPhotosList.Add(PhotoUrl);
                //}

                return CitiesinGov;
            }
            else
            {
                return Enumerable.Empty<string>();
            }

        }

    }
}
