using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarSys.Application.Interfaces;
using RentCarSys.Application.Services;
using RentCarSys.Enums;

namespace RentCarSys.Application.Controllers
{
    [ApiController]
    [Route("/v1/reservas")]
    public class ReservaController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly VeiculoService _veiculoService;
        private readonly ReservaService _reservaService;

        public ReservaController(ClienteService clienteService,
                                 VeiculoService veiculo,
                                 ReservaService reservaService)
        {
            _clienteService = clienteService;
            _veiculoService = veiculo;
            _reservaService = reservaService;
        }

        [HttpGet("buscarTodas")]
        public async Task<IActionResult> BuscarReserva()
        {
            var result = await _reservaService.BuscarTodasReservas();
            if (result.Erros.Count > 0)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpGet("buscarPorId/{reservaid:int}")]
        public async Task<IActionResult> BuscarReservasId(
        [FromRoute] int reservaid)
        {
            var result = await _reservaService.BuscarReservaPorId(reservaid);
            if (result.Erros.Count > 0)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }        

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CriarReservas(
        [FromBody] EditorReservaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Reserva>(ModelState.PegarErros()));
            }

            var result = await _reservaService.CriarReserva(model);

            if (result.Erros.Count > 0)
            {
                return StatusCode(500, result);
            }

            return Created($"reservas/{result.Dados.Id}", result);
        }

        [HttpPut("alterar/{reservaid:int}")]
        public async Task<IActionResult> EditarClientes(
        [FromRoute] int reservaid,
        [FromBody] EditorReservaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Reserva>(ModelState.PegarErros()));
            }


            var result = await _reservaService.EditarReserva(reservaid, model);

            if (result.Erros.Count > 0)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpDelete("excluir/{reservaid:int}")]
        public async Task<IActionResult> ExcluirClientes(
        [FromRoute] int reservaid)
        {
            var result = await _reservaService.ExcluirReserva(reservaid);

            if (result.Erros.Count > 0)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }
}
