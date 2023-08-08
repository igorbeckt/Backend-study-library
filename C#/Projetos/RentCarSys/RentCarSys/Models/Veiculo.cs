using Microsoft.EntityFrameworkCore;

namespace Localdorateste.Models
{
    [PrimaryKey(nameof(VeiculoId))]
    public class Veiculo
    {
        public int VeiculoId { get; set; }
        public string StatusVeiculo { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string AnoFabricacao { get; set; }
        public string KM { get; set; }
        public int QuantidadePortas { get; set; }
        public string Cor { get; set; }
        public int Condicao { get; set; }
        public string VidroEletrico { get; set; }
        public string TravaEletrica { get; set; }
        public string Automatico { get; set; }
        public string ArCondicionado { get; set; }
        public string DirecaoHidraulica { get; set; }
 
    }
}
