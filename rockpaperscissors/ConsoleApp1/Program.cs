using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<string> moves = new List<string> { "rock", "paper", "scissors" };
        string play_again = "y";
        while (play_again.ToLower() == "y")
        {
            Console.WriteLine("Enter your move (rock, paper, or scissors):");
            Console.Write("\u001b[1mEnter your move (\U0001F5FF, \U0001F4C4, or \U0001F9FB):\u001b[0m ");
            string player_move = Console.ReadLine();

            Random random = new Random();
            string computer_move = moves[random.Next(moves.Count)];

            if (player_move == computer_move)
            {
                Console.WriteLine("\u001b[93mIt's a tie!\u001b[0m");
            }
            else if (player_move == "rock" && computer_move == "scissors")
            {
                Console.WriteLine("\u001b[92mYou win!\u001b[0m");
            }
            else if (player_move == "paper" && computer_move == "rock")
            {
                Console.WriteLine("\u001b[92mYou win!\u001b[0m");
            }
            else if (player_move == "scissors" && computer_move == "paper")
            {
                Console.WriteLine("\u001b[92mYou win!\u001b[0m");
            }
            else
            {
                Console.WriteLine("\u001b[91mThe computer wins!\u001b[0m");
            }

            Console.WriteLine("\u001b[1mThe computer played " + computer_move + ".\u001b[0m");

            Console.Write("Do you want to play again? (y/n) ");
            play_again = Console.ReadLine();
        }
    }
}
