using Microsoft.EntityFrameworkCore;
using RentCarSys.Enums;

namespace Localdorateste.Models
{    
    public class Cliente
    {        
        public int Id { get; set; }
        public ClienteStatus Status { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public long RG { get; set; }
        public long CPF { get; set; }

        public int? ReservaId { get; set; }
        public virtual Reserva? Reserva { get; set; }
    }
}
