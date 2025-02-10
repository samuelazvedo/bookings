using backend.DTOs;
using backend.Models;
using backend.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace backend.Services
{
    public class SuiteService
    {
        private readonly SuiteRepository _suiteRepository;
        private readonly MotelRepository _motelRepository;

        public SuiteService(SuiteRepository suiteRepository, MotelRepository motelRepository)
        {
            _suiteRepository = suiteRepository;
            _motelRepository = motelRepository;
        }

        public IEnumerable<SuiteDTO> GetAllSuites()
        {
            return _suiteRepository.GetAll().Select(suite => new SuiteDTO
            {
                Id = suite.Id,
                MotelId = suite.MotelId,
                SuiteName = suite.SuiteName,
                BasePrice = suite.BasePrice
            }).ToList();
        }

        public SuiteDTO? GetSuiteById(int id)
        {
            var suite = _suiteRepository.GetById(id);
            if (suite == null) return null;

            return new SuiteDTO
            {
                Id = suite.Id,
                MotelId = suite.MotelId,
                SuiteName = suite.SuiteName,
                BasePrice = suite.BasePrice
            };
        }

        public bool CreateSuite(SuiteDTO suiteDto)
        {
            var motel = _motelRepository.GetById(suiteDto.MotelId);
            if (motel == null) 
            {
                throw new Exception($"Motel com ID {suiteDto.MotelId} não encontrado.");
            }
            
            var suite = new Suite
            {
                MotelId = suiteDto.MotelId,
                SuiteName = suiteDto.SuiteName,
                BasePrice = suiteDto.BasePrice
            };

            _suiteRepository.Add(suite);
            return true;
        }

        public bool UpdateSuite(int id, SuiteDTO suiteDto)
        {
            var existing = _suiteRepository.GetById(id);
            if (existing == null) return false;

            existing.MotelId = suiteDto.MotelId;
            existing.SuiteName = suiteDto.SuiteName;
            existing.BasePrice = suiteDto.BasePrice;

            _suiteRepository.Update(existing);
            return true;
        }

        public bool DeleteSuite(int id)
        {
            var existing = _suiteRepository.GetById(id);
            if (existing == null) return false;

            _suiteRepository.Delete(existing);
            return true;
        }
    }
}
