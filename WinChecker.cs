using System;

namespace Blackjack
{
    public static class WinChecker
    {
        private static Player player;
        private static Player dealer;


        public static void WinCheck ()
        {
            // checks if current hand busted
            if (player.GetScore(player.CurrentHand) > 21)
            {
                PlayerBustedUpdate(player.CurrentHand);
            }
            // checks if dealer busted
            else if (dealer.GetScore(dealer.CurrentHand) > 21)
            {
                DealerBustedUpdate();
            }
            // if dealer has ever made a move
            else if (dealer.GetScore(dealer.CurrentHand) > 0)
            {
                DealerVsPlayer ();
            }
        }

        public static void PlayerBustedUpdate (int hand)
        {
            Console.WriteLine("Hand " + (player.CurrentHand + 1) + " busted...");
            player.SetIsBusted (player.CurrentHand, true);
            LoseUpdate (player.CurrentHand);

            player.CurrentHand++;
            // if not more available hands
            if (player.CurrentHand >= player.TimesSplit)
            {
                Console.WriteLine("No more available hands. Ending game...");
            }
        }

        public static void DealerBustedUpdate ()
        {
            Console.WriteLine("Dealer busted!!!");
            dealer.SetIsBusted(dealer.CurrentHand, true);

            // sets all hands of player to win
            for (int hand = 0; hand < Player.MAX_HANDS; hand++)
            {
                WinUpdate (hand);
            }
        }

        public static void WinUpdate (int hand)
        {
            Console.WriteLine("Hand " + (hand + 1) + " has won!!!");
            player.Money += player.GetBet(hand) * 2;
            Console.WriteLine("Money: " + player.Money);
        }

        public static void LoseUpdate (int hand)
        {
            Console.WriteLine("Hand " + (hand + 1) + " has lost...");
            player.Money -= player.GetBet(hand);
            Console.WriteLine("Money: " + player.Money);
        }

        public static void DrawUpdate (int hand)
        {
            Console.WriteLine("Hand " + (hand + 1) + " has drawn!!!");
            player.Money += player.GetBet(hand);
            Console.WriteLine("Money: " + player.Money);
        }

        public static void DealerVsPlayer ()
        {
            for (int hand = 0; hand <= player.TimesSplit; hand++)
            {
                // checks if dealer has a greater score than player
                if (dealer.GetScore(dealer.CurrentHand) > player.GetScore(hand))
                {
                    LoseUpdate (hand);
                }
                // checks if dealer has a lesser score than player
                else if (dealer.GetScore(dealer.CurrentHand) < player.GetScore(hand))
                {
                    WinUpdate (hand);
                }
                // checks if dealer and player both tied
                else if (dealer.GetScore(dealer.CurrentHand) == player.GetScore(hand))
                {
                    DrawUpdate (hand);
                }
            }
        }

        public static Player SetPlayer
        {
            set {player = value;}
        }

        public static Player SetDealer
        {
            set {dealer = value;}
        }
    }
}