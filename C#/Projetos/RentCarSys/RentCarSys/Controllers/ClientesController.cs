using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Localdorateste.Data;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Localdorateste.Extensions;
using RentCarSys.Enums;

namespace Localdorateste.Controllers
{
    
    [ApiController]
    [Route("/v1/clientes")]
    public class ClientesController : ControllerBase
    {
        
        // GET: v1/clientes
        [HttpGet]
        public async Task<IActionResult> BuscarClientes(
        [FromServices]Contexto context)
        {
            try
            {
                var clientes = await context.Clientes.ToListAsync();
                return Ok(new ResultViewModel<List<Cliente>>(clientes));
            }// Buscando todos os cliente no banco
            catch 
            {
                return StatusCode(500, value:new ResultViewModel<List<Cliente>>(erro:"05X05 - Falha interna no servidor!"));
            }// Tratando erro
        }
        

        // GET: v1/Clientes/1
        [HttpGet("{clienteid:int}")]
        public async Task<IActionResult> BuscarClientesId(
        [FromRoute] int clienteid,
        [FromServices] Contexto context)
        {
            try
            {
                var cliente = await context
                     .Clientes
                     .FirstOrDefaultAsync(x => x.ClienteId == clienteid);
                if (cliente == null)
                    return NotFound(new ResultViewModel<Cliente>(erro:"Cliente não encontrado, verifique se o cliente já foi cadastrado!"));
                // Validação do cliente

                return Ok(new ResultViewModel<Cliente>(cliente));
            }// Buscando o cliente no banco
            catch
            {
                return StatusCode(500, value:new ResultViewModel<Cliente>(erro:"Falha interna no servidor!"));
            }// Tratando erro

        }

        // GET: v1/Clientes/11122233344
        [HttpGet("{cpf}")]
        public async Task<IActionResult> BuscarClientesCPF(
        [FromRoute] long cpf,
        [FromServices] Contexto context)
        {
            try
            {
                var cliente = await context
                    .Clientes
                    .FirstOrDefaultAsync(x => x.CPF == cpf);
                if (cliente == null)
                    return NotFound(new ResultViewModel<Cliente>(erro: "Cliente não encontrado, verifique se o CPF está correto!"));
                // Validação do cliente pelo CPF

                return Ok(new ResultViewModel<Cliente>(cliente));                
            }// Buscando cliente pelo CPF
            catch
            {
                return StatusCode(500, value: new ResultViewModel<Cliente>(erro: "Falha interna no servidor!"));
            }// Tratando erro

        }

        // POST: v1/clientes
        [HttpPost]
        public async Task<IActionResult> CriarClientes(
        [FromBody] EditorClienteViewModel model,
        [FromServices] Contexto context)
        {
            if(!ModelState.IsValid)
                return BadRequest(error:new ResultViewModel<Cliente>(ModelState.PegarErros()));
            // Configuração de padronização de erro

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

                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();

                return Created(uri:$"v1/clientes/{cliente.ClienteId}", new ResultViewModel<Cliente>(cliente));
            }// Criação do cliente
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro:"05XE8 - Não foi possível criar o cliente!"));
            }// Tratando erros
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro:"05X10 - Falha interna no servidor!"));
            }// Tratando erros


        }

        // PUT: v1/Clientes
        [HttpPut("{clienteid:int}")]
        public async Task<IActionResult> EditarClientes(
        [FromRoute] int clienteid,
        [FromBody] EditorClienteViewModel model,
        [FromServices] Contexto context)
        {
            if (!ModelState.IsValid)
                return BadRequest(error: new ResultViewModel<Cliente>(ModelState.PegarErros()));
            // Configuração de padronização de erro

            try
            {
                var cliente = await context
                    .Clientes
                    .FirstOrDefaultAsync(x => x.ClienteId == clienteid);
                if (cliente == null)
                    return NotFound(new ResultViewModel<Cliente>(erro: "Cliente não encontrado!"));
                // Validação do cliente

                if (cliente.Status == ClienteStatus.Online)
                    return NotFound(new ResultViewModel<Cliente>(erro: "Não foi possivel alterar o cliente,possui reserva em andamento"));
                // Validação para saber se o cliente não tem uma reserva em andamento

                cliente.NomeCompleto = model.NomeCompleto;
                cliente.Email = model.Email;
                cliente.RG = model.RG;
                cliente.CPF = model.CPF;

                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Cliente>(cliente));
                // Alteração do cliente
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "05XE8 - Não foi possível alterar o cliente!"));
            }// Tratando erro
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "05X11 - Falha interna no servidor!"));
            }// Tratando erro

        }

        // DELETE: v1/clientes/1
        [HttpDelete("{clienteid:int}")]
        public async Task<IActionResult> ExcluirClientes(
        [FromRoute] int clienteid,
        [FromServices] Contexto context)
        {
            try
            {
                var cliente = await context
                    .Clientes
                    .FirstOrDefaultAsync(x => x.ClienteId == clienteid);
                if (cliente == null)
                    return NotFound(new ResultViewModel<Cliente>(erro: "Cliente não encontrado!"));
                // Validação do cliente

                if (cliente.Status == ClienteStatus.Offline)
                    return NotFound(new ResultViewModel<Cliente>(erro: "Não foi possivel excluir o cliente,possui reserva em andamento"));
                // Validação para saber se o cliente não tem uma reserva em andamento

                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Cliente>(cliente));
                // Remoção do cliente
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "05XE7 - Não foi possível excluir o cliente!"));
            }// Tratando erro
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "05X12 - Falha interna no servidor!"));
            }// Tratando erro
        }
    }
}
