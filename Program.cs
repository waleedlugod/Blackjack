using System;

namespace Blackjack
{
    class Program
    {
        static void Setup (Player player)
        {
            Move.SetPlayer = player;
            Commentate.SetPlayer = player;
            WinChecker.SetPlayer = player;
        }
        static void Main(string[] args)
        {
            Player dealer = new Player ("Dealer");
            Player player = new Player ("Player 1");
            WinChecker.SetDealer = dealer;
            string input = "";


            do
            {
                Console.Clear();    
                player.Reset();
                Deck.Reset();

                Setup(player);
                

                Commentate.AskForBet(player.CurrentHand);
                
                // player gets 2 cards when they start
                for (int i = 0; i < 2; i++)
                {
                    Move.Hit();
                }
                player.EvaluateScore();
                Commentate.PrintScore(player.CurrentHand);
                
                // asks for move until player has either busted or turn is over
                do
                {
                    Commentate.Prompt();
                    input = Console.ReadLine();
                    input = input.ToUpper();
                    if (input == "Q" || input == "QUIT")
                    {
                        return;
                    }
                    Move.Execute(input);
                    player.EvaluateScore ();
                    Commentate.PrintScore(player.CurrentHand);

                    WinChecker.WinCheck ();
                } while (player.IsTurn || player.GetIsBusted(player.CurrentHand));

                // if player has not busted
                if (!player.GetIsBusted(player.CurrentHand))
                {
                    // dealer hits until they have a score over 17
                    while (dealer.GetScore(dealer.CurrentHand) < 17)
                    {
                        Move.SetPlayer = dealer;
                        Commentate.SetPlayer = dealer;
                        Move.Hit();
                        dealer.EvaluateScore();
                        Commentate.PrintScore(dealer.CurrentHand);
                    }

                    WinChecker.WinCheck ();
                }

                Console.WriteLine("Enter \"Q\" to QUIT or any other key to continue...");
                input = Console.ReadLine();
                input = input.ToUpper();
            } while (!(input == "Q" || input == "QUIT"));
        }
    }
}
