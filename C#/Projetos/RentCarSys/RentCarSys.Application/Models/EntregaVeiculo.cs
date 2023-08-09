using Microsoft.EntityFrameworkCore;

namespace Localdorateste.Models
{
    [PrimaryKey(nameof(EntregaVeiculoId))]
    public class EntregaVeiculo
    {
        public int EntregaVeiculoId { get; set; }
        public string Operacao { get; set; }        
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public string NomeCompleto { get; set; }
        public int VeiculoId { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
    }
}
