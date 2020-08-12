using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    if (tabuleiro.Peca(linha, coluna) == null)
                        Console.Write("- ");
                    else
                        Console.Write(tabuleiro.Peca(linha, coluna) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
