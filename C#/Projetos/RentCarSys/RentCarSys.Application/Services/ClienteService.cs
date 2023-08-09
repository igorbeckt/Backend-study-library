using Localdorateste.Extensions;
using Localdorateste.Models;
using Localdorateste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RentCarSys.Application.Interfaces;
using RentCarSys.Enums;

namespace RentCarSys.Application.Services
{
    [ApiController]
    [Route("/v1/clientes")]
    public class ClienteService : ControllerBase
    {
        private readonly IClientesRepository _repositorioClientes;

        public ClienteService(IClientesRepository repositorioClientes)
        {
            _repositorioClientes = repositorioClientes;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarClientes()
        {
            try
            {
                var clientes = await _repositorioClientes.ObterTodosClientesAsync();
                return Ok(new ResultViewModel<List<Cliente>>(clientes));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Cliente>>(erro: "05X05 - Falha interna no servidor!"));
            }
        }

        [HttpGet("{clienteid:int}")]
        public async Task<IActionResult> BuscarClientesId
        ([FromRoute] int clienteid)
        {
            try
            {
                var cliente = await _repositorioClientes.ObterClientePorIdAsync(clienteid);
                if (cliente == null)
                {
                    return NotFound(new ResultViewModel<Cliente>(erro: "Cliente não encontrado, verifique se o cliente já foi cadastrado!"));
                }

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "Falha interna no servidor!"));
            }
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> BuscarClientesCPF
        ([FromRoute] long cpf)
        {
            try
            {
                var cliente = await _repositorioClientes.ObterClientePorCPFAsync(cpf);
                if (cliente == null)
                {
                    return NotFound(new ResultViewModel<Cliente>(erro: "Cliente não encontrado, verifique se o CPF está correto!"));
                }

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "Falha interna no servidor!"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarClientes
        ([FromBody] EditorClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Cliente>(ModelState.PegarErros()));
            }

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

                return Created($"v1/clientes/{cliente.ClienteId}", new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "05X10 - Falha interna no servidor!"));
            }
        }

        [HttpPut("{clienteid:int}")]
        public async Task<IActionResult> EditarClientes
            ([FromRoute] int clienteid, 
            [FromBody] EditorClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Cliente>(ModelState.PegarErros()));
            }

            try
            {
                var cliente = await _repositorioClientes.ObterClientePorIdAsync(clienteid);
                if (cliente == null)
                {
                    return NotFound(new ResultViewModel<Cliente>(erro: "Cliente não encontrado!"));
                }

                if (cliente.Status == ClienteStatus.Online)
                {
                    return NotFound(new ResultViewModel<Cliente>(erro: "Não foi possível alterar o cliente, possui reserva em andamento"));
                }

                cliente.NomeCompleto = model.NomeCompleto;
                cliente.Email = model.Email;
                cliente.RG = model.RG;
                cliente.CPF = model.CPF;

                await _repositorioClientes.AtualizarClienteAsync(cliente);

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "05X11 - Falha interna no servidor!"));
            }
        }

        [HttpDelete("{clienteid:int}")]
        public async Task<IActionResult> ExcluirClientes([FromRoute] int clienteid)
        {
            try
            {
                var cliente = await _repositorioClientes.ObterClientePorIdAsync(clienteid);
                if (cliente == null)
                {
                    return NotFound(new ResultViewModel<Cliente>(erro: "Cliente não encontrado!"));
                }

                if (cliente.Status == ClienteStatus.Offline)
                {
                    return NotFound(new ResultViewModel<Cliente>(erro: "Não foi possível excluir o cliente, possui reserva em andamento"));
                }

                await _repositorioClientes.ExcluirClienteAsync(cliente);

                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Cliente>(erro: "05X12 - Falha interna no servidor!"));
            }
        }
    }
}
