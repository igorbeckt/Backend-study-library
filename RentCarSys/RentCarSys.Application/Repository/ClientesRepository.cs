using Localdorateste.Data;
using Localdorateste.Models;
using Microsoft.EntityFrameworkCore;
using RentCarSys.Application.Interfaces;

namespace RentCarSys.Application.Repository
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly Contexto _contexto;

        public ClientesRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Cliente>> ObterTodosClientesAsync()
        {
            return await _contexto.Clientes.ToListAsync();
        }

        public async Task<Cliente> ObterClientePorIdAsync(int clienteId)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(x => x.Id == clienteId);
        }

        public async Task<Cliente> ObterClientePorCPFAsync(long cpf)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        public async Task AdicionarClienteAsync(Cliente cliente)
        {
            await _contexto.Clientes.AddAsync(cliente);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarClienteAsync(Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirClienteAsync(Cliente cliente)
        {
            _contexto.Clientes.Remove(cliente);
            await _contexto.SaveChangesAsync();
        }
    }
}
