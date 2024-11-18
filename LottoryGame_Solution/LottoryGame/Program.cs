using System;
using LottoryGame.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LottoryGame
{
    class Program
    {
        //public static decimal TicketCost { get; private set; }

        static void Main(string[] args)
        {
            var config = new LotteryConfig
            {
                TicketCost = 1.00m,
                maxPlayer = 15,
                minPlayer = 10
            };

            var random = new Random();
            //service with dependancy injection
            ILotteryService lotteryservice = new LotteryService(config,random );
            lotteryservice.StartGame();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();



        }
    }
}
