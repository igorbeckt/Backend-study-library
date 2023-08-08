using Localdorateste.Data;
using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Localdorateste.Controllers
{

    [ApiController]
    [Route("/v1/veiculos")]
    public class VeiculosController : ControllerBase
    {
        // GET: v1/veiculos
        [HttpGet]
        public async Task<IActionResult> BuscarVeiculos(
        [FromServices] Contexto context)
        {
            try
            {
                var veiculos = await context.Veiculos.ToListAsync();
                return Ok(new ResultViewModel<List<Veiculo>>(veiculos));
            }// Buscando todos os veiculos
            catch
            {
                return StatusCode(500, value: new ResultViewModel<List<Veiculo>>(erro: "05X05 - Falha interna no servidor!"));
            }// Tratando erros

        }


        // GET: v1/veiculos/1
        [HttpGet("{veiculoid:int}")] 
        public async Task<IActionResult> BuscarVeiculosId(
        [FromRoute] int veiculoid,
        [FromServices] Contexto context)
        {
            try
            {
                var veiculo = await context
                     .Veiculos
                     .FirstOrDefaultAsync(x => x.VeiculoId == veiculoid);
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


        // GET: v1/veiculos/XXX1234
        [HttpGet("{placa}")]
        public async Task<IActionResult> BuscarVeiculosCPF(
        [FromRoute] string placa,
        [FromServices] Contexto context)
        {
            try
            {
                var veiculo = await context
                    .Veiculos
                    .FirstOrDefaultAsync(x => x.Placa == placa);
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

        // POST: v1/veiculos
        [HttpPost] 
        public async Task<IActionResult> CriarVeiculos(
        [FromBody] EditorVeiculoViewModel model,
        [FromServices] Contexto context)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Veiculo>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var veiculo = new Veiculo
                {
                    StatusVeiculo = "Disponivel",
                    Placa = model.Placa,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    AnoFabricacao = model.AnoFabricacao,
                    KM = model.KM,
                    QuantidadePortas = model.QuantidadePortas,
                    Cor = model.Cor,
                    Condicao = model.Condicao,
                    VidroEletrico = model.VidroEletrico,
                    TravaEletrica = model.TravaEletrica,
                    Automatico = model.Automatico,
                    ArCondicionado = model.ArCondicionado,
                    DirecaoHidraulica = model.DirecaoHidraulica,                    
                };                   

                await context.Veiculos.AddAsync(veiculo);
                await context.SaveChangesAsync();

                return Created(uri: $"v1/veiculos/{veiculo.VeiculoId}", new ResultViewModel<Veiculo>(veiculo));
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

        // PUT: v1/veiculos/1
        [HttpPut("{veiculoid:int}")]
        public async Task<IActionResult> EditarVeiculos(
        [FromRoute] int veiculoid,
        [FromBody] EditorVeiculoViewModel model,
        [FromServices] Contexto context)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Veiculo>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var veiculo= await context
                    .Veiculos
                    .FirstOrDefaultAsync(x => x.VeiculoId == veiculoid);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Veiculo não encontrado!"));
                // Validação do veiculo

                if (veiculo.StatusVeiculo == "Ocupado")
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Não foi possivel alterar o veiculo, possui reserva em andamento"));
                // Validação para saber se o veiculo já foi alugado

                veiculo.Placa = model.Placa;
                veiculo.Marca = model.Marca;
                veiculo.Modelo = model.Modelo;
                veiculo.AnoFabricacao = model.AnoFabricacao;
                veiculo.KM = model.KM;
                veiculo.QuantidadePortas = model.QuantidadePortas;
                veiculo.Cor = model.Cor;
                veiculo.Condicao = model.Condicao;
                veiculo.VidroEletrico = model.VidroEletrico;
                veiculo.TravaEletrica = model.TravaEletrica;
                veiculo.Automatico = model.Automatico;
                veiculo.ArCondicionado = model.ArCondicionado;
                veiculo.DirecaoHidraulica = model.DirecaoHidraulica;

                context.Veiculos.Update(veiculo);
                await context.SaveChangesAsync();

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

        // DELETE: v1/veiculos/1
        [HttpDelete("{veiculoid:int}")]
        public async Task<IActionResult> ExcluirClientes(
        [FromRoute] int veiculoid,
        [FromServices] Contexto context)
        {
            try
            {
                var veiculo = await context
                    .Veiculos
                    .FirstOrDefaultAsync(x => x.VeiculoId == veiculoid);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Veiculo não encontrado!"));
                // Validação de veiculo

                if (veiculo.StatusVeiculo == "Ocupado")
                    return NotFound(new ResultViewModel<Veiculo>(erro: "Não foi possivel excluir o veiculo, o veiculo está alugado!"));
                // Validação para saber se o veiculo está disponivel.

                context.Veiculos.Remove(veiculo);
                await context.SaveChangesAsync();

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
