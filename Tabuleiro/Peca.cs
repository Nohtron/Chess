using tabuleiro.enums;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QuantidadeDeMovimentos = 0;
        }

        public void IncrementaQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos++;
        }

        public abstract bool[,] PosicoesPossiveis();
    }
}
