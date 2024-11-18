using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoryGame.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int TickerPurchased { get; set; }

        public decimal Balance { get; set; } = 10.00m;
        public Player(String name) 
        {
            Name = name;
            TickerPurchased = 0;
            Balance = 0.0m;
        }
    }
}
