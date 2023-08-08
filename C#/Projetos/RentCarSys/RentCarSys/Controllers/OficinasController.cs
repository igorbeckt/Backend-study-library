using Localdorateste.Data;
using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Localdorateste.Controllers
{

    [ApiController]
    [Route("/v1/oficina")]
    public class OficinasController : ControllerBase
    {
        //GET: PORT/v1/oficina/retiradas
        [HttpGet("retiradas")]
        public async Task<IActionResult> BuscarRetiradas(
        [FromServices] Contexto context)
        {
            try
            {
                var oficinas = await context.Retiradas.ToListAsync();
                return Ok(new ResultViewModel<List<RetiradaVeiculo>>(oficinas));
            }// Buscando todas as reservas no Banco 
            catch 
            {
                return StatusCode(500, value: new ResultViewModel<List<RetiradaVeiculo>>(erro: "05X05 - Falha interna no servidor!"));
            }// Tratando erro de servidor
        }

        //GET: PORT/v1/oficina/entregas
        [HttpGet("entregas")]
        public async Task<IActionResult> BuscarEntregas(
        [FromServices] Contexto context)
        {
            try
            {
                var oficinas = await context.Entregas.ToListAsync();
                return Ok(new ResultViewModel<List<EntregaVeiculo>>(oficinas));
            }// Buscando todas as reservas no Banco 
            catch 
            {
                return StatusCode(500, value: new ResultViewModel<List<EntregaVeiculo>>(erro: "05X05 - Falha interna no servidor!"));
            }// Tratando erro
        }

        // POST: PORT/v1/oficina/entregas
        [HttpPost("entregas")]
        public async Task<IActionResult> EntregaVeiculo(
        [FromBody] EditorOficinaViewModel model,
        [FromServices] Contexto context)


        {
            if (!ModelState.IsValid) 
                return BadRequest(error: new ResultViewModel<EntregaVeiculo>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var cliente = await context
                    .Clientes
                    .FirstOrDefaultAsync(x => x.ClienteId == model.ClienteId);
                if (cliente == null)
                    return NotFound(new ResultViewModel<EntregaVeiculo>(erro: "Cliente não encontrado, insira um cliente cadastrado!"));
                // Validação de Cliente

                var veiculo = await context
                  .Veiculos
                 .FirstOrDefaultAsync(x => x.VeiculoId == model.VeiculoId);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<EntregaVeiculo>(erro: "Veiculo não encontrado, insira um veiculo cadastrado!"));
                // Validação de Veiculo

                var reserva = await context
                        .Reservas
                        .FirstOrDefaultAsync(x => x.ReservaId == model.ReservaId);
                if (reserva == null)
                    return NotFound(new ResultViewModel<EntregaVeiculo>(erro: "Reserva não encontrada, insira uma reserva valida!"));
                // Validação de Reserva

                if (!reserva.StatusReserva.Equals("Em Andamento"))
                    return NotFound(new ResultViewModel<RetiradaVeiculo>(erro: "Você precisa criar o contrato da reserva para realizar a retirada."));
                // Validação do status do reserva para saber se o veiculo ja foi retirado


                cliente.StatusCliente = "Disponivel";
                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();
                // Alteração do status do cliente para livre

                veiculo.StatusVeiculo = "Disponivel";
                context.Veiculos.Update(veiculo);
                await context.SaveChangesAsync();
                // Alteração do status do veiculo para Disponivel

                reserva.StatusReserva = "Finalizado";
                context.Reservas.Update(reserva);
                await context.SaveChangesAsync();
                // Alteração do status da reserva para andamento.

                var entrega = new EntregaVeiculo

                {
                    Operacao = "Entrega",
                    ClienteId = cliente.ClienteId,                   
                    NomeCompleto = cliente.NomeCompleto,                    
                    VeiculoId = veiculo.VeiculoId,
                    Placa = veiculo.Placa,
                    Modelo = veiculo.Modelo,
                };

                await context.Entregas.AddAsync(entrega);
                await context.SaveChangesAsync();
                return Created(uri: $"v1/oficinas/{entrega.EntregaVeiculoId}", new ResultViewModel<EntregaVeiculo>(entrega));
                // Criação da EntregaVeiculo
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<EntregaVeiculo>(erro: "05XE8 - Não foi possível realizar a retirada!"));
            } // Tratando erros
            catch
            {
                return StatusCode(500, new ResultViewModel<EntregaVeiculo>(erro: "05X10 - Falha interna no servidor!"));
            } // Tratando erros
        } 

        // POST: PORT/v1/oficina/retiradas
        [HttpPost("retiradas")]
        public async Task<IActionResult> RetiradaVeiculo(
        [FromBody] EditorOficinaViewModel model,
        [FromServices] Contexto context)


        {
            if (!ModelState.IsValid) // Configuração de padronização de erro
                return BadRequest(error: new ResultViewModel<RetiradaVeiculo>(ModelState.PegarErros()));

            try
            {
                var cliente = await context
                    .Clientes
                    .FirstOrDefaultAsync(x => x.ClienteId == model.ClienteId);
                if (cliente == null)
                    return NotFound(new ResultViewModel<RetiradaVeiculo>(erro: "Cliente não encontrado, insira um cliente cadastrado!"));
                // Validação do cliente

                var veiculo = await context
                   .Veiculos
                   .FirstOrDefaultAsync(x => x.VeiculoId == model.VeiculoId);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<RetiradaVeiculo>(erro: "Veiculo não encontrado, insira um veiculo cadastrado!"));
                // Validação do veiculo

                var reserva = await context
                   .Reservas
                   .FirstOrDefaultAsync(x => x.ReservaId == model.ReservaId);
                if (reserva == null)
                    return NotFound(new ResultViewModel<RetiradaVeiculo>(erro: "Reserva não encontrada, insira uma reserva valida!"));
                // Validação da reserva


                if (!reserva.StatusReserva.Equals("Pago"))
                    return NotFound(new ResultViewModel<RetiradaVeiculo>(erro: "Reserva com status incorreto, informe uma reserva valida."));
                // Validação do status do reserva que não foi pago o contrato



                cliente.StatusCliente = "Ocupado";
                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();
                // Alteração do status do cliente após a retirada

                veiculo.StatusVeiculo = "Ocupado";
                context.Veiculos.Update(veiculo);
                await context.SaveChangesAsync();
                // Alteração do status do veiculos após a retirada

                reserva.StatusReserva = "Em Andamento";
                context.Reservas.Update(reserva);
                await context.SaveChangesAsync();
                // Alteração do status da reserva após a retirada

                var retirada = new RetiradaVeiculo

                {
                    Operacao = "Retirada",
                    ClienteId = cliente.ClienteId,
                    NomeCompleto = cliente.NomeCompleto,
                    VeiculoId = veiculo.VeiculoId,
                    Placa = veiculo.Placa,
                    Modelo = veiculo.Modelo,
                };

                await context.Retiradas.AddAsync(retirada);
                await context.SaveChangesAsync();                
                return Created(uri: $"v1/oficinas/{retirada.RetiradaVeiculoId}", new ResultViewModel<RetiradaVeiculo>(retirada));
                // Criação da RetiradaVeiculo

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<RetiradaVeiculo>(erro: "05XE8 - Não foi possível criar a reserva!"));
            } // Tratando erros
            catch
            {
                return StatusCode(500, new ResultViewModel<RetiradaVeiculo>(erro: "05X10 - Falha interna no servidor!"));
            } // Tratando erros            
        }


    }
}
