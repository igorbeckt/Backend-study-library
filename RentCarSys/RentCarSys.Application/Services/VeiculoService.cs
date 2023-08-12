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
    public class VeiculoService
    {
        private readonly IVeiculosRepository _repositorioVeiculos;

        public VeiculoService(IVeiculosRepository repositorioVeiculos)
        {
            _repositorioVeiculos = repositorioVeiculos;
        }

        public async Task<ResultViewModel<List<Veiculo>>> BuscarTodosVeiculos()
        {
            try
            {
                var veiculos = await _repositorioVeiculos.ObterTodosVeiculosAsync();
                return new ResultViewModel<List<Veiculo>>(veiculos);
            }
            catch
            {
                return new ResultViewModel<List<Veiculo>>(erro: "05X05 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Veiculo>> BuscarVeiculoPorId(int veiculoId)
        {
            try
            {
                var veiculo = await _repositorioVeiculos.ObterVeiculoPorIdAsync(veiculoId);
                if (veiculo == null)
                {
                    return new ResultViewModel<Veiculo>(erro: "Veiculo não encontrado, verifique se o veiculo já foi cadastrado!");
                }

                return new ResultViewModel<Veiculo>(veiculo);
            }
            catch
            {
                return new ResultViewModel<Veiculo>(erro: "Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Veiculo>> BuscarVeiculoPorPlaca(string placa)
        {
            try
            {
                var veiculo = await _repositorioVeiculos.ObterVeiculoPorPlacaAsync(placa);
                if (veiculo == null)
                {
                    return new ResultViewModel<Veiculo>("Veiculo não encontrado, verifique se a placa está correta!");
                }

                return new ResultViewModel<Veiculo>(veiculo);
            }
            catch
            {
                return new ResultViewModel<Veiculo>("Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Veiculo>> CriarVeiculo(EditorVeiculoViewModel model)
        {
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

                await _repositorioVeiculos.AdicionarVeiculoAsync(veiculo);

                return new ResultViewModel<Veiculo>(veiculo);
            }
            catch
            {
                return new ResultViewModel<Veiculo>("05X10 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Veiculo>> EditarVeiculo(int veiculoId, EditorVeiculoViewModel model)
        {

            try
            {
                var veiculo = await _repositorioVeiculos.ObterVeiculoPorIdAsync(veiculoId);
                if (veiculo == null)
                {
                    return new ResultViewModel<Veiculo>("Veiculo não encontrado!");
                }

                if (veiculo.Status == VeiculoStatus.Running)
                {
                    return new ResultViewModel<Veiculo>("Não foi possível alterar o veiculo, possui reserva em andamento");
                }

                veiculo.Placa = model.Placa;
                veiculo.Marca = model.Marca;
                veiculo.Modelo = model.Modelo;
                veiculo.AnoFabricacao = model.AnoFabricacao;
                veiculo.KM = model.KM;
                veiculo.QuantidadePortas = model.QuantidadePortas;
                veiculo.Cor = model.Cor;
                veiculo.Automatico = model.Automatico;

                await _repositorioVeiculos.AtualizarVeiculoAsync(veiculo);

                return new ResultViewModel<Veiculo>(veiculo);
            }
            catch
            {
                return new ResultViewModel<Veiculo>("05X11 - Falha interna no servidor!");
            }
        }

        public async Task<ResultViewModel<Veiculo>> ExcluirVeiculo(int veiculoId)
        {
            try
            {
                var veiculo = await _repositorioVeiculos.ObterVeiculoPorIdAsync(veiculoId);
                if (veiculo == null)
                {
                    return new ResultViewModel<Veiculo>("Veiculo não encontrado!");
                }

                if (veiculo.Status == VeiculoStatus.Running)
                {
                    return new ResultViewModel<Veiculo>("Não foi possível excluir o veiculo, possui reserva em andamento");
                }

                await _repositorioVeiculos.ExcluirVeiculoAsync(veiculo);

                return new ResultViewModel<Veiculo>(veiculo);
            }
            catch
            {
                return new ResultViewModel<Veiculo>("05X12 - Falha interna no servidor!");
            }
        }
    }
}
