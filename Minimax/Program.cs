using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniMax_Nim_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialPiles = new List<int> { 3, 4, 5 };
            bool botStarts = true;

            var game = new NimGame(initialPiles, botStarts);
            GameState state;

            do
            {
                state = game.PlayTurn();
            } while (state == GameState.Ongoing);


            if (state == GameState.BotWon)
                Console.WriteLine("Vyhrál počítač!");
            else
                Console.WriteLine("Gratulujeme! Vyhráli jste!");
        }
    }

    public enum GameState
    {
        Ongoing,
        BotWon,
        HumanWon
    }

    public class NimGameState
    {
        public List<int> Piles { get; private set; }
        public int MatchesInGame { get; private set; }

        public NimGameState(List<int> initialPiles)
        {
            Piles = new List<int>(initialPiles);
            MatchesInGame = Piles.Sum();
        }

        public void MakeMove(int pileIndex, byte matchesToRemove)
        {
            if (IsValidMove(pileIndex, matchesToRemove))
            {
                Piles[pileIndex] -= matchesToRemove;
                MatchesInGame -= matchesToRemove;
            }
            else
            {
                throw new ArgumentException("Neplatný tah!");
            }
        }

        private bool IsValidMove(int pileIndex, byte matchesToRemove)
        {
            return pileIndex >= 0 &&
                   pileIndex < Piles.Count &&
                   Piles[pileIndex] >= matchesToRemove &&
                   matchesToRemove > 0;
        }
    }

    public class NimGame
    {
        private NimGameState _state; // pro privátní datové položky používáme podtržítko na začátku jména
        private bool _botStarts;
        private bool _isBotTurn;

        public NimGame(List<int> initialPiles, bool botStarts)
        {
            _state = new NimGameState(initialPiles);
            _botStarts = botStarts;
            _isBotTurn = botStarts;
        }

        public GameState PlayTurn()
        {
            PrintGameState();

            if (_isBotTurn)
            {
                var botMove = GetBestBotMove();
                MakeAndPrintBotMove(botMove);
            }
            else
            {
                var humanMove = GetHumanInput();
                _state.MakeMove(humanMove.Item1, humanMove.Item2);
            }

            _isBotTurn = !_isBotTurn;

            if (_state.MatchesInGame == 0)
                if (_isBotTurn)
                    return GameState.BotWon;
                else
                    return GameState.HumanWon;
            else
                return GameState.Ongoing;
        }

        private Tuple<int, byte> GetBestBotMove()
        {
            int bestPile = 0; // budoucí nejlepší hromádka k odebírání sirek
            byte matchesToRemove = 1; // budoucí nejlepší momentální počet k odebrání

            int minimax(List<int> piles, int depth, bool maximizingPlayer, int alpha, int beta)
            {
                int totalMatches = piles.Sum();

                // Terminální stav - žádné sirky
                if (totalMatches == 0)
                {
                    // Pokud již není co brát, VYHRÁVÁ hráč, který je na tahu (Misère Nim)
                    return maximizingPlayer ? 1000 : -1000;
                }

                // BONUS: Ohodnocovací funkce v omezené hloubce (nedostaneme se až do listu)
                if (depth == 0)
                {
                    // Ohodnocovací funkce: použijeme Nim-sum (XOR)
                    int nimSum = 0;
                    foreach (var pile in piles)
                        nimSum ^= pile;

                    // V Misère Nim: nim-sum 0 je špatná pozice
                    return nimSum == 0 ? (maximizingPlayer ? -10 : 10) : (maximizingPlayer ? 10 : -10);
                }

                if (maximizingPlayer)
                {
                    int maxEval = int.MinValue;

                    // Procházíme všechny možné tahy
                    for (int pileIndex = 0; pileIndex < piles.Count; pileIndex++)
                    {
                        for (byte matches = 1; matches <= Math.Min(piles[pileIndex], 2); matches++)
                        {
                            if (piles[pileIndex] >= matches)
                            {
                                // Provedeme tah
                                var newPiles = new List<int>(piles);
                                newPiles[pileIndex] -= matches;

                                // Rekurzivně zavoláme minimax s alfa-beta prořezáváním
                                int eval = minimax(newPiles, depth - 1, false, alpha, beta);

                                // Aktualizujeme nejlepší tah (pouze na nejvyšší úrovni)
                                if (eval > maxEval)
                                {
                                    maxEval = eval;
                                    if (depth == 10) // pouze na nejvyšší úrovni
                                    {
                                        bestPile = pileIndex;
                                        matchesToRemove = matches;
                                    }
                                }

                                // BONUS: Alfa-beta prořezávání
                                alpha = Math.Max(alpha, eval);
                                if (beta <= alpha)
                                    break; // Beta cut-off
                            }
                        }
                        if (beta <= alpha)
                            break; // Beta cut-off
                    }

                    return maxEval;
                }
                else
                {
                    int minEval = int.MaxValue;

                    // Procházíme všechny možné tahy
                    for (int pileIndex = 0; pileIndex < piles.Count; pileIndex++)
                    {
                        for (byte matches = 1; matches <= Math.Min(piles[pileIndex], 2); matches++)
                        {
                            if (piles[pileIndex] >= matches)
                            {
                                // Provedeme tah
                                var newPiles = new List<int>(piles);
                                newPiles[pileIndex] -= matches;

                                // Rekurzivně zavoláme minimax s alfa-beta prořezáváním
                                int eval = minimax(newPiles, depth - 1, true, alpha, beta);
                                minEval = Math.Min(minEval, eval);

                                // BONUS: Alfa-beta prořezávání
                                beta = Math.Min(beta, eval);
                                if (beta <= alpha)
                                    break; // Alpha cut-off
                            }
                        }
                        if (beta <= alpha)
                            break; // Alpha cut-off
                    }

                    return minEval;
                }
            }

            // Zavoláme minimax s počáteční hloubkou a alfa-beta parametry
            minimax(_state.Piles.ToList(), 10, true, int.MinValue, int.MaxValue);

            return new Tuple<int, byte>(bestPile, matchesToRemove);
        }

        private void PrintGameState()
        {
            Console.WriteLine("Aktuální stav hry:");
            foreach (var pile in _state.Piles)
                Console.Write(pile + " ");
            Console.WriteLine();
        }

        private void MakeAndPrintBotMove(Tuple<int, byte> move)
        {
            _state.MakeMove(move.Item1, move.Item2);
            Console.WriteLine($"Počítač bere {move.Item2} sirky z hromádky {move.Item1}");
        }

        private Tuple<int, byte> GetHumanInput()
        {

            Console.Write("Z které hromádky chcete brát? (");
            for (int i = 0; i < _state.Piles.Count; i++)
            {
                if (_state.Piles[i] > 0)
                    Console.Write($"{i} ");
            }
            Console.Write(")");

            int pileIndex = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Kolik sirek chcete vzít? (1-{_state.Piles[pileIndex]})");
            byte matches = Convert.ToByte(Console.ReadLine());

            return new Tuple<int, byte>(pileIndex, matches);
        }
    }


}
