using Localdorateste.Models;

namespace RentCarSys.Application.Interfaces
{
    public interface IClientesRepository
    {
        Task<List<Cliente>> ObterTodosClientesAsync();
        Task<Cliente> ObterClientePorIdAsync(int clienteId);
        Task<Cliente> ObterClientePorCPFAsync(long cpf);
        Task AdicionarClienteAsync(Cliente cliente);
        Task AtualizarClienteAsync(Cliente cliente);
        Task ExcluirClienteAsync(Cliente cliente);
    }
}
