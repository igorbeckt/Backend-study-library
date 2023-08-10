using Microsoft.EntityFrameworkCore;
using RentCarSys.Enums;
using System.Collections.Generic;
using System.Numerics;

namespace Localdorateste.Models
{    
    public class Reserva
    {
        public int Id { get; set; }
        public ReservaStatus Status { get; set; }   
        public string DataReserva { get; set; }
        public double ValorLocacao { get; set; }
        public string DataRetirada { get; set; }
        public string DataEntrega { get; set; }   
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Veiculo> Veiculo { get; set; }
    }
}

