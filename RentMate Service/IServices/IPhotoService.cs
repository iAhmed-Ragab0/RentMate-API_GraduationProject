using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using RentMate_Service.DTOs.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IPhotoService 
    {
        public Task<string> ReturnMainPhotoForProperty(int propId);
        public Task<ImageUploadResult> AddPhotoToCloudAsync(IFormFile file);
        public Task<DeletionResult> DeletePhotoFromCloudAsync(string publicId);
        public Task<bool> SavePhotoTodb(Photo photo);
        public Task<IEnumerable<string>> GetAllPhotosForProperty(int propId);
        public  Task<PhotoDTO_GetById> GetphotoById(int photoId);
        public Task<bool> deletePhotoFromdb(int photoId);
        public Task<IEnumerable<photoDTO_Update>> GetPropertyPhotos_Update(int propId);






    }
}
