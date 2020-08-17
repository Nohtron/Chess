using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteUmInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool PosicaoLivre(Posicao posicao)
        {
            return Tabuleiro.Peca(posicao) == null;
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] PosicoesPossiveis()
        {
            bool[,] matrizDePosicoesPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(posicao.Linha - 2, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao) && QuantidadeDeMovimentos == 0)
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            else
            {
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(posicao.Linha + 2, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao) && QuantidadeDeMovimentos == 0)
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;
            }

            return matrizDePosicoesPossiveis;
        }
    }
}
