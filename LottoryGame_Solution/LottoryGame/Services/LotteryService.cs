using LottoryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoryGame.Services
{
    public interface ILotteryService
    {
        void StartGame();
        void DrawResults();
        void GenerateCPUPlayer();
        void PurchaseTickets(Player player, int numberOfTickets);
    }

    public class LotteryService : ILotteryService
    {

        private readonly LotteryConfig _Config;
        private Random _rand = new Random();
        public List<Player> Players { get; set; }

        public decimal HouseRevenue { get; set; }

        public LotteryService(LotteryConfig lotteryConfig, Random random )
        {
            Players = new List<Player>();
            _rand = random ?? throw new ArgumentNullException(nameof(random)) ;
            _Config = lotteryConfig ?? throw new ArgumentNullException(nameof(lotteryConfig));
            HouseRevenue = 0.0M;
        }

       

        public void DrawResults()
        {
            var winners = Players.Where(p => p.TickerPurchased > 0)
                            .OrderByDescending(p => p.TickerPurchased)
                            .ToList();

            if (winners.Any())
            {
                //Console.WriteLine("Drawing results...");
                var grandPrizeWinner = winners.First();
                decimal grandPrize = 50.00m;
                decimal secondTierPrize = 7.50m;
                decimal thirdTierPrize = 2.00m;

                Console.WriteLine("\n Ticket Draw Results:");
                Console.WriteLine($"Grand Prize Winner: {grandPrizeWinner.Name} with {grandPrizeWinner.TickerPurchased} tickets!");
            
                var secondTierWinner=winners.Skip(1).Take(4).ToList();
                Console.WriteLine($"Second Tier: " +
                    $"{string.Join(",",secondTierWinner.Select(p=>p.Name))} win {secondTierPrize:c} each");

                var thirdTierWinner= winners.Skip(5).Take(5).ToList();
                Console.WriteLine($"Third Tier: {string.Join(",", thirdTierWinner.Select(p => p.Name))} win {thirdTierPrize:C} each!");

                Console.WriteLine("\n Congratulations to winners..!!");
                Console.WriteLine($"House revenue: {HouseRevenue}");

            }
            else
            {
                Console.WriteLine("No winners this time.");
            }
        }

               
        public void StartGame()
        {            
            Console.WriteLine("Welcome to the lottery Game !");
            Console.WriteLine("Please enter your name-");

            var playerName = Console.ReadLine();
            var player = new Player(playerName)
            {
                Balance = 10.00m
            };
            Players.Add(player);

            Console.WriteLine($"Your digital balance:{player.Balance}");
            Console.WriteLine($"Ticket price: {_Config.TicketCost} each");

            int ticketToPurchase = GetNumberOfTickets(player);  
            PurchaseTickets(player, ticketToPurchase);

            GenerateCPUPlayer();

            DrawResults();

        }

        private int GetNumberOfTickets(Player player)
        {
            //throw new NotImplementedException();
            int tktToPurchase = 10;  
            while(true)
            {
                Console.WriteLine($"How many tickets do you want to buy,{player.Name} ? ");
                if(int.TryParse(Console.ReadLine(), out tktToPurchase) && tktToPurchase >0 )
                {
                    return tktToPurchase;
                }
                Console.WriteLine("Invalid input. Please enter positive number..");
            }
        }

        public void PurchaseTickets(Player player, int numberOfTickets)
        {            
            decimal totalCost=numberOfTickets* _Config.TicketCost; 
            if (player.Balance>= totalCost)
            {
                player.TickerPurchased += numberOfTickets;
                player.Balance -= totalCost;
                HouseRevenue += totalCost; //add to House revenue
                Console.WriteLine($"{player.Name} purchased {numberOfTickets} tickets.");

            }
            else
            {
                Console.WriteLine($"{player.Name} does not have enough balance to purchase {numberOfTickets} tickets.");
            }
        }

        public void GenerateCPUPlayer()
        {
            int cpuPlayerCount = _rand.Next(_Config.minPlayer, _Config.maxPlayer + 1);
            for (int i = 0; i < cpuPlayerCount; i++)
            {
                var cpuPlayer = new Player($"CPU Player {i + 1}");
                cpuPlayer.Balance = 10.00m; // set balance for CPU players
                int ticketsToPurchase = _rand.Next(1, 6); // Random tickets between 1 and 5
                PurchaseTickets(cpuPlayer, ticketsToPurchase);
                Players.Add(cpuPlayer);
            }
            Console.WriteLine($"{cpuPlayerCount} other CPU players also have purchased tickets.");


        }


    }
    
}
