using System;
using System.Threading;

namespace Blackjack
{
    static class Commentate
    {
        private static Player player;


        public static void Rules ()
        {
            Console.WriteLine(
                "At the beginning of the game you are given 2 cards.\n"
                + "Your goal is to get as close to 21 as possible without going over.\n"
                + "You can choose to:\n"
                + "\tH - HIT to get an additional card\n"
                + "\tST - STAND to confirm your hand\n"
                + "\tD - DOUBLE your bet and recieve one last card or\n"
                + "\tSP - SPLIT your hand if you only have 2 cards of the same rank but of different suit.\n"
                + "Additionally all face cards (Joker, Queen, King) have a value of 10.\n"
                + "The Ace card has either the value of 11 or 1, depending on which is more beneficial.\n"
                + "Enjoy the game!\n"
            );
            
            
        }

        public static void Prompt ()
        {
            Console.Write(
                "Choose any of the following options:\n"
                + "\tH - HIT to get an additional card\n"
                + "\tST - STAND to confirm your hand\n"
                + "\tD - DOUBLE your bet and recieve one last card or\n"
                + "\tSP - SPLIT your hand if you only have 2 cards of the same rank but of different suit.\n"
                + "\tHND - to see your HANDS\n"
                + "\tR - to see RULES\n"
                + "\tQ - QUIT\n"
            );
        }

        public static void ShowHands ()
        {
            string str = "";

            // this block of code is really messy but deal with it
            // loops through hands
            for (int hand = 0; hand < Player.MAX_HANDS; hand++)
            {
                bool isPrintedHand = false;
                // loops through suits
                for (int suit = 0; suit < Deck.MAX_SUITS; suit++)
                {
                    bool isPrintedSuit = false;
                    // loops through ranks
                    for (int rank = 1; rank < Deck.MAX_RANKS; rank++)
                    {
                        // if card is present
                        if (player.IsHasCard (hand, (Suit) suit, rank) == true)
                        {
                            if (!isPrintedHand)
                            {
                                str += "Hand " + (hand + 1) + ":";
                                isPrintedHand = true;
                            }

                            if (!isPrintedSuit)
                            {
                                str += "\n" + (Suit) suit + ": ";
                                isPrintedSuit = true;
                            }

                            switch (rank)
                            {
                                case 1:
                                    str += "Ace" + " ";
                                    break;
                                case 11:
                                    str += "Joker" + " ";
                                    break;
                                case 12:
                                    str += "Queen" + " ";
                                    break;
                                case 13:
                                    str += "King" + "";
                                    break;
                                default:
                                    str += rank + " ";
                                    break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(str);
        }

        public static void PrintScore (int hand)
        {
            Console.WriteLine(
                player.UserName
                + " has now has a score of "
                + player.GetScore(hand)
                + " in hand "
                + (hand + 1)
                + "\n"
            );

            Thread.Sleep(1000);
        }

        public static void AskForBet (int hand)
        {
            string input;
            int bet;
            bool validInput = false;

            Console.WriteLine("Money: " + player.Money);
            do
            {
                Console.Write("Please enter bet for hand " + (hand + 1) + ": ");
                input = Console.ReadLine();
                
                validInput = int.TryParse(input, out bet);
                if ((!validInput) || (bet <= 0) || (bet > player.Money))
                {
                    Console.WriteLine("That is not a valid input...");
                }
            } while (!validInput);
            Console.WriteLine();

            player.Money -= bet;
            player.SetBet(hand, bet);
        }
        public static void PullCard (Suit suit, int rank)
        {
            string rankString = "";
            switch (rank)
            {
                case 1:
                    rankString = "Ace";
                    break;
                case 11:
                    rankString = "Joker";
                    break;
                case 12:
                    rankString = "Queen";
                    break;
                case 13:
                    rankString = "King";
                    break;
                default:
                    rankString += rank;
                    break;
            }

            Console.WriteLine(player.UserName + " pulled a(n) " + rankString + " of " + suit);
            Thread.Sleep(1000);
        }

        public static void Hit ()
        {
            Console.WriteLine(player.UserName + " chose to hit!");
        }

        public static void Stand ()
        {
            Console.WriteLine(player.UserName + " chose to stand!");
        }

        public static void Double ()
        {
            Console.WriteLine(player.UserName + " chose to double their bet!");
        }

        public static void Split ()
        {
            Console.WriteLine(player.UserName + " chose to split their hand!");
        }

        public static Player SetPlayer
        {
            set {player = value;}
        }
    }
}