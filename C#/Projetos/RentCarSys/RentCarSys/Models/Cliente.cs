using Microsoft.EntityFrameworkCore;

namespace Localdorateste.Models
{
    [PrimaryKey(nameof(ClienteId))]
    public class Cliente
    {        
        public int ClienteId { get; set; }
        public string StatusCliente { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public long RG { get; set; }
        public long CPF { get; set; }
    }
}
