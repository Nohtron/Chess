using tabuleiro;

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
