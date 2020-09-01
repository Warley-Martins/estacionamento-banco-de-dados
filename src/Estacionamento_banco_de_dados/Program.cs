using System;

namespace Estacionamento_banco_de_dados
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            
            int opcaoMenu;
            do  //Inicio do loop
            {
                do  // Inicio Opcao do menu
                { 
                    PrintarMenu();
                    try
                    {
                        opcaoMenu = int.Parse(Console.ReadLine());
                    }
                    catch(Exception e)
                    {
                        opcaoMenu = int.MaxValue;
                        Console.WriteLine($"Exceção lançada: {e.Message}");
                    }
                } while (opcaoMenu < 0 || opcaoMenu > 3); // Fim Opcao do menu
                using (var contexto = new EstacionamentoContext()) //Abertura do banco de dados
                {
                    switch (opcaoMenu)
                    {
                        case 1:
                            IniciarRegistro(contexto); //Inicia o registro de um cliente
                            break;
                        case 2:
                            FinalizarRegistro(contexto); //Finaliza o registro de um cliente
                            break;
                        case 3:
                            AlterarDados(contexto); //Altera dados de um cliente
                            break;
                    }
                }  // Fechamento do banco de dados
            } while (opcaoMenu != 0); //Fim do loop do programa
        }
    }
}
