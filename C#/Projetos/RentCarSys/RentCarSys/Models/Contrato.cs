using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Localdorateste.Models
{
    [PrimaryKey(nameof(ContratoId))]
    public class Contrato
    {
        
        public int ContratoId { get; set; }
        public int ReservaId { get; set; }
        public string StatusReserva { get; set; }
        public string DataReserva { get; set; }
        public string DataRetirada { get; set; }
        public string DataEntrega { get; set; }
        public double ValorLocacao { get; set; }
        public int ClienteId { get; set; }
        public string NomeCompleto { get; set; }
        public long CPF { get; set; }
        public int VeiculoId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string FormaPagamento { get; set; }
        public int Parcelas { get; set; }


        public string TextoContrato()
        {
            string textoContrato = "Hoje dia: "+DataReserva+". Pelo presente instrumento particular de locação, o LOCADOR, cujo nome: "+NomeCompleto+", portador do CPF nº:"+CPF+ ", residente e domiciliado nesta capital, tem entre si justos e contratados o seguinte: PRIMEIRA CLÁUSULA – O LOCADOR declara ser o legítimo proprietário do veículo de marca: "+Marca+" e modelo: "+Modelo+" com a placa: "+Placa+", em perfeito estado e que resolveu dá-lo em locação à empresa , pelo prazo da data:"+DataRetirada+" até a data:"+DataEntrega+", renováveis automaticamente por igual período caso não haja manifestação em contrário de uma das partes, mediante o valor de R$"+ValorLocacao+", que será pago em moeda corrente do país, veículo este que entrega nessa data:"+DataRetirada+" ao LOCATÁRIO, para que do mesmo possa utilizar-se como entender. O valor cobrado será utilizado para o pagamento do aluguel do veículo, o combustível utilizado e todas as despesas de manutenção, que ficarão por conta do LOCADOR.";
                
            return textoContrato;
        }
    }
}
