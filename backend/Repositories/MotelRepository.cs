using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MotelRepository
    {
        private readonly AppDbContext _context;

        public MotelRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Motel> GetAll()
        {
            return _context.Motels
                .Include(m => m.Suites)
                .Include(m => m.Images)
                .ToList();
        }

        public Motel? GetById(int id)
        {
            return _context.Motels
                .Include(m => m.Suites)
                .Include(m => m.Images)
                .FirstOrDefault(m => m.Id == id);
        }

        public void Add(Motel motel)
        {
            _context.Motels.Add(motel);
            _context.SaveChanges();
        }

        public void Update(Motel motel)
        {
            _context.Motels.Update(motel);
            _context.SaveChanges();
        }

        public void Delete(Motel motel)
        {
            _context.Motels.Remove(motel);
            _context.SaveChanges();
        }
    }
}