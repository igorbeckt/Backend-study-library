using Localdorateste.Models;

namespace RentCarSys.Application.Interfaces
{
    public interface IVeiculosRepository
    {
        Task<List<Veiculo>> ObterTodosVeiculosAsync();
        Task<Veiculo> ObterVeiculoPorIdAsync(int veiculoId);
        Task<Veiculo> ObterVeiculoPorPlacaAsync(string placa);
        Task AdicionarVeiculoAsync(Veiculo veiculo);
        Task AtualizarVeiculoAsync(Veiculo veiculo);
        Task ExcluirVeiculoAsync(Veiculo veiculo);
    }
}
