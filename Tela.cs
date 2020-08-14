using System;
using tabuleiro;
using tabuleiro.enums;
using xadrez;

namespace xadrez_console
{
    class Tela
    {

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write((8 - linha) + " ");
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    if (tabuleiro.Peca(linha, coluna) == null)
                        Console.Write("- ");
                    else
                    {
                        ImprimirPeca(tabuleiro.Peca(linha, coluna));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
                Console.Write(peca);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string posicaoString = Console.ReadLine();
            char coluna = posicaoString[0];
            int linha = int.Parse(posicaoString[1].ToString());
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
