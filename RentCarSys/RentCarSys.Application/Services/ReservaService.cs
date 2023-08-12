using Localdorateste.Data;
using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarSys.Application.DTO;
using RentCarSys.Application.Interfaces;
using RentCarSys.Enums;

namespace RentCarSys.Application.Services
{
    public class ReservaService
    {
        private readonly IClientesRepository _repositorioClientes;
        private readonly IVeiculosRepository _repositorioVeiculos;
        private readonly IReservasRepository _repositorioReservas;

        public ReservaService(IClientesRepository repositorioClientes, 
                              IVeiculosRepository repositorioVeiculos, 
                              IReservasRepository repositorioReservas)
        {
            _repositorioClientes = repositorioClientes;
            _repositorioVeiculos = repositorioVeiculos;
            _repositorioReservas = repositorioReservas;
        }

        public async Task<ResultViewModel<List<Reserva>>> BuscarTodasReservas()
        {
            try
            {
                var reservas = await _repositorioReservas.ObterTodasReservasAsync();
                return new ResultViewModel<List<Reserva>>(reservas);
            }
            catch
            {
                return new ResultViewModel<List<Reserva>>(erro: "05X05 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Reserva>> BuscarReservaPorId(int reservaId)
        {
            try
            {
                var reserva = await _repositorioReservas.ObterReservaPorIdAsync(reservaId);
                if (reserva == null)
                {
                    return new ResultViewModel<Reserva>(erro: "Reserva não encontrada, verifique se a reserva já foi cadastrada!");
                }

                return new ResultViewModel<Reserva>(reserva);
            }
            catch
            {
                return new ResultViewModel<Reserva>(erro: "Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Reserva>> CriarReserva(EditorReservaViewModel model)
        {
            try
            {
                var cliente = await _repositorioClientes.ObterClientePorIdAsync(model.ClienteId);
                if (cliente == null)
                {
                    return new ResultViewModel<Reserva>(erro: "Cliente não encontrado, verifique se o cliente já foi cadastrado!");
                }

                var veiculo = await _repositorioVeiculos.ObterVeiculoPorIdAsync(model.VeiculoId);
                if (veiculo == null)
                {
                    return new ResultViewModel<Reserva>(erro: "Veiculo não encontrado, verifique se o veiculo já foi cadastrado!");
                }

                if (cliente.Status == ClienteStatus.Running)
                {
                    return new ResultViewModel<Reserva>("Não foi possível alterar o cliente, possui reserva em andamento");
                }

                if (veiculo.Status == VeiculoStatus.Running)
                {
                    return new ResultViewModel<Reserva>("Não foi possível alterar o veiculo, possui reserva em andamento");
                }

                var reserva = new Reserva

                {
                    Status = ReservaStatus.Online,
                    DataReserva = model.DataReserva,
                    ValorLocacao = model.ValorLocacao,
                    DataRetirada = model.DataRetirada,
                    DataEntrega = model.DataRetirada,
                    Cliente = new List<Cliente> { cliente },
                    Veiculo = new List<Veiculo> { veiculo }
                };

                await _repositorioReservas.AdicionarReservaAsync(reserva);

                return new ResultViewModel<Reserva>(reserva);

            }
            catch (DbUpdateException ex)
            {
                return new ResultViewModel<Reserva>(erro: "05XE8 - Não foi possível criar a reserva!");
            }
            catch
            {
                return new ResultViewModel<Reserva>(erro: "05X10 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Reserva>> EditarReserva(int reservaId, EditorReservaViewModel model)
        {

            try
            {
                var reserva = await _repositorioReservas.ObterReservaPorIdAsync(reservaId);
                if (reserva == null)
                {
                    return new ResultViewModel<Reserva>("Cliente não encontrado!");
                }

                if (reserva.Status == ReservaStatus.Running)
                {
                    return new ResultViewModel<Reserva>("Não foi possivel alterar a reserva, o veiculo já foi retirado e a reserva está em andamento!");
                }

                if (reserva.Status == ReservaStatus.Offline)
                {
                    return new ResultViewModel<Reserva>("Não é possivel alterar uma reserva finalizada!");
                }

                reserva.DataRetirada = model.DataRetirada;
                reserva.DataEntrega = model.DataEntrega;
                reserva.ValorLocacao = model.ValorLocacao;

                await _repositorioReservas.AtualizarReservaAsync(reserva);

                return new ResultViewModel<Reserva>(reserva);
            }
            catch (DbUpdateException ex)
            {
                return new ResultViewModel<Reserva>(erro: "05XE8 - Não foi possível alterar a reserva!");
            }
            catch (Exception ex)
            {
                return new ResultViewModel<Reserva>("05X11 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Reserva>> ExcluirReserva(int reservaId)
        {
            try
            {
                var reserva = await _repositorioReservas.ObterReservaPorIdAsync(reservaId);
                if (reserva == null)
                {
                    return new ResultViewModel<Reserva>("Reserva não encontrada!");
                }

                if (reserva.Status == ReservaStatus.Running)
                {
                    return new ResultViewModel<Reserva>("Não foi possível excluir a reserva, possui reserva em andamento");
                }

                if (reserva.Status == ReservaStatus.Offline)
                {
                    return new ResultViewModel<Reserva>("Não é possivel alterar uma reserva finalizada!");
                }

                await _repositorioReservas.ExcluirReservaAsync(reserva);

                return new ResultViewModel<Reserva>(reserva);
            }
            catch
            {
                return new ResultViewModel<Reserva>("05X12 - Falha interna no servidor!");
            }
        }
    }
}
