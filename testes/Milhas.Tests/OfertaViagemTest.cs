using JornadaMilhasV1.Modelos;

namespace Milhas.Tests;

public class OfertaViagemTest
{
    [Fact]
    public void TestandoOfertaValida()
    {

        // TIPO - ARRANGE
        Rota rota = new("OrigemTeste", "DestinoTeste");
        Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100.0;
        var validacao = true;

        // O QUE PODE DAR ERRO -ACT
        OfertaViagem oferta = new(rota, periodo, preco);

        // RESULTADO COMPARADO -ASSSERT
        Assert.Equal(validacao, oferta.EhValido);
    }

    [Fact]
    public void TestandoOfertaComRotaNula()
    {
        Rota rota = null;
        Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        double preco = 100.0;


        OfertaViagem oferta = new(rota, periodo, preco);

        Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
    }

    [Fact]
    public void TestandoOfertaComPeriodoInvalido()
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

}