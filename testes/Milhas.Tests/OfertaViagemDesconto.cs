using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milhas.Tests;

public class OfertaViagemDesconto
{
    [Theory]
    [InlineData(100.00, 2.00, 98.00)]  // Desconto normal
    [InlineData(100.00, 200.00, 100.00)] // Desconto maior que o preço, esperado é o preço original (desconto zerado)
    [InlineData(100.00, -20.00, 100.00)] // Desconto negativo, o preço não muda
    public void RetornaPrecoNovoComDesconto(double precoOriginal, double desconto, double precoEsperado)
    {
        // ARRANGE
        Rota rota = new("OrigemA", "DestinoB");
        Periodo periodo = new(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        OfertaViagem oferta = new(rota, periodo, precoOriginal);

        // ACT
        oferta.Desconto = desconto;

        // ASSERT
        Console.WriteLine($"Preço esperado: {precoEsperado}, Preço calculado: {oferta.Preco}");

        Assert.Equal(precoEsperado, oferta.Preco, 0.001);
    }
}

