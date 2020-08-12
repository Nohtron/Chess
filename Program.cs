using System;
using Tabuleiro;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(3, 5);

            Console.WriteLine($"Posição: {p}");

            Console.ReadLine();
        }
    }
}
