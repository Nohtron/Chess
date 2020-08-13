using System;
using tabuleiro;
using tabuleiro.enums;
using tabuleiro.exceptions;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez('a', 1);

            Console.WriteLine(posicaoXadrez);

            Console.WriteLine(posicaoXadrez.ToPosicao());

            Console.ReadLine();
        }
    }
}
