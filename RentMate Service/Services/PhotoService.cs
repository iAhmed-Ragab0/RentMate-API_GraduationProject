using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RentMate_Domain.Constant;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Photo;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.Reviews;
using RentMate_Service.IServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class PhotoService : IPhotoService
    {
        private IUnitOfWork _IUnitOfWork;
        private IPhotoRepository _PhotoRepository;
        private Cloudinary _Cloudinary;

        public PhotoService(
            IUnitOfWork iUnitOfWork,
            IPhotoRepository photoRepository,
            IOptions<CloudinarySettings> config
            )
        {
            _IUnitOfWork = iUnitOfWork;
            _PhotoRepository = photoRepository;

            var acc = new CloudinaryDotNet.Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _Cloudinary = new Cloudinary(acc);
        }



        // add photo to cloud
        public async Task<ImageUploadResult> AddPhotoToCloudAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var Stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, Stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
                    Folder = "PropertiesPhotos"
                };

                uploadResult = await _Cloudinary.UploadAsync(uploadParams); 
            }

                return uploadResult;

        }
        //-------------------------------------------------
        //save photo url in db
        public async Task<bool> SavePhotoTodb(Photo photo)
        {

            var result = await _PhotoRepository.AddAsync(photo);
            await _IUnitOfWork.Commit();

            if (result != null)  return true ;
            else return false;
        }

        //--------------------------------------------------
        //get photo by id 
        public async Task<PhotoDTO_GetById> GetphotoById(int photoId)
        {

            var photo = await _PhotoRepository.GetByIdAsync(photoId) ?? default;

            if (photo == null)
                return null;
            else
            {

                PhotoDTO_GetById pic = new PhotoDTO_GetById()
                {
                   IsMain= photo.IsMain,
                   PropertyId=photo.PropertyId,
                   PublicId=photo.PublicId,
                   Url = photo.Url,
                };

                return pic;
            }
        }
        //----------------------------------------------------------------------------
        // DELETE PHOTO FROM CLOUD
        public async Task<DeletionResult> DeletePhotoFromCloudAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _Cloudinary.DestroyAsync(deleteParams);     
        }

        //---------------------------------------------------
        // delete photo from db
        public async Task<bool> deletePhotoFromdb(int photoId)
        {

            var result = await _PhotoRepository.DeleteAsync(photoId);
            await _IUnitOfWork.Commit();

            if (result != null) return true;
            else return false;
        }



        // GET ALL PHOTOS FOR A PROPERTY
        public async Task<IEnumerable<string>> GetAllPhotosForProperty(int propId)
        {

            var PropertyPhotos = await _PhotoRepository.GetPhotosByPropId(propId);


            if (PropertyPhotos.Any())
            {
                List<string> PropertyPhotosList = new List<string>();

                foreach (var photo in PropertyPhotos)
                {
                    var PhotoUrl = photo.Url;
                    

                    PropertyPhotosList.Add(PhotoUrl);
                }

                return PropertyPhotosList;
            }
            else
            {
                return Enumerable.Empty<string>();
            }

        }
        //----------------------------------------------------------------------
        // GET Property Photos For Updtate
        public async Task<IEnumerable<photoDTO_Update>> GetPropertyPhotos_Update(int propId)
        {

            var PropertyPhotos = await _PhotoRepository.GetPhotosByPropId(propId);


            if (PropertyPhotos.Any())
            {
                List<photoDTO_Update> allPhotos = new List<photoDTO_Update>();

                foreach (var photo in PropertyPhotos)
                {
                    photoDTO_Update pic = new photoDTO_Update()
                    {
                       id= photo.Id,
                       Url= photo.Url,
                       PublicId = photo.PublicId,

                    };

                    allPhotos.Add(pic);
                }

                return allPhotos;

            }
            else
            {
                return Enumerable.Empty<photoDTO_Update>();
            }

        }
        //-----------------------------------------------------------------------
        public async Task<string> ReturnMainPhotoForProperty(int propId)
        {

            var propertyPhotos = await _PhotoRepository.GetPhotosByPropId(propId);

            foreach (var photo in propertyPhotos)
            {
                if (photo.IsMain)
                {
                    return photo.Url;
                }
            }

            return string.Empty;
        }

    }
}
