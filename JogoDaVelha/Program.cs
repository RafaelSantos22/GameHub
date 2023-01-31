using HubJogos.Entities;
using HubJogos.Enums;
using System;

namespace GameHub
{
    class Program 
    {
        public static List<Jogador> jogadores { get; set; } = new List<Jogador>();

        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Menu();
                opcao = int.Parse(Console.ReadLine());
                MenuPrincipal escolha = (MenuPrincipal)opcao;

                switch (escolha)
                {
                    case MenuPrincipal.Sair:
                        Console.Clear();
                        Console.WriteLine("Programa Finalizado!!!");
                        break;
                    case MenuPrincipal.Cadastrar:
                        Cadastro();
                        break;
                    case MenuPrincipal.Login:
                        Login();
                        break;
                    case MenuPrincipal.ListaJogadores:
                        ListarJogadores();
                        break;
                }
            } while (opcao != 0);
        }

        static void Menu()
        {
            Console.WriteLine("\t\t\t\t\t===== GameHub =====\n\n");
            Console.WriteLine("1 - Cadastrar\n2 - Login\n3 - Lista de Jogadores\n0 - Sair\n");
            Console.Write("Digite a opção desejada: ");
        }

        static void Cadastro()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t===== TELA DE CADASTRO =====\n");
            Console.Write("Digite seu nickname: ");
            string nickName = Console.ReadLine();
            int indiceNick = jogadores.FindIndex(nick => nick.NickName == nickName);

            if (indiceNick == -1)
            {
                Console.Write("Digite sua senha: ");
                string senha = Console.ReadLine();
                
                if(senha.Length > 2)
                {
                    jogadores.Add(new Jogador(nickName, senha));
                    Console.WriteLine("\nUsuário cadastrado com sucesso!\n");
                    Console.WriteLine("Digite ENTER para voltar ao menu!");
                    Console.ReadLine();
                    Console.Clear();
                } else
                {
                    Console.Clear();
                    Console.WriteLine("A senha deve ter 3 digitos ou mais!!");
                }
            }
            else
            {
                Console.WriteLine("\nUsuário já cadastrado!\n");
                Console.WriteLine("Digite ENTER para voltar ao menu!");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t ===== TELA DE LOGIN =====\n\n");

            Console.Write("Digite seu nickname: ");
            string nickName = Console.ReadLine();

            int indiceNick = jogadores.FindIndex(nick => nick.NickName == nickName);

            if (indiceNick == -1)
            {
                Console.Clear();
                Console.WriteLine("\nNickName não encontrado!\n");
            }
            else
            {
                Console.Write("Digite sua senha: ");
                string senhaJogador = Console.ReadLine();

                while (senhaJogador != jogadores[indiceNick].Senha)
                {
                    Console.Write("Senha Incorreta! Digite novamente: ");
                    senhaJogador = Console.ReadLine();
                }
                Console.Clear();
                Console.WriteLine($"Bem vindo(a) {jogadores[indiceNick].NickName}\n");
                bool escolheuSair = false;
                while (!escolheuSair)
                {
                    Console.WriteLine("1 - Jogo da Velha\n2 - Batalha Naval\n0 - Voltar ao menu principal\n");
                    Console.Write("Escolha a opção desejada: ");
                    int opcao = int.Parse(Console.ReadLine());

                    MenuJogos escolha = (MenuJogos)opcao;

                    switch (escolha)
                    {
                        case MenuJogos.Sair:
                            Console.Clear();
                            escolheuSair = true;
                            break;
                        case MenuJogos.JogoDaVelha:
                            new JogoDaVelha().Menu();
                            break;
                        case MenuJogos.BatalhaNaval:
                            //new BatalhaNaval().IniciarJogo();
                            break;
                    }
                }
            }
        }

        public static void ListarJogadores()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t===== LISTA DE JOGADORES =====\n");

            int i = 1;
            if (jogadores.Count > 0)
            {
                foreach (Jogador jogador in jogadores)
                {
                    Console.WriteLine($"ID: {i} | NickName: {jogador.NickName}");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Não há jogadores cadastrados!");
            }
        }
    }
}
