using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class MotelService
    {
        private readonly MotelRepository _motelRepository;
        private readonly AppDbContext _context;

        public MotelService(MotelRepository motelRepository, AppDbContext context)
        {
            _motelRepository = motelRepository;
            _context = context;
        }

        public PagedResult<MotelDTO> GetAllMotels(int page, int pageSize)
        {
            var totalRecords = _context.Motels.Count();
            var totalPages = (int)System.Math.Ceiling(totalRecords / (double)pageSize);

            var data = _context.Motels
                .Include(m => m.Suites)
                .Include(m => m.Images)
                .OrderBy(m => m.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(motel => new MotelDTO
                {
                    Id = motel.Id,
                    Name = motel.Name,
                    Address = motel.Address,
                    Suites = motel.Suites.Select(suite => new SuiteDTO
                    {
                        Id = suite.Id,
                        SuiteName = suite.SuiteName,
                        BasePrice = suite.BasePrice
                    }).ToList(),
                    Images = motel.Images.Select(image => new ImageDTO
                    {
                        Id = image.Id,
                        Path = image.Path
                    }).ToList()
                })
                .ToList();

            return new PagedResult<MotelDTO>
            {
                Data = data,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize
            };
        }

        public MotelDTO? GetMotelById(int id)
        {
            var motel = _motelRepository.GetById(id);
            if (motel == null) return null;

            return new MotelDTO
            {
                Id = motel.Id,
                Name = motel.Name,
                Address = motel.Address,
                Suites = motel.Suites.Select(suite => new SuiteDTO
                {
                    Id = suite.Id,
                    SuiteName = suite.SuiteName,
                    BasePrice = suite.BasePrice
                }).ToList(),
                Images = motel.Images.Select(image => new ImageDTO
                {
                    Id = image.Id,
                    Path = image.Path
                }).ToList()
            };
        }

        public bool CreateMotel(MotelDTO motelDto)
        {
            var motel = new Motel
            {
                Name = motelDto.Name,
                Address = motelDto.Address,
                Suites = new List<Suite>(),
                Images = new List<Image>()
            };

            _motelRepository.Add(motel);
            return true;
        }

        public bool UpdateMotel(int id, MotelDTO motelDto)
        {
            var existing = _motelRepository.GetById(id);
            if (existing == null) return false;

            existing.Name = motelDto.Name;
            existing.Address = motelDto.Address;

            _motelRepository.Update(existing);
            return true;
        }

        public bool DeleteMotel(int id)
        {
            var existing = _motelRepository.GetById(id);
            if (existing == null) return false;

            _motelRepository.Delete(existing);
            return true;
        }
    }
}
