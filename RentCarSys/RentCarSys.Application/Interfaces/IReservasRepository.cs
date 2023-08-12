using Localdorateste.Models;

namespace RentCarSys.Application.Interfaces
{
    public interface IReservasRepository
    {
        Task<List<Reserva>> ObterTodasReservasAsync();
        Task<Reserva> ObterReservaPorIdAsync(int reservaId);
        Task AdicionarReservaAsync(Reserva reserva);
        Task AtualizarReservaAsync(Reserva reserva);
        Task ExcluirReservaAsync(Reserva reserva);
    }
}
