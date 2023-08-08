using Localdorateste.Data;
using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RentCarSys.Enums;
using System.Runtime.ConstrainedExecution;

namespace Localdorateste.Controllers
{

    [ApiController]
    [Route("/v1/reservas")]
    public class ReservasController : ControllerBase
    {
        
        // GET v1/reservas
        [HttpGet] 
        public async Task<IActionResult> BuscarReservas(
        [FromServices] Contexto context)
        {
            try
            {
                var reservas = await context.Reservas.ToListAsync(); 
                return Ok(new ResultViewModel<List<Reserva>>(reservas));
            }// Buscando todas as reservas no Banco 
            catch 
            {
                return StatusCode(500, value: new ResultViewModel<List<Reserva>>(erro: "05X05 - Falha interna no servidor!"));
            }// Tratando erro
        }
                
        // GET: /v1/reservas/1
        [HttpGet("{reservaid}")] 
        public async Task<IActionResult> BuscarReservasId(
        [FromRoute] int reservaid,
        [FromServices] Contexto context)
        {
            try
            {
                var reserva = await context
                     .Reservas
                     .FirstOrDefaultAsync(x => x.ReservaId == reservaid); 
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

        // POST v1/reservas
        [HttpPost]
        public async Task<IActionResult> CriarReservas(
        [FromBody] EditorReservaViewModel model,
        [FromServices] Contexto context)
        
        
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Reserva>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var cliente = await context
                    .Clientes
                    .FirstOrDefaultAsync(x => x.ClienteId == model.ClienteId);
                if (cliente == null)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Cliente não encontrado, insira um cliente cadastrado!"));
                // Validação do cliente
                
                    var veiculo = await context
                      .Veiculos
                     .FirstOrDefaultAsync(x => x.VeiculoId == model.VeiculoId);
                if (veiculo == null)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Veiculo não encontrado, insira um veiculo cadastrado!"));
                // Validação do veiculo

                if (cliente.Status == ClienteStatus.Offline)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar o cliente,possui reserva em andamento"));
                // Validação para saber se o cliente não tem uma reserva em andamento

                if (cliente.Status == ClienteStatus.Offline)
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar o veiculo, possui reserva em andamento"));
                // Validação para saber se o veiculo já foi alugado


                var reserva = new Reserva                
                
                    {
                        Status = ReservaStatus.Online,
                        ClienteId = cliente.ClienteId,
                        NomeCompleto = cliente.NomeCompleto,
                        CPF = cliente.CPF,
                        VeiculoId = veiculo.VeiculoId,
                        Placa = veiculo.Placa,
                        Marca = veiculo.Marca,
                        Modelo = veiculo.Modelo, 
                        DataReserva = model.DataReserva,
                        ValorLocacao = model.ValorLocacao,
                        DataRetirada = model.DataRetirada,
                        DataEntrega = model.DataRetirada,                                                  
                    };                    

                    await context.Reservas.AddAsync(reserva);
                    await context.SaveChangesAsync();
                    return Created(uri: $"v1/reservas/{reserva.ReservaId}", new ResultViewModel<Reserva>(reserva));
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

        // PUT: v1/reservas/1
        [HttpPut("{reservaid}")]  
        public async Task<IActionResult> EditarReservas(
        [FromRoute] int reservaid,
        [FromBody] EditorReservaViewModel model,
        [FromServices] Contexto context)
        {
            if (!ModelState.IsValid) 
                return BadRequest(error: new ResultViewModel<Reserva>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var reserva = await context
                    .Reservas
                    .FirstOrDefaultAsync(x => x.ReservaId == reservaid); 
                if (reserva == null) 
                    return NotFound(new ResultViewModel<Reserva>(erro: "Reserva não encontrada!"));
                // Validação da reserva

                /*if (reserva.StatusReserva == "Feita")
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, possui um contrato em aberto!"));
                // Validação para saber se a reserva está contrato em aberto

                if (reserva.StatusReserva == "Em Andamento")
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, possui reserva em andamento!"));
                // Validação para saber se a reserva está andamento

                if (reserva.StatusReserva == "Finalizado")
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não é possivel alterar uma reserva finalizada!"));
                // Validação para saber se a reserva foi finalizada*/


                reserva.DataRetirada = model.DataRetirada;
                reserva.DataEntrega = model.DataEntrega;     
                reserva.ValorLocacao = model.ValorLocacao;
                                
                context.Reservas.Update(reserva);
                await context.SaveChangesAsync();                
                
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

         
        // DELETE: v1/reservas/1
        [HttpDelete("{reservaid}")] 
        public async Task<IActionResult> ExcluirReservas(
        [FromRoute] int reservaid,
        [FromServices] Contexto context)
        {
            try
            {
                var reserva = await context
                    .Reservas
                    .FirstOrDefaultAsync(x => x.ReservaId == reservaid);
                if (reserva == null) 
                    return NotFound(new ResultViewModel<Reserva>(erro: "Reserva não encontrada, verifique se reserva foi cadastrada!"));
                // Validação da reserva

                /*if (reserva.StatusReserva == "Pago")
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, possui um contrato em aberto!"));
                // Validação para saber se a reserva está contrato em aberto

                if (reserva.StatusReserva == "Em Andamento")
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não foi possivel alterar a reserva, possui reserva em andamento!"));
                // Validação para saber se a reserva está andamento

                if (reserva.StatusReserva == "Finalizado")
                    return NotFound(new ResultViewModel<Reserva>(erro: "Não é possivel alterar uma reserva finalizada!"));
                // Validação para saber se a reserva foi finalizada*/

                context.Reservas.Remove(reserva);
                await context.SaveChangesAsync(); 
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
