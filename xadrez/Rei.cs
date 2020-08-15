using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "R";
        }

        private bool PodeMover(Posicao posicaoDestino)
        {
            Peca peca = Tabuleiro.Peca(posicaoDestino);

            return (peca == null || peca.Cor != Cor);
        }

        public override bool[,] PosicoesPossiveis()
        {
            bool[,] matrizDePosicoesPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // acima e direita
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // abaixo e direita
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // abaixo e esquerda
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            // acima e esquerda
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

            return matrizDePosicoesPossiveis;
        }
    }
}
