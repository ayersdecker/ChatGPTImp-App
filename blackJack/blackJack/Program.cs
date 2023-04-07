namespace blackJack
{
    using System;

    public class Program
    {
        public static void Main()
        {
            // Create a new deck of cards
            Deck deck = new Deck();
            deck.Shuffle();

            // Create the player and dealer
            Player player = new Player();
            Dealer dealer = new Dealer();

            // Deal 2 cards to each
            player.Hit(deck.Deal());
            player.Hit(deck.Deal());

            dealer.Hit(deck.Deal());
            dealer.Hit(deck.Deal());

            // Show the player's cards
            Console.WriteLine("Player's Cards:");
            foreach (Card card in player.Cards)
            {
                Console.WriteLine(card.ToString());
            }
            Console.WriteLine("Total: " + player.GetScore());

            // Show the dealer's cards
            Console.WriteLine("\nDealer's Cards:");
            foreach (Card card in dealer.Cards)
            {
                Console.WriteLine(card.ToString());
            }
            Console.WriteLine("Total: " + dealer.GetScore());

            // Check if the player has blackjack
            if (player.HasBlackjack())
            {
                Console.WriteLine("\nPlayer has Blackjack!");
                if (dealer.HasBlackjack())
                {
                    Console.WriteLine("Dealer has Blackjack too. Push!");
                }
                else
                {
                    Console.WriteLine("Player wins!");
                }
            }
            else
            {
                // Player's turn
                while (true)
                {
                    Console.WriteLine("\nHit or stand (H/S):");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "h")
                    {
                        player.Hit(deck.Deal());
                        Console.WriteLine("You drew a " + player.Cards[player.Cards.Count - 1].ToString());
                        Console.WriteLine("Total: " + player.GetScore());

                        if (player.IsBust())
                        {
                            Console.WriteLine("You busted!");
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Dealer's turn
                while (true)
                {
                    if (dealer.GetScore() < 17)
                    {
                        dealer.Hit(deck.Deal());
                        Console.WriteLine("\nDealer drew a " + dealer.Cards[dealer.Cards.Count - 1].ToString());
                        Console.WriteLine("Total: " + dealer.GetScore());

                        if (dealer.IsBust())
                        {
                            Console.WriteLine("Dealer busted!");
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Determine the winner
                if (player.IsBust())
                {
                    Console.WriteLine("Dealer wins!");
                }
                else if (dealer.IsBust())
                {
                    Console.WriteLine("Player wins!");
                }
                else if (player.GetScore() > dealer.GetScore())
                {
                    Console.WriteLine("Player wins!");
                }
                else if (player.GetScore() < dealer.GetScore())
                {
                    Console.WriteLine("Dealer wins!");
                }
                else
                {
                    Console.WriteLine("Push!");
                }
            }
        }
    }

    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 1; value <= 13; value++)
                {
                    cards.Add(new Card((Suit)suit, (Value)value));
                }
            }
        }

        public Card Deal()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public void Shuffle()
        {
            Random random = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = random.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Value Value { get; set; }

        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            return Value + " of " + Suit;
        }
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum Value
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public class Player
    {
        public List<Card> Cards { get; set; }

        public Player()
        {
            Cards = new List<Card>();
        }

        public void Hit(Card card)
        {
            Cards.Add(card);
        }

        public int GetScore()
        {
            int score = 0;
            int aces = 0;

            foreach (Card card in Cards)
            {
                if (card.Value == Value.Ace)
                {
                    aces++;
                }
                else if (card.Value >= Value.Ten)
                {
                    score += 10;
                }
                else
                {
                    score += (int)card.Value;
                }
            }

            while (aces > 0)
            {
                if (score + 11 > 21)
                {
                    score++;
                }
                else
                {
                    score += 11;
                }
                aces--;
            }

            return score;
        }

        public bool HasBlackjack()
        {
            return (Cards.Count == 2 && GetScore() == 21);
        }

        public bool IsBust()
        {
            return (GetScore() > 21);
        }
    }

    public class Dealer
    {
        public List<Card> Cards { get; set; }

        public Dealer()
        {
            Cards = new List<Card>();
        }

        public void Hit(Card card)
        {
            Cards.Add(card);
        }

        public int GetScore()
        {
            int score = 0;
            int aces = 0;

            foreach (Card card in Cards)
            {
                if (card.Value == Value.Ace)
                {
                    aces++;
                }
                else if (card.Value >= Value.Ten)
                {
                    score += 10;
                }
                else
                {
                    score += (int)card.Value;
                }
            }

            while (aces > 0)
            {
                if (score + 11 > 21)
                {
                    score++;
                }
                else
                {
                    score += 11;
                }
                aces--;
            }

            return score;
        }

        public bool HasBlackjack()
        {
            return (Cards.Count == 2 && GetScore() == 21);
        }

        public bool IsBust()
        {
            return (GetScore() > 21);
        }
    }
}