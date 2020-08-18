using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez PartidaDeXadrez;

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partidaDeXadrez) : base(tabuleiro, cor)
        {
            PartidaDeXadrez = partidaDeXadrez;
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
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao) && QuantidadeDeMovimentos == 0)
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                // # JogadaEspecial - En Passant
                if(Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteUmInimigo(esquerda) && Tabuleiro.Peca(esquerda) == PartidaDeXadrez.PossivelEnPassant)
                        matrizDePosicoesPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteUmInimigo(direita) && Tabuleiro.Peca(direita) == PartidaDeXadrez.PossivelEnPassant)
                        matrizDePosicoesPossiveis[direita.Linha - 1, direita.Coluna] = true;
                }

            }
            else
            {
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && PosicaoLivre(posicao) && QuantidadeDeMovimentos == 0)
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteUmInimigo(posicao))
                    matrizDePosicoesPossiveis[posicao.Linha, posicao.Coluna] = true;

                // # JogadaEspecial - En Passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteUmInimigo(esquerda) && Tabuleiro.Peca(esquerda) == PartidaDeXadrez.PossivelEnPassant)
                        matrizDePosicoesPossiveis[esquerda.Linha + 1, esquerda.Coluna] = true;

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && ExisteUmInimigo(direita) && Tabuleiro.Peca(direita) == PartidaDeXadrez.PossivelEnPassant)
                        matrizDePosicoesPossiveis[direita.Linha + 1, direita.Coluna] = true;
                }


            }

            return matrizDePosicoesPossiveis;
        }
    }
}
