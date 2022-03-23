using System;
using tabuleiro;
using tabuleiro.exceptions;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPosicao()
        {
            if (Linha > 8 || Coluna > 'h' || Linha < 1 || Coluna < 'a')
            {
                throw new TabuleiroException("Posição digitada inválida");
            }
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public static PosicaoXadrez Parse(Posicao posicao)
        {
            return new PosicaoXadrez((char)(posicao.Coluna + 'a'), 8 - posicao.Linha);
        }

        public override string ToString()
        {
            return $"{Coluna}{Linha}";
        }
    }
}
