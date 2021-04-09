namespace Blackjack
{
    public static class Deck
    {
        public const int MAX_SUITS = 4;
        public const int MAX_RANKS = 14; // 0 is left alone
        private static bool [,] usedCards = new bool[MAX_SUITS, MAX_RANKS];


        public static void Reset ()
        {
            for (int suit = 0; suit < MAX_SUITS; suit++)
            {
                for (int rank = 1; rank < MAX_RANKS; rank++)
                {
                    usedCards [suit, rank] = false;
                }
            }
        }

        public static bool IsCardAvailable (Suit suit, int rank)
        {
            // returns true if card is not used
            return !usedCards [(int) suit, rank];
        }

        public static void MarkCardAsUsed (Suit suit, int rank)
        {
            usedCards [(int) suit, rank] = true;
        }
    }
}