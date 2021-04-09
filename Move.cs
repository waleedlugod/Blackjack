using System;

namespace Blackjack
{
    public static class Move
    {
        private static Player player;
        private static Random rnd = new Random ();


        public static void Execute (string move)
        {
            move = move.ToUpper();
            switch (move)
            {
                case "H":
                case "HIT":
                    Commentate.Hit();
                    Hit();
                    break;
                case "ST":
                case "STAND":
                    Commentate.Stand();
                    Stand();
                    break;
                case "D":
                case "DOUBLE":
                    Commentate.Double();
                    Double();
                    break;
                case "SP":
                case "SPLIT":
                    if (IsCanSplit())
                    {
                        Commentate.Split();
                        Split();
                        Commentate.AskForBet(player.CurrentHand + 1);
                    }
                    else
                    {
                        Console.WriteLine("You cannot split your hand right now...");
                    }
                    break;
                case "R":
                case "RULES":
                    Commentate.Rules();
                    break;
                case "HND":
                case "HANDS":
                    Commentate.ShowHands();
                    break;
                default:
                    Console.WriteLine("That is not a valid input...");
                    break;
            }
        }
        public static void Hit ()
        {
            Suit suit; // suit of card
            int rank; // rank or number of card

            // get the suit and rank of a random available card
            do
            {
                suit = (Suit) rnd.Next (0, 3);
                rank = rnd.Next (1, 13);
            } while (!Deck.IsCardAvailable(suit, rank));

            player.PullCard (player.CurrentHand, suit, rank);
        }

        public static void Stand ()
        {
            // if player used all their hands or has split their hand
            if ((player.CurrentHand >= Player.MAX_HANDS) || (player.CurrentHand >= player.TimesSplit))
            {
                player.IsTurn = false;
            }

            // if player has split their hand
            if (player.CurrentHand < player.TimesSplit)
            {
                player.CurrentHand += 1;
            }
        }

        public static void Double ()
        {
            // double the bet of the current hand
            player.SetBet (player.CurrentHand, player.GetBet(player.CurrentHand) * 2);
            Hit ();
            player.IsTurn = false;
        }

        public static void Split ()
        {
            player.TimesSplit++;

            for (int hand = 0; hand < Player.MAX_HANDS; hand++)
            {
                for (int suit = 0; suit < Deck.MAX_SUITS; suit++)
                {
                    for (int rank = 0; rank < Deck.MAX_RANKS; rank++)
                    {
                        // if card is present in current hand
                        if (player.IsHasCard (hand, (Suit) suit, rank) == true)
                        {
                            // takes card of current hand and moves it to the next hand
                            player.SetCard (hand, (Suit) suit, rank, false);
                            player.SetCard (hand + 1, (Suit) suit, rank, true);

                            return;
                        }
                    }
                }
            }
        }

        public static bool IsCanSplit ()
        {
            int cards = 0;
            bool isHasMatchingCard = false;


            for (int suit = 0; suit < Deck.MAX_SUITS; suit++)
            {
                for (int rank = 1; rank < Deck.MAX_RANKS; rank++)
                {
                    if (player.IsHasCard(player.CurrentHand, (Suit) suit, rank))
                    {
                        cards++;
                        
                        for (int i = 1; i < Deck.MAX_SUITS; i++)
                        {
                            // checks if there is a matching rank in any of the other suits
                            if (player.IsHasCard(player.CurrentHand, (Suit) ((suit + i) % 4), rank))
                            {
                                cards++;
                                isHasMatchingCard = true;
                            }
                        }
                    }
                }
            }

            // if player has more than 2 cards in hand or doesnt have a matching card
            if (cards > 2 || !isHasMatchingCard)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Player SetPlayer
        {
            set {player = value;}
        }
    }
}