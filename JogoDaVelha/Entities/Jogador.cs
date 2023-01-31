
namespace HubJogos.Entities
{
    public class Jogador
    {
        public string NickName { get; set; }
        public string Senha { get; set; }

        public Jogador() { }

        public Jogador(string nickName, string senha)
        {
            NickName = nickName;
            Senha = senha;
        }
    }
}
