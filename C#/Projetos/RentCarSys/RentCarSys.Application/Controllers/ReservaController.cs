using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarSys.Application.Interfaces;
using RentCarSys.Enums;

namespace RentCarSys.Application.Controllers
{
    [ApiController]
    [Route("/v1/reservas")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly IVeiculosRepository _veiculosRepository;

        public ReservaController(IReservasRepository reservasRepository, IClientesRepository clientesRepository, IVeiculosRepository veiculosRepository)
        {
            _reservasRepository = reservasRepository;
            _clientesRepository = clientesRepository;
            _veiculosRepository = veiculosRepository;
        }

        [HttpGet("buscarTodos")]
        public async Task<IActionResult> BuscarReservas()
        {
            try
            {
                var reservas = await _reservasRepository.ObterTodasReservasAsync();
                return Ok(new ResultViewModel<List<Reserva>>(reservas));
            }// Buscando todas as reservas no Banco 
            catch
            {
                return StatusCode(500, value: new ResultViewModel<List<Reserva>>(erro: "05X05 - Falha interna no servidor!"));
            }// Tratando erro
        }


        [HttpGet("buscarPorId/{reservaid}")]
        public async Task<IActionResult> BuscarReservaPorId(
        [FromRoute] int reservaid)
        {
            try
            {
                var reserva = await _reservasRepository.ObterReservaPorIdAsync(reservaid);
                if (reserva == null)
                    return NotFound(new ResultViewModel<Reserva>(erro: "A reserva não foi encontrada, verifique se a reserva já foi cadastrada!"));
                // Validação de ID

                return Ok(new ResultViewModel<Reserva>(reserva));
                // Buscando a reserva no banco
            }
            catch
            {
                return StatusCode(500, value: new ResultViewModel<Reserva>(erro: "Falha interna no servidor!"));
            }// Tratando erro

        }


        [HttpPost("cadastrar")]
        public async Task<IActionResult> CriarReserva(
        [FromBody] EditorReservaViewModel model)


        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Reserva>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var cliente = await _clientesRepository.ObterClientePorIdAsync(model.ClienteId);
                if (cliente == null)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Cliente não encontrado, insira um cliente cadastrado!"));
                // Validação do cliente

                var veiculo = await _veiculosRepository.ObterVeiculoPorIdAsync(model.VeiculoId);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Veiculo não encontrado, insira um veiculo cadastrado!"));
                // Validação do veiculo

                if (cliente.Status == ClienteStatus.Running)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar o cliente,possui reserva em andamento"));
                // Validação para saber se o cliente não tem uma reserva em andamento

                if (veiculo.Status == VeiculoStatus.Running)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar o veiculo, possui reserva em andamento"));
                // Validação para saber se o veiculo já foi alugado


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

                await _reservasRepository.AdicionarReservaAsync(reserva);

                return Created(uri: $"v1/reservas/{reserva.Id}", new ResultViewModel<Reserva>(reserva));
                // Criação da reserva               
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Reserva>(erro: "05XE8 - Não foi possível criar a reserva!"));
            }// Tratando erro
            catch
            {
                return StatusCode(500, new ResultViewModel<Reserva>(erro: "05X10 - Falha interna no servidor!"));
            }// Tratando Erro
        }


        [HttpPut("alterar/{reservaid}")]
        public async Task<IActionResult> EditarReservas(
        [FromRoute] int reservaid,
        [FromBody] EditorReservaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Reserva>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var reserva = await _reservasRepository.ObterReservaPorIdAsync(reservaid);
                if (reserva == null)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Reserva não encontrada!"));
                // Validação da reserva

                /*if (reserva.Status == ReservaStatus.Running)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, possui um contrato em aberto!"));
                // Validação para saber se a reserva está contrato em aberto*/

                if (reserva.Status == ReservaStatus.Running)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, o veiculo já foi retirado e a reserva está em andamento!"));
                // Validação para saber se a reserva está andamento

                if (reserva.Status == ReservaStatus.Offline)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não é possivel alterar uma reserva finalizada!"));
                // Validação para saber se a reserva foi finalizada*/

                reserva.DataRetirada = model.DataRetirada;
                reserva.DataEntrega = model.DataEntrega;
                reserva.ValorLocacao = model.ValorLocacao;

                await _reservasRepository.AtualizarReservaAsync(reserva);


                return Ok(new ResultViewModel<Reserva>(reserva));
                // Alteração da Reserva
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Reserva>(erro: "05XE8 - Não foi possível alterar a reserva!"));
            }// Tratando erros
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Reserva>(erro: "05X11 - Falha interna no servidor!"));
            }// Tratando erros

        }



        [HttpDelete("excluir/{reservaid}")]
        public async Task<IActionResult> ExcluirReservas(
        [FromRoute] int reservaid)
        {
            try
            {
                var reserva = await _reservasRepository.ObterReservaPorIdAsync(reservaid);
                if (reserva == null)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Reserva não encontrada, verifique se reserva foi cadastrada!"));
                // Validação da reserva

                /*if (reserva.StatusReserva == "Pago")
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, possui um contrato em aberto!"));
                // Validação para saber se a reserva está contrato em aberto*/

                if (reserva.Status == ReservaStatus.Running)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, o veiculo já foi retirado e a reserva está em andamento!"));
                // Validação para saber se a reserva está andamento

                if (reserva.Status == ReservaStatus.Offline)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não é possivel alterar uma reserva finalizada!"));
                // Validação para saber se a reserva foi finalizada

                await _reservasRepository.ExcluirReservaAsync(reserva);

                return Ok(new ResultViewModel<Reserva>(reserva));
                // Remoção da reserva
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Reserva>(erro: "05XE7 - Não foi possível excluir a reserva!"));
            }// Tratando erro
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Reserva>(erro: "05X12 - Falha interna no servidor!"));
            }// Tratando erro
        }
    }
}
