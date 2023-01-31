using HubJogos.Entities;

namespace GameHub
{
    public class JogoDaVelha
    {
        public bool FimDeJogo { get; set; }
        public char[] Posicoes { get; set; }
        public char Vez { get; set; }
        public int QuantidadePreenchida { get; set; }
        public string player;
        public int v1 = 0, d1 = 0, v2 = 0, d2 = 0, empate = 0;
        public List<Jogador> players = Program.jogadores;

        public JogoDaVelha()
        {
            FimDeJogo = false;
            Posicoes = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Vez = 'X';
            QuantidadePreenchida = 0;
        }

        public void Menu()
        {
            int opcao;
            Console.Clear();
            Console.WriteLine("\t\t\t\t========== JOGO DA VELHA ==========\n");
            Console.WriteLine("1 - Start\n2 - Ranking\n0 - Sair\n");
            Console.Write("Escolha sua opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 0:
                    Console.Clear();
                    break;
                case 1:
                    EscolherOponente();
                    break;
                case 2:
                    Ranking();
                    break;
            }
        }

        public void Ranking()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t ===== PLACAR =====\n\n");
            Console.WriteLine($"Jogador 1: {players[0].NickName}    | V: {v1} D: {d1} E: {empate}");
            Console.WriteLine($"Jogador 2: {players[1].NickName}    | V: {v2} D: {d2} E: {empate}\n\n");
        }

        public void EscolherOponente()
        {
            Program.ListarJogadores();
            Console.Write("\nDigite o nick do seu adversário: ");
            string nickname = Console.ReadLine();

            int indiceNick = players.FindIndex(nick => nick.NickName == nickname);

            if(indiceNick == - 1)
            {
                Console.Clear();
                Console.WriteLine("\nNickName não encontrado!\n");
            } else
            {
                Iniciar();
            }
        }

        public void Iniciar()
        {
            player = players[0].NickName;
            while (!FimDeJogo)
            {
                RenderizarTabela();
                LerEscolhaDoUsuario();
                RenderizarTabela();
                VerificarFimJogo();
                MudarVez();
            }
        }

        private void RenderizarTabela()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t========== JOGO DA VELHA ==========\n");
            Console.WriteLine($"Jogador 1 (X): {players[0].NickName}");
            Console.WriteLine($"Jogador 2 (O): {players[1].NickName}");
            Console.WriteLine(Tabela());
        }

        private string Tabela()
        {
            return $"\n\t\t\t\t\t  {Posicoes[0]}  |  {Posicoes[1]}  |  {Posicoes[2]}  \n" +
                   $"\t\t\t\t\t_____|_____|_____\n" +
                   $"\t\t\t\t\t     |     |     \n" +
                   $"\t\t\t\t\t  {Posicoes[3]}  |  {Posicoes[4]}  |  {Posicoes[5]}  \n" +
                   $"\t\t\t\t\t_____|_____|_____\n" +
                    $"\t\t\t\t\t     |     |     \n" +
                   $"\t\t\t\t\t  {Posicoes[6]}  |  {Posicoes[7]}  |  {Posicoes[8]}  \n\n";
        }

        private void LerEscolhaDoUsuario()
        {
            Console.Write($"Vez de {{{player}}} {Vez}, digite a posicao de 1 a 9: ");
            int posicao = int.Parse(Console.ReadLine());

            int indice = posicao - 1;
            Posicoes[indice] = Vez;
            QuantidadePreenchida++;
        }

        private void MudarVez()
        {
            Vez = Vez == 'X' ? 'O' : 'X';
            if (Vez == 'X')
            {
                player = players[0].NickName;
            }
            else
            {
                player = players[1].NickName;
            }
        }

        private void VerificarFimJogo()
        {
            int continuar;
            if (QuantidadePreenchida < 5)
                return;

            if (VitoriaHorizontal() | VitoriaVertical() | VitoriaDiagonal())
            {
                FimDeJogo = true;
                Console.WriteLine($"Fim de Jogo! Vitória de {player} - {Vez}\n");
                Console.Write("Jogar outra partida? (1 - Sim / 2 - Nao)");
                continuar = int.Parse(Console.ReadLine());

                if (player == players[0].NickName)
                {
                    v1++;
                    d1++;
                }
                else if (player == players[1].NickName)
                {
                    v2++;
                    d2++;
                }
                else
                {
                    empate++;
                }

                if (continuar == 1)
                {
                    Console.Clear();
                    FimDeJogo = false;
                    Posicoes = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                    QuantidadePreenchida = 0;
                } else
                {
                    Console.Clear();
                    Menu();
                    FimDeJogo = true;
                }
            }

            if (QuantidadePreenchida == 9)
            {
                FimDeJogo = true;
                Console.WriteLine("Fim de Jogo! Deu EMPATE!\n");
                Console.Write("Jogar outra partida? (1 - Sim / 2 - Nao)");
                continuar = int.Parse(Console.ReadLine());
                if (continuar == 1)
                {
                    Console.Clear();
                    FimDeJogo = true;
                    Posicoes = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                    QuantidadePreenchida = 0;
                    Iniciar();
                }
                else
                {
                    Console.Clear();
                    Menu();
                    FimDeJogo = false;
                }
            }
        }

        private bool VitoriaDiagonal()
        {
            bool diagonal1 = Posicoes[2] == Posicoes[4] && Posicoes[2] == Posicoes[6];
            bool diagonal2 = Posicoes[0] == Posicoes[4] && Posicoes[0] == Posicoes[8];

            return diagonal1 | diagonal2;
        }

        private bool VitoriaVertical()
        {
            bool vertical1 = Posicoes[0] == Posicoes[3] && Posicoes[0] == Posicoes[6];
            bool vertical2 = Posicoes[1] == Posicoes[4] && Posicoes[1] == Posicoes[7];
            bool vertical3 = Posicoes[2] == Posicoes[5] && Posicoes[2] == Posicoes[8];

            return vertical1 | vertical2 | vertical3;
        }

        private bool VitoriaHorizontal()
        {
            bool linha1 = Posicoes[0] == Posicoes[1] && Posicoes[0] == Posicoes[2];
            bool linha2 = Posicoes[3] == Posicoes[4] && Posicoes[3] == Posicoes[5];
            bool linha3 = Posicoes[6] == Posicoes[7] && Posicoes[6] == Posicoes[8];

            return linha1 | linha2 | linha3;
        }
    }
}
