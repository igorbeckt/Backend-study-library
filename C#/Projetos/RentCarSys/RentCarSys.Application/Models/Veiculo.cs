using Microsoft.EntityFrameworkCore;
using RentCarSys.Enums;

namespace Localdorateste.Models
{
    
    public class Veiculo
    {
        public int Id { get; set; }
        public VeiculoStatus Status { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string AnoFabricacao { get; set; }
        public string KM { get; set; }
        public int QuantidadePortas { get; set; }
        public string Cor { get; set; }
        public string Automatico { get; set; }

        public int? ReservaId { get; set; }
        public virtual Reserva? Reserva { get; set; }
 
    }
}
