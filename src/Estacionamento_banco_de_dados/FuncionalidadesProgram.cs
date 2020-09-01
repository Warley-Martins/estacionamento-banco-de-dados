using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Estacionamento_banco_de_dados
{
    public partial class Program
    {

        private static void AlterarDados(EstacionamentoContext contexto)
        {
            Console.Write("Digite o cpf do cliente que deseja alterar: ");
            string cpf = Console.ReadLine();
            var cliente = contexto.Clientes.Where(c => c.CPF.Equals(cpf)).FirstOrDefault();
            int opcaoMenu;
            do
            {
                do
                {
                    Console.Write("\nDeseja alterar: " +
                                  "\n(1). Nome" +
                                  "\n(2). CPF" +
                                  "\n(3). Adicionar Veiculo" +
                                  "\n(0). Salvar alterações" +
                                  "\nOpção: ");
                    try
                    {
                        opcaoMenu = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        opcaoMenu = int.MaxValue;
                        Console.WriteLine(e.Message);
                    } // Tentativa de atribuir em int
                } while (opcaoMenu < 0 || opcaoMenu > 3);

                switch (opcaoMenu)
                {
                    case 1:
                        Console.Write("Digite o novo nome: ");
                        cliente.Nome = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Digite o novo cpf: ");
                        cliente.CPF = Console.ReadLine();
                        break;
                    case 3:
                        var veiculo = CadastrarVeiculo();
                        cliente.IncluirVeiculo(veiculo);
                        contexto.SaveChanges();
                        break;
                }
            } while (opcaoMenu != 0);
            contexto.Clientes.Update(cliente);
            contexto.SaveChanges();
        }
        private static void FinalizarRegistro(EstacionamentoContext contexto)
        {
            var cliente = ProcurarCliente(contexto);
            var registro = cliente.Registros
                                            .Where(x => x.Estado == true)
                                            .FirstOrDefault();
            Console.WriteLine(cliente.CPF);
            int pagamento;
            do
            {
                Console.Write($"\nO valor do estacionamento foi: R${registro.GetValor(DateTime.Now.AddHours(2))}" +
                              $"\nO cliente pagou?" +
                              $"\n(1). Sim" +
                              $"\n(0). Não" +
                              $"\nOpção: ");
                pagamento = int.Parse(Console.ReadLine());
            } while (pagamento != 1);
            registro.Estado = false;
            contexto.SaveChanges();
        }

        private static Cliente ProcurarCliente(EstacionamentoContext contexto)
        {
            Console.Write("Digite o cpf do cliente: ");
            string cpf = Console.ReadLine();
            return contexto.Clientes
                                    .Where(x => x.CPF.Equals(cpf))
                                    .Select(x => new Cliente
                                    {
                                        CPF = x.CPF,
                                        Nome = x.Nome,
                                        Registros = x.Registros
                                    })
                                    .FirstOrDefault();
        }
        private static void IniciarRegistro(EstacionamentoContext contexto)
        {
            int opcaoMenu;
            do
            {
                Console.Write("\nO cliente ja possui cadastro?" +
                              "\n(1). Sim" +
                              "\n(0). Não" +
                              "\nOpção: ");
                opcaoMenu = int.Parse(Console.ReadLine());
            } while (opcaoMenu < 0 || opcaoMenu > 1);
            Cliente Cliente;
            switch (opcaoMenu)
            {
                case 1:
                    Console.Write("Digite o cpf do cliente: ");
                    var cpf = Console.ReadLine();
                    Cliente = contexto.Clientes.Where(c => c.CPF == cpf).FirstOrDefault();
                    Registro reg = new Registro();
                    Cliente.IncluirRegistro(reg);
                    contexto.SaveChanges();
                    break;
                case 0:
                    Cliente = CadastrarCliente();
                    var veiculo = CadastrarVeiculo();
                    Cliente.IncluirVeiculo(veiculo);
                    var registro = new Registro();
                    Cliente.IncluirRegistro(registro);
                    contexto.Clientes.Add(Cliente);
                    contexto.SaveChanges();
                    break;
            }
        }

        private static Cliente CadastrarCliente()
        {
            Console.Write("Digite o nome do cliente: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o cpf do cliente: ");
            string cpf = Console.ReadLine();
            return new Cliente(nome, cpf);
        }

        private static Veiculo CadastrarVeiculo()
        {

            Console.Write("Digite a placa do veiculo: ");
            var placa = Console.ReadLine();
            Console.Write("Digite o modelo do veiculo: ");
            var modelo = Console.ReadLine();
            Console.Write("Digite a cor do veiculo: ");
            var cor = Console.ReadLine();
            return new Veiculo(placa, modelo, cor);
        }
        private static void PrintarMenu()
        {
            Console.Write("\nDigite a opção desejada:" +
                          "\n(1). Iniciar um registro" +
                          "\n(2). Finalizar um registro" +
                          "\n(3). Alterar dados de um cliente" +
                          "\n(0). Encerrar" +
                          "\nOpção: ");
        }
    }
}
