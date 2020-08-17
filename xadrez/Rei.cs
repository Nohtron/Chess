using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez PartidaDeXadrez;

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partidaDeXadrez) : base(tabuleiro, cor)
        {
            PartidaDeXadrez = partidaDeXadrez;
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

        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QuantidadeDeMovimentos == 0;
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

            // #JogadaEspecial - ROQUE
            if (QuantidadeDeMovimentos == 0 && !PartidaDeXadrez.Xeque)
            {
                // # JogadaEspecial - Roque Pequeno
                Posicao posicaoTorrePerto = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoTorrePerto))
                {
                    Posicao posicao1EntreReiETorre = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao posicao2EntreReiETorre = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tabuleiro.Peca(posicao1EntreReiETorre) == null && Tabuleiro.Peca(posicao2EntreReiETorre) == null)
                        matrizDePosicoesPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
                }
                // # JogadaEspecial - Roque Grande
                Posicao posicaoTorreLonge = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoTorreLonge))
                {
                    Posicao posicao1EntreReiETorre = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao posicao2EntreReiETorre = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posicao3EntreReiETorre = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tabuleiro.Peca(posicao1EntreReiETorre) == null && Tabuleiro.Peca(posicao2EntreReiETorre) == null && Tabuleiro.Peca(posicao3EntreReiETorre) == null)
                        matrizDePosicoesPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }


            return matrizDePosicoesPossiveis;
        }
    }
}
