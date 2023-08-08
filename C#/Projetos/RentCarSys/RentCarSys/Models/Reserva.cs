using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;

namespace Localdorateste.Models
{    
    public class Reserva
    {
        public int ReservaId { get; set; }
        public string StatusReserva { get; set; }
        public int ClienteId { get; set; }
        public string NomeCompleto { get; set; }
        public long CPF { get; set; }
        public int VeiculoId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }        
        public string DataReserva { get; set; }
        public long ValorLocacao { get; set; }
        public string DataRetirada { get; set; }
        public string DataEntrega { get; set; }              
                            
    }

}

