// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;

class Carro
{
    [Display(Order = 2)]
    public string Modelo { get; set; }

    [Display(Order = 1)]
    public string Marca { get; set; }

    [Display(Order = 4, Name = "Ano de lançamento")]
    public int Ano { get; set; }

    [Display(Order = 3)]
    public double Preco { get; set; } // em reais
}
