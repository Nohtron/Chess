using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "C";
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

            // Pra frente 1 e direita 2
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Pra frente 2 e direita 1
            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Pra frente 1 e esquerda 2
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Pra frente 2 e esquerda 1
            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Pra tras 1 e direita 2
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Pra tras 2 e direita 1
            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Pra tras 1 e esquerda 2
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Pra tras 2 e esquerda 1
            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            return matrizDePosicoesPossiveis;
        }
    }
}
