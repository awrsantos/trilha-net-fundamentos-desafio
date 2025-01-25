using System.Net;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            
            string placa = Console.ReadLine().ToUpper();
            if (validarPlaca(placa)) {
                veiculos.Add(placa);
            } else {
                Console.WriteLine("Placa inválida! A placa deve estar no formato ABC1234 ou ABC1D23");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa = Console.ReadLine().ToUpper();
            if (validarPlaca(placa)) {
                // Verifica se o veículo existe
                if (veiculos.Any(x => x.ToUpper() == placa.ToUpper())) {
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                    uint horas = 0;
                    decimal valorTotal = 0;

                    bool sucess = uint.TryParse(Console.ReadLine(), out horas);
                    if (sucess) {
                        valorTotal = precoInicial + (precoPorHora * horas);
                    } else {
                        throw new Exception("A quantidade de horas informadas é inválida.");
                    }

                    veiculos.Remove(placa);
                    Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
                } else {
                    Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
                }
            } else {
                Console.WriteLine("Placa inválida! A placa deve estar no formato ABC1234 ou ABC1D23");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any()) {
                Console.WriteLine("Os veículos estacionados são:");
                
                foreach (string veiculo in veiculos) {
                    Console.WriteLine(veiculo);
                }
            } else {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
        
        private bool validarPlaca(string placa)
        {
            // O \b é usado para limitar a string, evitando que aceite mais caracteres que o necessário
            Match searchPlacaAntiga = Regex.Match(placa, @"\b[a-zA-z]{3}[0-9]{4}\b");
            Match searchPlacaNova = Regex.Match(placa, @"\b[a-zA-z]{3}[0-9]{1}[a-zA-z]{1}[0-9]{2}\b");
            
            return searchPlacaAntiga.Success || searchPlacaNova.Success;
        }
    }
}
