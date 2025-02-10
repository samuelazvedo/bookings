using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class SuiteRepository
    {
        private readonly AppDbContext _context;

        public SuiteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Suite> GetAll()
        {
            return _context.Suites
                .Include(s => s.Motel)
                .Include(m => m.Images)
                .ToList();
        }

        public Suite? GetById(int id)
        {
            return _context.Suites
                .Include(s => s.Motel)
                .Include(m => m.Images)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Add(Suite suite)
        {
            _context.Suites.Add(suite);
            _context.SaveChanges();
        }

        public void Update(Suite suite)
        {
            _context.Suites.Update(suite);
            _context.SaveChanges();
        }

        public void Delete(Suite suite)
        {
            _context.Suites.Remove(suite);
            _context.SaveChanges();
        }
    }
}