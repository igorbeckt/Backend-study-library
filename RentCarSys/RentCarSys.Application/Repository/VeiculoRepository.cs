using Localdorateste.Data;
using Localdorateste.Models;
using Microsoft.EntityFrameworkCore;
using RentCarSys.Application.Interfaces;

namespace RentCarSys.Application.Repository
{
    public class VeiculosRepository : IVeiculosRepository
    {
        private readonly Contexto _contexto;

        public VeiculosRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Veiculo>> ObterTodosVeiculosAsync()
        {
            return await _contexto.Veiculos.ToListAsync();
        }

        public async Task<Veiculo> ObterVeiculoPorIdAsync(int veiculoId)
        {
            return await _contexto.Veiculos.FirstOrDefaultAsync(x => x.Id == veiculoId);
        }

        public async Task<Veiculo> ObterVeiculoPorPlacaAsync(string placa)
        {
            return await _contexto.Veiculos.FirstOrDefaultAsync(x => x.Placa == placa);
        }

        public async Task AdicionarVeiculoAsync(Veiculo veiculo)
        {
            await _contexto.Veiculos.AddAsync(veiculo);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarVeiculoAsync(Veiculo veiculo)
        {
            _contexto.Veiculos.Update(veiculo);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirVeiculoAsync(Veiculo veiculo)
        {
            _contexto.Veiculos.Remove(veiculo);
            await _contexto.SaveChangesAsync();
        }
    }

}
