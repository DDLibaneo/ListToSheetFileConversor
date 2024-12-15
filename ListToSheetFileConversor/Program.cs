// See https://aka.ms/new-console-template for more information
using ListToSheetConversor;
using ListToSheetFileConversor;

Console.WriteLine("Hello, World!");

var frutas = new List<Fruta>
{
    new Fruta { Nome = "Maçã", Cor = "Vermelha", Peso = 150.0 },
    new Fruta { Nome = "Banana", Cor = "Amarela", Peso = 120.0 },
    new Fruta { Nome = "Laranja", Cor = "Laranja", Peso = 200.0 },
    new Fruta { Nome = "Uva", Cor = "Roxa", Peso = 50.0 },
    new Fruta { Nome = "Abacaxi", Cor = "Amarelo com Verde", Peso = 1200.0 }
};

List<Carro> carros = new List<Carro>
{
    new Carro { Modelo = "Civic", Marca = "Honda", Ano = 2022, Preco = 150000.00 },
    new Carro { Modelo = "Corolla", Marca = "Toyota", Ano = 2023, Preco = 160000.00 },
    new Carro { Modelo = "Gol", Marca = "Volkswagen", Ano = 2020, Preco = 80000.00 },
    new Carro { Modelo = "Onix", Marca = "Chevrolet", Ano = 2021, Preco = 75000.00 },
    new Carro { Modelo = "Mustang", Marca = "Ford", Ano = 2021, Preco = 350000.00 }
};

await ListToCsvFileConversor.ConvertListToCsvFile(@"C:\Estudos\Arquivos_CSV\frutas.csv", frutas);
await ListToCsvFileConversor.ConvertListToCsvFile(@"C:\Estudos\Arquivos_CSV\carros.csv", carros);

