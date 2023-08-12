using Localdorateste.Data;
using Localdorateste.Models;
using Microsoft.EntityFrameworkCore;
using RentCarSys.Application.Interfaces;

namespace RentCarSys.Application.Repository
{
    public class ReservasRepository : IReservasRepository
    {
        private readonly Contexto _contexto;

        public ReservasRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Reserva>> ObterTodasReservasAsync()
        {

            return await _contexto.Reservas.Include(x => x.Cliente).Include(x => x.Veiculo).ToListAsync();
        }

        public async Task<Reserva> ObterReservaPorIdAsync(int reservaId)
        {
            return await _contexto.Reservas.FirstOrDefaultAsync(x => x.Id == reservaId);   
        }

        public async Task AdicionarReservaAsync(Reserva reserva)
        {
            await _contexto.Reservas.AddAsync(reserva);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarReservaAsync(Reserva reserva)
        {
            _contexto.Reservas.Update(reserva);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirReservaAsync(Reserva reserva)
        {
            _contexto.Reservas.Remove(reserva);
            await _contexto.SaveChangesAsync();
        }
    }
}
