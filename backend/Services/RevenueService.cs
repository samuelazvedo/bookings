using backend.Repositories;
using System;

namespace backend.Services
{
    public class RevenueService
    {
        private readonly RevenueRepository _revenueRepository;

        public RevenueService(RevenueRepository revenueRepository)
        {
            _revenueRepository = revenueRepository;
        }

        public (decimal total, decimal change) GetTotalRevenue()
        {
            var totalAtual = _revenueRepository.GetTotalRevenue();
            var totalAnterior = _revenueRepository.GetPreviousTotalRevenue();
            decimal change = CalculateChange(totalAtual, totalAnterior);

            return (totalAtual, change);
        }

        public (decimal monthly, decimal change) GetMonthlyRevenue(int year, int month)
        {
            var faturamentoAtual = _revenueRepository.GetRevenueForMonth(year, month);
            var faturamentoAnterior = _revenueRepository.GetPreviousMonthlyRevenue(year, month);

            decimal change = CalculateChange(faturamentoAtual, faturamentoAnterior);
            return (faturamentoAtual, change);
        }

        public (decimal weekly, decimal change) GetWeeklyRevenue()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(6);

            var faturamentoAtual = _revenueRepository.GetRevenueForWeek(startOfWeek, endOfWeek);
            var faturamentoAnterior = _revenueRepository.GetPreviousWeeklyRevenue();

            decimal change = CalculateChange(faturamentoAtual, faturamentoAnterior);
            return (faturamentoAtual, change);
        }

        public decimal GetDailyRevenue(DateTime date)
        {
            return _revenueRepository.GetRevenueForDay(date);
        }

        private decimal CalculateChange(decimal valorAtual, decimal valorAnterior)
        {
            if (valorAnterior == 0) return 0; // Evita divisão por zero
            return Math.Round(((valorAtual - valorAnterior) / Math.Abs(valorAnterior)) * 100, 2);
        }
    }
}
