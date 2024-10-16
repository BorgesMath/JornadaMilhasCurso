using JornadaMilhasV1.Modelos;

namespace Milhas.Tests;

            // Nome referente ao que vai ser testado
public class OfertaViagemConstrutor
{
    [Theory]
    [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
    [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05",100, true)]
    [InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1, false)]
    [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
    [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
    public void RetornaEhValidoComParametros(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
    {

        // TIPO - ARRANGE
        Rota rota = new(origem, destino);
        Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

        // O QUE PODE DAR ERRO -ACT
        OfertaViagem oferta = new(rota, periodo, preco);

        // RESULTADO COMPARADO -ASSSERT
        Assert.Equal(validacao, oferta.EhValido);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-250)]
    public void RetornaMsgErroPrecoInvalido(double preco)
    {
        //arrange
        Rota rota = new("Origem1", "Destino1");
        Periodo periodo = new(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));

        //act
        OfertaViagem oferta = new(rota, periodo, preco);

        //assert
        Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
    }



    [Fact]
    public void RetornaMsgErroQuandoRotaNula()
    {
        Rota rota = null;
        Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100.0;


        OfertaViagem oferta = new(rota, periodo, preco);

        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
    }

    [Fact]
    public void RetornaMsgErroDeQuandoPeriodoInvalido()
    {
        //arrange
        Rota rota = new Rota("OrigemTeste", "DestinoTeste");
        Periodo periodo = new Periodo(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
        double preco = 100.0;

        //act
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //assert
        Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
        Assert.False(oferta.EhValido);
    }


    [Fact]
    public void RetornaMsgErroQuandoPrecoNegativo()
    {
        //arrange
        Rota rota = new("Origem1", "Destino1");
        Periodo periodo = new(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));
        double preco = -250.99;


        //act 
        OfertaViagem oferta = new(rota, periodo, preco);


        //assert 
        Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
    }
}