using System.Collections.Generic;
using tabuleiro;
using tabuleiro.enums;
using tabuleiro.exceptions;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor CorDoJogadorAtual { get; private set; }
        public bool FimDeJogo { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> PecasCapturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            CorDoJogadorAtual = Cor.Branca;
            FimDeJogo = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementaQuantidadeDeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
                PecasCapturadas.Add(pecaCapturada);

            // JogadaEspecial - Roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao posicaoOrigemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao posicaoDestinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoOrigemDaTorre);
                torre.IncrementaQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoDestinoDaTorre);
            }

            // JogadaEspecial - Roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao posicaoOrigemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao posicaoDestinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoOrigemDaTorre);
                torre.IncrementaQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoDestinoDaTorre);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementaQuantidadeDeMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(peca, origem);

            // JogadaEspecial - Roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao posicaoOrigemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao posicaoDestinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoDestinoDaTorre);
                torre.DecrementaQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoOrigemDaTorre);
            }

            // JogadaEspecial - Roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao posicaoOrigemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao posicaoDestinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoDestinoDaTorre);
                torre.DecrementaQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoOrigemDaTorre);
            }
        }


        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXequeRei(CorDoJogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }

            if (EstaEmXequeRei(CorAdversaria(CorDoJogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (testeXequemate(CorAdversaria(CorDoJogadorAtual)))
                FimDeJogo = true;
            else
            {
                Turno++;
                MudaJogadorAtual();
            }


        }

        public void ValidarPosicaoDeOrigem(Posicao posicao)
        {
            if (Tabuleiro.Peca(posicao) == null)
                throw new TabuleiroException($"Não existe peça na posição {PosicaoXadrez.Parse(posicao)}");

            if (CorDoJogadorAtual != Tabuleiro.Peca(posicao).Cor)
                throw new TabuleiroException($"A peça de origem neste turno deve ser {CorDoJogadorAtual}");

            if (!Tabuleiro.Peca(posicao).ExisteMovimentosPossiveis())
                throw new TabuleiroException($"Não existem movimentos possíveis para a peça na posição {PosicaoXadrez.Parse(posicao)}");
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException($"Posição de destino {PosicaoXadrez.Parse(destino)} não é um movimento válido para a peça em {PosicaoXadrez.Parse(origem)}");
        }

        public void MudaJogadorAtual()
        {
            if (CorDoJogadorAtual == Cor.Branca)
                CorDoJogadorAtual = Cor.Preta;
            else
                CorDoJogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadasDaCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in PecasCapturadas)
            {
                if (peca.Cor == cor)
                    aux.Add(peca);
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogoDaCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in Pecas)
            {
                if (peca.Cor == cor)
                    aux.Add(peca);
            }
            aux.ExceptWith(PecasCapturadasDaCor(cor));
            return aux;
        }

        private Cor CorAdversaria(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;
            else
                return Cor.Branca;
        }

        private Peca GetRei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogoDaCor(cor))
            {
                if (peca is Rei)
                    return peca;
            }
            return null;
        }

        public bool EstaEmXequeRei(Cor cor)
        {
            Peca reiJogadorAtual = GetRei(cor);
            if (reiJogadorAtual == null)
                throw new TabuleiroException($"Não existe Rei da cor {cor} no tabuleiro.");

            foreach (Peca peca in PecasEmJogoDaCor(CorAdversaria(cor)))
            {
                bool[,] matrizDeMovimentosPossiveis = peca.PosicoesPossiveis();
                if (matrizDeMovimentosPossiveis[reiJogadorAtual.Posicao.Linha, reiJogadorAtual.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!EstaEmXequeRei(cor))
                return false;

            foreach (Peca peca in PecasEmJogoDaCor(cor))
            {
                bool[,] matrizDeMovimentosPossiveis = peca.PosicoesPossiveis();
                for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                    {
                        if (matrizDeMovimentosPossiveis[linha, coluna])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(linha, coluna);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXequeRei(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta));

        }


    }
}
