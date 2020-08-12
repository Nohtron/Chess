using System;

namespace tabuleiro.exceptions
{
    class TabuleiroException : ApplicationException
    {
        public TabuleiroException(string mensagem) : base(mensagem)
        {

        }
    }
}
