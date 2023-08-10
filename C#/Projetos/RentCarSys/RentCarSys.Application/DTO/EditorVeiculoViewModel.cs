using System.ComponentModel.DataAnnotations;

namespace Localdorateste.ViewModels
{
    public class EditorVeiculoViewModel
    {
        [Required(ErrorMessage = "A placa é obrigatório!")]
        [StringLength(7, MinimumLength = 7,ErrorMessage ="A Placa deve conter 4 letras e 3 números!")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "A marca é obrigatório!")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório!")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O ano de fabricação é obrigatório!")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O ano de Fabricação deve conter 4 números!")]
        public string AnoFabricacao { get; set; }

        [Required(ErrorMessage = "A quilometragem é obrigatório!")]
        public string KM { get; set; }

        [Required(ErrorMessage = "A quantidade de portas é obrigatório!")]
        public int QuantidadePortas { get; set; }

        [Required(ErrorMessage = "A cor é obrigatório!")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório!")]
        public string Automatico { get; set; }
    }
}
