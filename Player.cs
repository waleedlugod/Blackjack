namespace Blackjack
{
    public interface IPlayer
    {
        public void EvaluateScore ();
        public void PullCard(int hand, Suit suit, int rank);
        public void Reset ();

        public bool IsHasCard (int hand, Suit suit, int rank);
        public int GetScore (int hand);
        public int GetBet (int hand);
        public bool GetIsBusted (int hand);
        public string UserName {get;}
        public void SetBet (int hand, int bet);
        public void SetScore (int hand, int score);
        public bool SetIsBusted (int hand, bool value);
        public int Money {get; set;}
        public int CurrentHand {get; set;}
        public int TimesSplit {get; set;}
        public bool IsTurn {get; set;}
    }
    public class Player : IPlayer
    {
        public const int MAX_HANDS = 2;
        private bool [,,] hands = new bool [MAX_HANDS, Deck.MAX_SUITS, Deck.MAX_RANKS];
        private string userName;
        private int [] score = new int [MAX_HANDS];
        private int [] bet = new int [MAX_HANDS];
        private bool [] isBusted = new bool [MAX_HANDS];
        private int money = 1000;
        private int currentHand = 0;
        private int timesSplit = 0;
        private bool isTurn = true;

        
        public Player (string name)
        {
            this.userName = name;
        }

        public void EvaluateScore ()
        {
            for (int hand = 0; hand < Player.MAX_HANDS; hand++)
            {
                SetScore(hand, 0);
                for (int suit = 0; suit < Deck.MAX_SUITS; suit++)
                {
                    for (int rank = 1; rank < Deck.MAX_RANKS; rank++)
                    {
                        if (IsHasCard(hand, (Suit) suit, rank))
                        {
                            // adds score with corresponding value of card
                            switch (rank)
                            {
                                case 1: // ace
                                    SetScore(hand, GetScore(hand) + 11);
                                    break;
                                case 11: // joker
                                case 12: // queen
                                case 13: // king
                                    SetScore(hand, GetScore(hand) + 10);
                                    break;
                                default:
                                    SetScore(hand, GetScore(hand) + rank);
                                    break;
                            }

                            // reevaluates value of aces
                            if (GetScore(hand) > 21)
                            {
                                for (int i = 0; i < Deck.MAX_SUITS; i++)
                                {
                                    // if has ace
                                    if (IsHasCard(hand, (Suit) i, 1))
                                    {
                                        // sets value of this ace card from 11 to 1 by subtracting 10
                                        SetScore (hand, GetScore(hand) - 11);
                                        if (GetScore(hand) <= 21)
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void PullCard (int hand, Suit suit, int rank)
        {
            
            // marks card as present in hand
            this.hands [hand, (int) suit, rank] = true;

            // marks card as used
            Deck.MarkCardAsUsed (suit, rank);

            Commentate.PullCard (suit, rank);
        }

        public void Reset ()
        {
            TimesSplit = 0;
            CurrentHand = 0;
            IsTurn = true;
            
            for (int hand = 0; hand < MAX_HANDS; hand++)
            {
                SetScore (hand, 0);
                SetBet (hand, 0);
                for (int suit = 0; suit < Deck.MAX_SUITS; suit++)
                {
                    for (int rank = 1; rank < Deck.MAX_RANKS; rank++)
                    {
                        SetCard (hand, (Suit) suit, rank, false);
                    }
                }
            }
        }

        public bool IsHasCard (int hand, Suit suit, int rank)
        {
            return this.hands [hand, (int) suit, rank];
        }

        public int GetScore (int hand)
        {
            return this.score [hand];
        }
        public int GetBet (int hand)
        {
            return this.bet [hand];
        }

        public bool GetIsBusted (int hand)
        {
            return this.isBusted [hand];
        }

        public string UserName
        {
            get {return this.userName;}
        }

        public void SetBet (int hand, int bet)
        {
            this.bet [hand] = bet;
        }

        public void SetScore (int hand, int score)
        {
            this.score [hand] = score;
        }

        public bool SetIsBusted (int hand, bool value)
        {
            return this.isBusted [hand] = value;
        }

        public int Money
        {
            get {return this.money;}
            set {this.money = value;}
        }

        public void SetCard (int hand, Suit suit, int rank, bool value)
        {
            this.hands [hand, (int) suit, rank] = value;
        }

        public int CurrentHand
        {
            get {return this.currentHand;}
            set {this.currentHand = value;}
        }

        public int TimesSplit
        {
            get {return this.timesSplit;}
            set {this.timesSplit = value;}
        }

        public bool IsTurn
        {
            get {return this.isTurn;}
            set {this.isTurn = value;}
        }
    }
}