﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http.Headers;

namespace Estacionamento_banco_de_dados
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcaoMenu;
            do
            {
                do
                {
                    PrintarMenu();
                    opcaoMenu = int.Parse(Console.ReadLine());
                } while (opcaoMenu < 0 || opcaoMenu > 3);
                using (var contexto = new EstacionamentoContext())
                {
                    switch (opcaoMenu)
                    {
                        case 1:
                            CadastrarCliente(contexto);
                            break;
                        case 2:
                            RemoverCliente(contexto); ;
                            break;
                        case 3:
                            AlterarDados(contexto);
                            break;
                    }
                }
            } while (opcaoMenu != 0);

        }

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
                    Console.Write("\nOla mundo teste\nDeseja alterar: " +
                                  "\n(1). Nome" +
                                  "\n(2). CPF" +
                                  "\n(3). Veiculo" +
                                  "\n(0). Salvar alterações" +
                                  "\nOpção: ");
                    opcaoMenu = int.Parse(Console.ReadLine());
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
                        Console.Write("Digite a nova placa: ");
                        cliente.PlacaCarro = Console.ReadLine();
                        break;
                }
            } while (opcaoMenu != 0);
            contexto.Clientes.AddOrUpdate(cliente);
            contexto.SaveChanges();
        }

        private static void Entries(IEnumerable<DbEntityEntry> entries)
        {
            Console.WriteLine(entries);
        }

        private static void RemoverCliente(EstacionamentoContext contexto)
        {
            Console.Write("Digite o cpf do cliente: ");
            string cpf = Console.ReadLine();
            var clienteRemovido = contexto.Clientes.Where(x => x.CPF.Equals(cpf)).FirstOrDefault();
            contexto.Clientes.Remove(clienteRemovido);
            contexto.SaveChanges();
        }

        private static void CadastrarCliente(EstacionamentoContext contexto)
        {

            Console.Write("Digite o nome do cliente: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o cpf do cliente: ");
            string cpf = Console.ReadLine();
            Console.Write("Digite a placa do veiculo do cliente: ");
            string placa = Console.ReadLine();
            contexto.Clientes.Add(new Cliente(nome, cpf, placa));
            contexto.SaveChanges();
        }

        private static void PrintarMenu()
        {
            Console.Write("\nDigite a opção desejada:" +
                          "\n(1). Cadastrar um novo cliente" +
                          "\n(2). Remover um cliente" +
                          "\n(3). Alterar dados de um cliente" +
                          "\n(0). Encerrar" +
                          "\nOpção: ");
        }
    }
}
