using Microsoft.EntityFrameworkCore;

namespace Localdorateste.Models
{
    [PrimaryKey(nameof(RetiradaVeiculoId))]
    public class RetiradaVeiculo
    {
                
        public int RetiradaVeiculoId { get; set; }
        public string Operacao { get; set; }    
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public string NomeCompleto { get; set; }
        public int VeiculoId { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
    }
}
