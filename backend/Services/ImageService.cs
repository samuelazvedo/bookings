using backend.Models;
using backend.Repositories;
using System.Collections.Generic;

namespace backend.Services
{
    public class ImageService
    {
        private readonly ImageRepository _imageRepository;

        public ImageService(ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public IEnumerable<Image> GetImagesByMotel(int motelId)
        {
            return _imageRepository.GetByMotelId(motelId);
        }

        public IEnumerable<Image> GetImagesBySuite(int suiteId)
        {
            return _imageRepository.GetBySuiteId(suiteId);
        }

        public bool AddImageToMotel(int motelId, string path)
        {
            var image = new Image { MotelId = motelId, Path = path };
            _imageRepository.Add(image);
            return true;
        }

        public bool AddImageToSuite(int suiteId, string path)
        {
            var image = new Image { SuiteId = suiteId, Path = path };
            _imageRepository.Add(image);
            return true;
        }
    }
}