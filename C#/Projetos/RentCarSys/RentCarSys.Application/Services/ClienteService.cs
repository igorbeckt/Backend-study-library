using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RentCarSys.Application.DTO;
using RentCarSys.Application.Interfaces;
using RentCarSys.Enums;
using System.Web.Mvc;

namespace RentCarSys.Application.Services
{
    public class ClienteService
    {
        private readonly IClientesRepository _repositorioClientes;

        public ClienteService(IClientesRepository repositorioClientes)
        {
            _repositorioClientes = repositorioClientes;
        }

        public async Task<ResultViewModel<List<Cliente>>> BuscarTodosClientes()
        {
            try
            {
                var clientes = await _repositorioClientes.ObterTodosClientesAsync();
                return new ResultViewModel<List<Cliente>>(clientes);
            }
            catch
            {
                return new ResultViewModel<List<Cliente>>(erro: "05X05 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<ClienteGetDto>> BuscarClientePorId(int clienteId)
        {
            try
            {
                var cliente = await _repositorioClientes.ObterClientePorIdAsync(clienteId);
                if (cliente == null)
                {
                    return new ResultViewModel<ClienteGetDto>(erro: "Cliente não encontrado, verifique se o cliente já foi cadastrado!");
                }

                var clienteDto = new ClienteGetDto
                {
                    NomeCompleto = cliente.NomeCompleto
                };

                return new ResultViewModel<ClienteGetDto>(clienteDto);
            }
            catch
            {
                return new ResultViewModel<ClienteGetDto>(erro: "Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Cliente>> BuscarClientePorCPF(long cpf)
        {
            try
            {
                var cliente = await _repositorioClientes.ObterClientePorCPFAsync(cpf);
                if (cliente == null)
                {
                    return new ResultViewModel<Cliente>("Cliente não encontrado, verifique se o CPF está correto!");
                }

                return new ResultViewModel<Cliente>(cliente);
            }
            catch
            {
                return new ResultViewModel<Cliente>("Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Cliente>> CriarCliente(EditorClienteViewModel model)
        {
            try
            {
                var cliente = new Cliente
                {
                    Status = ClienteStatus.Online,
                    NomeCompleto = model.NomeCompleto,
                    Email = model.Email,
                    RG = model.RG,
                    CPF = model.CPF,
                };

                await _repositorioClientes.AdicionarClienteAsync(cliente);

                return new ResultViewModel<Cliente>(cliente);
            }
            catch
            {
                return new ResultViewModel<Cliente>("05X10 - Falha interna no servidor!");
            }
        }
        public async Task<ResultViewModel<Cliente>> EditarCliente(int clienteId, EditorClienteViewModel model)
        {

            try
            {
                var cliente = await _repositorioClientes.ObterClientePorIdAsync(clienteId);
                if (cliente == null)
                {
                    return new ResultViewModel<Cliente>("Cliente não encontrado!");
                }

                if (cliente.Status == ClienteStatus.Running)
                {
                    return new ResultViewModel<Cliente>("Não foi possível alterar o cliente, possui reserva em andamento");
                }

                cliente.NomeCompleto = model.NomeCompleto;
                cliente.Email = model.Email;
                cliente.RG = model.RG;
                cliente.CPF = model.CPF;

                await _repositorioClientes.AtualizarClienteAsync(cliente);

                return new ResultViewModel<Cliente>(cliente);
            }
            catch
            {
                return new ResultViewModel<Cliente>("05X11 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Cliente>> ExcluirCliente(int clienteId)
        {
            try
            {
                var cliente = await _repositorioClientes.ObterClientePorIdAsync(clienteId);
                if (cliente == null)
                {
                    return new ResultViewModel<Cliente>("Cliente não encontrado!");
                }

                if (cliente.Status == ClienteStatus.Running)
                {
                    return new ResultViewModel<Cliente>("Não foi possível excluir o cliente, possui reserva em andamento");
                }

                await _repositorioClientes.ExcluirClienteAsync(cliente);

                return new ResultViewModel<Cliente>(cliente);
            }
            catch
            {
                return new ResultViewModel<Cliente>("05X12 - Falha interna no servidor!");
            }
        }
    }
}
