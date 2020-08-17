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

        public void DecrementaQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matrizDeMovimentosPossiveis = PosicoesPossiveis();

            for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
            {
                for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                {
                    if (matrizDeMovimentosPossiveis[linha, coluna])
                        return true;
                }
            }
            return false;
        }

        public bool PodeMoverPara(Posicao posicao)
        {
            return PosicoesPossiveis()[posicao.Linha, posicao.Coluna];
        }


        public abstract bool[,] PosicoesPossiveis();
    }
}
