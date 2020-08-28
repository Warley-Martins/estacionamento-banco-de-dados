using System;

namespace Estacionamento_banco_de_dados
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new EstacionamentoContext())
            {

                Cliente c = new Cliente("Warley Martins", "25847539630");
                Veiculo v = new Veiculo("AGB-0876", "palio", "preto");
                c.IncluirVeiculo(v);
                Registro r = new Registro(c);
                contexto.Registros.Add(r);
                contexto.SaveChanges();
                Console.ReadLine();

            //}
            //int opcaoMenu;
            //do
            //{
            //    do
            //    {
            //        PrintarMenu();
            //        opcaoMenu = int.Parse(Console.ReadLine());
            //    } while (opcaoMenu < 0 || opcaoMenu > 3);
            //    using (var contexto = new EstacionamentoContext())
            //    {
            //        switch (opcaoMenu)
            //        {
            //            case 1:
            //                CadastrarCliente(contexto);
            //                break;
            //            case 2:
            //                RemoverCliente(contexto); ;
            //                break;
            //            case 3:
            //                AlterarDados(contexto);
            //                break;
            //        }
            //    }
            //} while (opcaoMenu != 0);
        }
    }
}
