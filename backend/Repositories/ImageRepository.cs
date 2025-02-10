using backend.Data;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class ImageRepository
    {
        private readonly AppDbContext _context;

        public ImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Image> GetAll()
        {
            return _context.Images.ToList();
        }

        public IEnumerable<Image> GetByMotelId(int motelId)
        {
            return _context.Images.Where(i => i.MotelId == motelId).ToList();
        }

        public IEnumerable<Image> GetBySuiteId(int suiteId)
        {
            return _context.Images.Where(i => i.SuiteId == suiteId).ToList();
        }

        public void Add(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public void Delete(Image image)
        {
            _context.Images.Remove(image);
            _context.SaveChanges();
        }
    }
}