using System.ComponentModel.DataAnnotations;

namespace Localdorateste.ViewModels
{
    public class EditorContratoViewModel
    {
        [Required(ErrorMessage = "A ReservaId é obrigatório!")]
        public int ReservaId { get; set; }

        [Required(ErrorMessage = "A Forma de pagamento é obrigatório!")]
        
        public string FormaPagamento { get; set; }

        [Required(ErrorMessage = "O número de parcelas é obrigatório!")]
        [Range(0, 12, ErrorMessage = "Digite '1' para pagamentos em dinheiro,pix ou boleto e '1' a '12' para pagamentos no cartão de crédito!")]
        public int Parcelas { get; set; }
    }
}
