using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Estacionamento_banco_de_dados
{
    public partial class Program
    {

        private static void AlterarDados(EstacionamentoContext contexto)
        {
            string mensagem;
            Cliente cliente = ProcurarCliente(contexto);
            if (cliente == null)
            {
                Console.WriteLine("\nNão existe cliente cadastrado com o cpf digitado!");
                return;
            } // retorna para main
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
                        mensagem = "Digite o novo nome: ";
                        string nome = AtribuirString(mensagem);
                        cliente.Nome = nome;
                        break;
                    case 2:
                        mensagem = "Digite o novo cpf: ";
                        cliente.CPF = AtribuirString(mensagem);
                        break;
                    case 3:
                        var veiculo = CadastrarVeiculo();
                        cliente.IncluirVeiculo(veiculo);
                        contexto.SaveChanges();
                        break;
                }
            } while (opcaoMenu != 0); // Alteracao de dados do cliente
            contexto.Clientes.Update(cliente);
            contexto.SaveChanges();
        }
        private static void FinalizarRegistro(EstacionamentoContext contexto)
        {
            var cliente = ProcurarCliente(contexto);
            if (cliente == null)
            {
                Console.WriteLine($"\nNão foi encontrado cliente com o cpf digitado!");
                return;
            } //retorna para main
            Registro registro = cliente.Registros
                                                  .Where(x => x.Estado == true)
                                                  .FirstOrDefault();
            if (registro == null)
            {
                Console.WriteLine("\nO cliente não possui registros ativos");
                return;
            } // retorna para main
            int pagamento;
            do
            {
                Console.Write($"\nO valor do estacionamento foi: R${registro.GetValor(DateTime.Now.AddHours(2))}" +
                              $"\nO cliente pagou?" +
                              $"\n(1). Sim" +
                              $"\n(0). Não" +
                              $"\nOpção: ");
                try
                {
                    pagamento = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    pagamento = int.MaxValue;
                    Console.WriteLine($"Exceção Lançada: {e.Message}");
                } // Atribuicao segura de int
            } while (pagamento != 1); // Enquanto o cliente não pagar
            registro.Estado = false;
            contexto.SaveChanges();
        }
        private static Cliente ProcurarCliente(EstacionamentoContext contexto)
        {
            var mensagem = "Digite o cpf do cliente: ";
            string cpf = AtribuirString(mensagem);
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
                try
                {
                    opcaoMenu = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    opcaoMenu = int.MaxValue;
                    Console.WriteLine($"Exceção do tipo: {e.Message}");
                } // Atribuicao de int seguro
            } while (opcaoMenu < 0 || opcaoMenu > 1); // Menu de opcoes
            Cliente Cliente;
            switch (opcaoMenu)
            {
                case 1: // Inicio de registo para clientes cadastrados
                    Cliente = ProcurarCliente(contexto);
                    if (Cliente == null)
                    {
                        Console.WriteLine($"\nNão foi encontrado cliente com o cpf digitado!");
                        return;
                    } // retorna para main
                    Registro reg = new Registro();
                    Cliente.IncluirRegistro(reg);
                    contexto.SaveChanges();
                    break;
                case 0: // Inicio de registro para clientes nao cadastrados
                    Cliente = CadastrarCliente();
                    var veiculo = CadastrarVeiculo();
                    Cliente.IncluirVeiculo(veiculo);
                    var registro = new Registro();
                    Cliente.IncluirRegistro(registro);
                    contexto.Clientes.Add(Cliente);
                    contexto.SaveChanges();
                    break;
            } // Inicio de registro
        }

        private static Cliente CadastrarCliente()
        {
            var mensagem = "Digite o nome do cliente: ";
            string nome = AtribuirString(mensagem);
            mensagem = "Digite o cpf do cliente: ";
            string cpf = AtribuirString(mensagem);
            return new Cliente(nome, cpf);
        }

        private static Veiculo CadastrarVeiculo()
        {
            var mensagem = "Digite a placa do veiculo: ";
            var placa = AtribuirString(mensagem);
            mensagem = "Digite o modelo do veiculo: ";
            var modelo = AtribuirString(mensagem);
            mensagem = "Digite a cor do veiculo: ";
            var cor = AtribuirString(mensagem);
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
        private static string AtribuirString(string mensagem)
        {
            string auxiliar;
            do
            {
                Console.Write(mensagem);
                auxiliar = Console.ReadLine();
            } while (String.IsNullOrEmpty(auxiliar) == true);
            return auxiliar;
        }
    }
}
