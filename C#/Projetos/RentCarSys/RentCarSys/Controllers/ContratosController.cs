using Localdorateste.Data;
using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Localdorateste.Controllers
{
    [ApiController]
    [Route("/v1/contratos")]
    public class ContratosController : ControllerBase
    {

        //GET v1/contratos
        [HttpGet] 
        public async Task<IActionResult> BuscarContratos(
        [FromServices] Contexto context)
        {
            try
            {
                var contratos = await context.Contratos.ToListAsync();
                return Ok(new ResultViewModel<List<Contrato>>(contratos));
            }// Buscando todos os contratos no Banco
            catch
            {
                return StatusCode(500, value: new ResultViewModel<List<Contrato>>(erro: "05X05 - Falha interna no servidor!"));
            }// Tratanto erros
        }


        //GET: v1/contratos/1
        [HttpGet("imprimircontrato/{contratoid}")]
        public async Task<IActionResult> ImprimirContratosId(
        [FromRoute] int contratoid,
        [FromServices] Contexto context)
        {
            try
            {
                var contrato = await context
                     .Contratos
                     .FirstOrDefaultAsync(x => x.ContratoId == contratoid);
                if (contrato == null)
                    return NotFound(new ResultViewModel<Contrato>(erro: "Contrato não encontrado, verifique se o contrato já foi cadastrado!"));
                // Validação do contrato

                return Ok(new ResultViewModel<Contrato>(contrato.TextoContrato()));
                // Buscando o contrato no banco
            }
            catch
            {
                return StatusCode(500, value: new ResultViewModel<Contrato>(erro: "Falha interna no servidor!"));
            }// Tratando erros

        }

        //POST: v1/contratos
        [HttpPost]
        public async Task<IActionResult> CriarContratos(
        [FromBody] EditorContratoViewModel model,
        [FromServices] Contexto context)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Contrato>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var reserva = await context
                    .Reservas
                    .FirstOrDefaultAsync(x => x.ReservaId == model.ReservaId);
                if (reserva == null)
                    return NotFound(new ResultViewModel<Contrato>(erro: "Reserva não encontrada, insira uma reserva cadastrada!"));
                // Validação da Reserva

                if (reserva.StatusReserva == "Pago")
                    return NotFound(new ResultViewModel<Contrato>(erro: "Não foi possivel criar o contrato, a reserva já tem um contrato feito!"));
                // Validação para saber se a reserva está contrato em aberto

                if (reserva.StatusReserva == "Em Andamento")
                    return NotFound(new ResultViewModel<Contrato>(erro: "Não foi possivel criar o contrato, a reserva está em andamento!"));
                // Validação para saber se a reserva está andamento

                if (reserva.StatusReserva == "Finalizado")
                    return NotFound(new ResultViewModel<Contrato>(erro: "Não é possivel criar o contrato, a reserva já foi finalizada!"));
                // Validação para saber se a reserva foi finalizada

                reserva.StatusReserva = "Pago";
                context.Reservas.Update(reserva);
                await context.SaveChangesAsync();
                // Alteração do status da reserva após o pagamento do contrato

                var contrato = new Contrato
                {
                    StatusReserva = reserva.StatusReserva,
                    ReservaId = reserva.ReservaId,
                    NomeCompleto = reserva.NomeCompleto,                     
                    ClienteId = reserva.ClienteId,
                    CPF = reserva.CPF,
                    Modelo = reserva.Modelo,
                    Marca = reserva.Marca,
                    Placa = reserva.Placa,
                    VeiculoId = reserva.VeiculoId,
                    DataReserva = reserva.DataReserva,
                    DataRetirada = reserva.DataRetirada,                    
                    DataEntrega = reserva.DataRetirada,
                    ValorLocacao = reserva.ValorLocacao, 
                    FormaPagamento = model.FormaPagamento,
                    Parcelas = model.Parcelas                      
                };

                if (contrato.FormaPagamento != "dinheiro")
                if (contrato.FormaPagamento != "boleto")
                if (contrato.FormaPagamento != "cartao")
                if (contrato.FormaPagamento != "pix")
                   return NotFound(new ResultViewModel<Contrato>(erro: "Forma de pagamento incorreta! Digite: 'dinheiro' para pagamento no dinheiro. Digite: 'boleto' para pagamento no boleto. Digite: 'pix' para pagamaento no pix. Digite: 'cartao' para pagamento no cartão de crédito"));
                // Validação da forma de pagamento

                await context.Contratos.AddAsync(contrato);
                await context.SaveChangesAsync();

                return Created(uri: $"v1/contratos/{contrato.ContratoId}", new ResultViewModel<Contrato>(contrato.TextoContrato()));
                // Criação do contrato
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Contrato>(erro: "05XE8 - Não foi possível criar o contrato!"));
            }// Tratando erros
            catch
            {
                return StatusCode(500, new ResultViewModel<Contrato>(erro: "05X10 - Falha interna no servidor!"));
            }//Tratando erros
        }

        //PUT: PORT/v1/contratos/1
        [HttpPut("{contratoid}-{reservaid}")]
        public async Task<IActionResult> EditarContratos(
        [FromRoute] int contratoid, int reservaid,
        [FromBody] EditorContratoViewModel model,
        [FromServices] Contexto context)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Contrato>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var contrato = await context
                     .Contratos
                     .FirstOrDefaultAsync(x => x.ContratoId == contratoid);
                if (contrato == null)
                    return NotFound(new ResultViewModel<Contrato>(erro: "Contrato não encontrado, verifique se o contrato foi criado!"));
                //Validação do contrato

                var reserva = await context
                     .Reservas
                     .FirstOrDefaultAsync(x => x.ReservaId == reservaid);
                if (reserva == null)
                    return NotFound(new ResultViewModel<Contrato>(erro: "Reserva não encontrada, verifique se a reserva foi criada!"));
                // Validação da reserva

                if (reserva.StatusReserva == "Livre")
                    return NotFound(new ResultViewModel<Contrato>(erro: "Reserva livre, insira uma reserva 'Feita'!"));
                // Validação para saber se o contrato está contrato em aberto

                if (reserva.StatusReserva == "Em Andamento")
                    return NotFound(new ResultViewModel<Contrato>(erro: "Reserva em andamento, insira uma reserva 'Feita'!"));
                // Validação para saber se o contrato está andamento

                if (reserva.StatusReserva == "Finalizado")
                    return NotFound(new ResultViewModel<Contrato>(erro: "Reserva finalizada, insira uma reserva 'Feita'"));
                // Validação para saber se o contrato foi finalizada

                contrato.ReservaId = model.ReservaId;
                contrato.FormaPagamento = model.FormaPagamento;
                contrato.Parcelas = model.Parcelas;

                if (contrato.FormaPagamento != "dinheiro")
                if (contrato.FormaPagamento != "boleto")
                if (contrato.FormaPagamento != "cartao")
                if (contrato.FormaPagamento != "pix")
                                return NotFound(new ResultViewModel<Contrato>(erro: "Forma de pagamento incorreta! Digite: 'dinheiro' para pagamento no dinheiro. Digite: 'boleto' para pagamento no boleto. Digite: 'pix' para pagamaento no pix. Digite: 'cartao' para pagamento no cartão de crédito"));
                // Validação da forma de pagamento

                context.Contratos.Update(contrato);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Contrato>(contrato));
                // Alteração da Reserva
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Contrato>(erro: "05XE8 - Não foi possível alterar o contrato!"));
            }// Tratando erros
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Contrato>(erro: "05X11 - Falha interna no servidor!"));
            }// Tratando erros

        }

        //DELETE: PORT/v1/contratos/1
        [HttpDelete("{contratoid}")]
        public async Task<IActionResult> ExcluirContratos(
        [FromRoute] int contratoid, int reservaid,
        [FromServices] Contexto context)
        {

            try
            {
                var contrato = await context
                     .Contratos
                     .FirstOrDefaultAsync(x => x.ContratoId == contratoid);
                if (contrato == null)
                    return NotFound(new ResultViewModel<Contrato>(erro: "Contrato não encontrado, Verifique se o contrato foi criado!"));
                // Validação Contrato

                var reserva = await context
                     .Reservas
                     .FirstOrDefaultAsync(x => x.ReservaId == reservaid);
                if (contrato == null)
                    return NotFound(new ResultViewModel<Contrato>(erro: "Contrato não encontrado, Verifique se o contrato foi criado!"));
                // Validação Reserva

                reserva.StatusReserva = "Livre";
                context.Reservas.Update(reserva);
                await context.SaveChangesAsync();
                // Alteração do status da reserva após a retirada

                context.Contratos.Remove(contrato);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Contrato>(contrato));
                // Remoção do contrato
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Contrato>(erro: "05XE7 - Não foi possível excluir o contrato!"));
            }// Tratando erro
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Contrato>(erro: "05X12 - Falha interna no servidor!"));
            }// Tratando erro
        }


    }
}
