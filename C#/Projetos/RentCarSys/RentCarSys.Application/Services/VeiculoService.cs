using Localdorateste.Data;
using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarSys.Application.Interfaces;
using RentCarSys.Enums;

namespace RentCarSys.Application.Services
{
    [ApiController]
    [Route("/veiculos")]
    public class VeiculoService : ControllerBase
    {
        private readonly IVeiculosRepository _veiculosRepository;

        public VeiculoService(IVeiculosRepository veiculosRepository) 
        {
            _veiculosRepository = veiculosRepository;
        }

        [HttpGet("buscarTodos")]
        public async Task<IActionResult> BuscarVeiculos()
        {
            try
            {
                var veiculos = await _veiculosRepository.ObterTodosVeiculosAsync();
                return Ok(new ResultViewModel<List<Veiculo>>(veiculos));
            }// Buscando todos os veiculos
            catch
            {
                return StatusCode(500, value: new ResultViewModel<List<Veiculo>>(erro: "05X05 - Falha interna no servidor!"));
            }// Tratando erros

        }
        
        [HttpGet("buscarPorId/{veiculoid:int}")]
        public async Task<IActionResult> BuscarVeiculoPorId(
        [FromRoute] int veiculoid)
        {
            try
            {
                var veiculo = await _veiculosRepository.ObterVeiculoPorIdAsync(veiculoid);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Veiculo não encontrado, verifique se o veiculo já foi cadastrado!"));
                // Validação do cliente

                return Ok(new ResultViewModel<Veiculo>(veiculo));
            }// Buscando cliente no bancos
            catch
            {
                return StatusCode(500, value: new ResultViewModel<Veiculo>(erro: "Falha interna no servidor!"));
            }// Tratando erros

        }

        [HttpGet("buscarPorPlaca/{placa}")]
        public async Task<IActionResult> BuscarVeiculoPorPlaca(
        [FromRoute] string placa)
        {
            try
            {
                var veiculo = await _veiculosRepository.ObterVeiculoPorPlacaAsync(placa);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Veiculo não encontrado, verifique se a placa está correta!"));
                // Validação do veiculo

                return Ok(new ResultViewModel<Veiculo>(veiculo));
            }// Buscando o veiculo no banco
            catch
            {
                return StatusCode(500, value: new ResultViewModel<Veiculo>(erro: "Falha interna no servidor!"));
            }// tratanto erros

        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CriarVeiculo(
        [FromBody] EditorVeiculoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Veiculo>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var veiculo = new Veiculo
                {
                    Status = VeiculoStatus.Online,
                    Placa = model.Placa,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    AnoFabricacao = model.AnoFabricacao,
                    KM = model.KM,
                    QuantidadePortas = model.QuantidadePortas,
                    Cor = model.Cor,
                    Automatico = model.Automatico
                };

                await _veiculosRepository.AdicionarVeiculoAsync(veiculo);                

                return Created(uri: $"v1/veiculos/{veiculo.Id}", new ResultViewModel<Veiculo>(veiculo));
            }// Criação do veiculo
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Veiculo>(erro: "05XE8 - Não foi possível criar o veiculo!"));
            }// Tratando erros
            catch
            {
                return StatusCode(500, new ResultViewModel<Veiculo>(erro: "05X10 - Falha interna no servidor!"));
            }// Tratando erros


        }

        [HttpPut("alterar/{veiculoid:int}")]
        public async Task<IActionResult> EditarVeiculo(
        [FromRoute] int veiculoid,
        [FromBody] EditorVeiculoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Veiculo>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var veiculo = await _veiculosRepository.ObterVeiculoPorIdAsync(veiculoid);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Veiculo não encontrado!"));
                // Validação do veiculo

                if (veiculo.Status == VeiculoStatus.Running)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Não foi possivel alterar o veiculo, possui reserva em andamento"));
                // Validação para saber se o veiculo já foi alugado

                veiculo.Placa = model.Placa;
                veiculo.Marca = model.Marca;
                veiculo.Modelo = model.Modelo;
                veiculo.AnoFabricacao = model.AnoFabricacao;
                veiculo.KM = model.KM;
                veiculo.QuantidadePortas = model.QuantidadePortas;
                veiculo.Cor = model.Cor;
                veiculo.Automatico = model.Automatico;


                await _veiculosRepository.AtualizarVeiculoAsync(veiculo);                

                return Ok(new ResultViewModel<Veiculo>(veiculo));
                // Alteração do veiculo
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Veiculo>(erro: "05XE8 - Não foi possível alterar o veiculo!"));
            }// Tratando erros
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Veiculo>(erro: "05X11 - Falha interna no servidor!"));
            }// Tratando erros

        }
                
        [HttpDelete("excluir/{veiculoid:int}")]
        public async Task<IActionResult> ExcluirVeiculo(
        [FromRoute] int veiculoid)
        {
            try
            {
                var veiculo = await _veiculosRepository.ObterVeiculoPorIdAsync(veiculoid);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Veiculo não encontrado!"));
                // Validação de veiculo

                if (veiculo.Status == VeiculoStatus.Offline)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Não foi possivel excluir o veiculo, o veiculo está alugado!"));
                // Validação para saber se o veiculo está disponivel.

                await _veiculosRepository.ExcluirVeiculoAsync(veiculo);                

                return Ok(new ResultViewModel<Veiculo>(veiculo));
                // Remoção do veiculo
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Veiculo>(erro: "05XE7 - Não foi possível excluir o veiculo!"));
            }// Tratando erros
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Veiculo>(erro: "05X12 - Falha interna no servidor!"));
            }// Tratando erros
        }
    }
}
