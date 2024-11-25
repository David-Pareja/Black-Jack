using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Transactions;

namespace Blackjack
{
    class Program
    {
        //aces can be 1 or 11 for the dealer or player 
        public static int cardtotal = 0;
        public static int firstcard = 0;
        public static int seccard = 0;
        public static int money = 2500;
        public static int playerbet = 0;

        public static int dealerFirstCard = 0;
        public static int dealerSecCard = 0;
        public static int dealerTotalCard = 0;
       

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nWelcome to Blackjack, Would you like to: ");
                Console.WriteLine("(1.) Start a Game");
                Console.WriteLine("(2.) Quit");
                string answer = Console.ReadLine();
                if (answer == "1" || answer == "one" || answer == "One")
                {
                    betAmount();
                }
                else if (answer == "2" || answer == "two" || answer == "Two")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Not an option, choose one form above");
                }
            }
        }


        public static void betAmount()
        {
            Console.WriteLine("\nMoneys: " + money);
            Console.WriteLine("How much would you like to bet:      ");

            playerbet = Int32.Parse(Console.ReadLine());

            if (playerbet == 0)
            {
                Console.WriteLine("\nInvaild bet must be greater then 0.");
                betAmount();
            }
            if (playerbet <= money)
            {
                money -= playerbet;
                Console.WriteLine("\nYou are betting: " + playerbet);

                Console.WriteLine("Drawing cards...");
                first2cards();
            }

        }
        public static void first2cards()
        {
            cardtotal = 0;
            Random rnd = new Random();
            firstcard = rnd.Next(1, 13);
            seccard = 333;

            if (firstcard + seccard > 22)
            {
                seccard = rnd.Next(1, 13);
            }

            cardtotal = (firstcard + seccard);
            Console.WriteLine("\nYou've been dealt a " + firstcard + " and a " + seccard);
            Console.WriteLine("Total card value: " + cardtotal);
            dealerFirst2cards();
        }

        public static void dealerFirst2cards()
        {
            dealerTotalCard = 0;
            Random rnd = new Random();
            dealerFirstCard = rnd.Next(1, 11);
            dealerSecCard = 333;

            if (dealerFirstCard + dealerSecCard > 22)
            {
                dealerSecCard = rnd.Next(1, 11);
            }

            dealerTotalCard = (dealerFirstCard + dealerSecCard);
            StartBlackJack();
        }
        static void StartBlackJack()
        {
            if (cardtotal < 21)
            {
                // Console.WriteLine("\nDealers total card value:    " + dealerTotalCard);
                Console.WriteLine("Dealers cards:   HIDDEN and " + dealerSecCard);
                Console.WriteLine("Hit or Stand");
                string choice = Console.ReadLine();

                if (choice == "hit" || choice == "Hit" || choice == "h")
                {
                    AddCard();
                }
                else if (choice == "stand" || choice == "Stand" || choice == "s")
                {
                    StandDealerPull();
                }
            }
        }
        static void AddCard()
        {
            {
                Console.WriteLine("\nHitting...");
                Random rnd = new Random();

                int dealedCard = rnd.Next(1, 13);
                Console.WriteLine("You've been dealt a " + dealedCard);
                cardtotal += dealedCard;
                Console.WriteLine("Total card value: " + cardtotal);
                if (cardtotal == 21)
                {
                    
                    money += (playerbet * 2);
                    Console.WriteLine("BLACKJACK YOU WIN" + playerbet + "\nNew Amount  " + money );
                    
                }
                else if (cardtotal > 21)
                {
                    Console.WriteLine("\nBusted You lose");
                    playerbet = 0;
                    betAmount();
                }
                else if (cardtotal < 21)
                {
                    StartBlackJack();
                }
            }
        }

        static void StandDealerPull()
        {
            Console.WriteLine("\nStand");
            Random rnd = new Random();

            while (cardtotal > dealerTotalCard)
            {
                int dealerCard = rnd.Next(1, 11);
                dealerTotalCard += dealerCard;

                Console.WriteLine("\nDealers pulls a ..." + dealerCard + "\nNew Total: " + dealerTotalCard);
                if (dealerTotalCard == 21)
                {
                    Console.WriteLine("Dealer pulled a BLACKAJCK");
                    if (dealerTotalCard == cardtotal)
                    {
                        Console.WriteLine("\nPUSH");
                        money += playerbet;
                    }
                    else
                    {
                        Console.WriteLine("\nPlayer Loses to the black jack");
                        playerbet = 0;
                        betAmount();
                    }
                }
                if (dealerTotalCard > 21)
                {
                    Console.WriteLine("Dealer Busted. You win.");
                    money += playerbet * 2;
                    betAmount();
                }
            
            }
            Console.WriteLine("Dealer Total: " + dealerTotalCard);

        }
    }
}