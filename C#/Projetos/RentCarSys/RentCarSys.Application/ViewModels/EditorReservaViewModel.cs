using Localdorateste.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Localdorateste.ViewModels
{
    public class EditorReservaViewModel
    {
        [Required(ErrorMessage = "O ClienteId é obrigatório!")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O VeiculoId é obrigatório!")]
        public int VeiculoId { get; set; }

        [Required(ErrorMessage = "A data da reserva é obrigatório!")]
        public string DataReserva { get; set; }
        public long ValorLocacao { get; set; }

        [Required(ErrorMessage = "A data de retirada é obrigatório!")]
        public string DataRetirada { get; set; }

        [Required(ErrorMessage = "A data de entrega é obrigatório!")]
        public string DataEntrega { get; set; }
    }
}
