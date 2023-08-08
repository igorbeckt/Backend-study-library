using System.ComponentModel.DataAnnotations;

namespace Localdorateste.ViewModels
{
    public class EditorOficinaViewModel
    {
        [Required(ErrorMessage = "A reserva é obrigatório!")]
        public int ReservaId { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório!")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O Veiculo é obrigatório!")]
        public int VeiculoId { get; set; }        
    }
}