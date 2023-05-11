// using System;
// using System.Threading;
// using System.IO;
// using System.Collections.Generic;
// using System.IO;
// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.ForegroundColor = ConsoleColor.Green;
//         Console.WriteLine("Welcome to the Bipin ChatApp!");
//         Console.ResetColor();

//         Console.Write("Enter your name: ");
//         string name = Console.ReadLine();

//         Console.WriteLine();
//         Console.ForegroundColor = ConsoleColor.Yellow;
//         Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
//         Console.WriteLine("║                     Bipin ChatApp                      ║");
//         Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
//         Console.ResetColor();
//         Console.WriteLine();

//         Thread receiveThread = new Thread(() => ReceiveMessages(name));
//         receiveThread.Start();

//         Dictionary<string, string> replies = LoadReplies("replies.csv");

//         bool greeted = false; // to keep track of whether the chatbot has greeted the user

//         while (true)
//         {
//             Console.Write(" > ");
//             Console.ForegroundColor = ConsoleColor.Blue;
//             Console.Write(name + ": ");
//             Console.ResetColor();
//             string message = Console.ReadLine();
//             Console.WriteLine();

//             if (message.ToLower() == "hello" && !greeted)
//             {
//                 greeted = true;
//                 Console.ForegroundColor = ConsoleColor.Gray;
//                 Console.WriteLine("┌───────────────────────────────────────────────────────┐");
//                 Console.Write("│ ");
//                 Console.ForegroundColor = ConsoleColor.Red;
//                 Console.Write("Bipin");
//                 Console.ForegroundColor = ConsoleColor.Gray;
//                 Console.Write(": ");
//                 Console.WriteLine("Hello, " + name + "! How can I assist you today?".PadRight(46) + "│ " + GetTimeStamp());
//                 Console.WriteLine("└───────────────────────────────────────────────────────┘");
//                 Console.WriteLine();
//                 Console.ResetColor();
//             }
//             else
//             {
//                 string reply;
//                 if (replies.TryGetValue(message.ToLower(), out reply))
//                 {
//                     Console.ForegroundColor = ConsoleColor.Gray;
//                     Console.WriteLine("┌───────────────────────────────────────────────────────┐");
//                     Console.Write("│ ");
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.Write("Bipin");
//                     Console.ForegroundColor = ConsoleColor.Gray;
//                     Console.Write(": ");
//                     Tuple<string, string> replyTuple = GetReplyFromCSV(message);
//                     Console.WriteLine(replyTuple.Item1.PadRight(46) + "│ " + GetTimeStamp());
//                     Console.WriteLine("│ " + replyTuple.Item2.PadRight(46) + "│ " + GetTimeStamp());
//                     Console.WriteLine("└───────────────────────────────────────────────────────┘");
//                     Console.WriteLine();
//                     Console.ResetColor();
//                 }
//                 else
//                 {
//                     Console.ForegroundColor = ConsoleColor.Gray;
//                     Console.WriteLine("┌───────────────────────────────────────────────────────┐");
//                     Console.Write("│ ");
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.Write("Bipin");
//                     Console.ForegroundColor = ConsoleColor.Gray;
//                     Console.Write(": ");
//                     Console.WriteLine("Sorry, I don't understand. Can you please rephrase or ask a different question?".PadRight(46) + "│ " + GetTimeStamp());
//                     Console.WriteLine("└───────────────────────────────────────────────────────┘");
//                     Console.WriteLine();
//                     Console.ResetColor();
//                 }
//             }
//         }
//     }
// static void ReceiveMessages(string name)
// {
//     // Open the chat.csv file for appending
//     using (StreamWriter file = new StreamWriter("chat.csv", true))
//     {
//         while (true)
//         {
//             Console.Write(" > ");
//             Console.ForegroundColor = ConsoleColor.Blue;
//             Console.Write("You: ");
//             Console.ResetColor();
//             string message = Console.ReadLine();
//             Console.WriteLine();

//             Tuple<string, string> replyTuple = GetReplyFromCSV(message);

//             Console.ForegroundColor = ConsoleColor.Gray;
//             Console.WriteLine("┌───────────────────────────────────────────────────────┐");
//             Console.Write("│ ");
//             Console.ForegroundColor = ConsoleColor.Blue;
//             Console.Write(name);
//             Console.ForegroundColor = ConsoleColor.Gray;
//             Console.Write(": ");
//             Console.WriteLine(message.PadRight(46) + "│ " + GetTimeStamp());
//             Console.WriteLine("└───────────────────────────────────────────────────────┘");
//             Console.WriteLine();
//             Console.ResetColor();

//             // Write the user's chat message to the chat.csv file
//             string userEntry = $"{GetTimeStamp()},{name},{message}";
//             file.WriteLine(userEntry);

//             // Flush the StreamWriter to ensure the data is written to the file immediately
//             file.Flush();

//             Console.Write(" > ");
//             Console.ForegroundColor = ConsoleColor.Blue;
//             Console.Write("Bipin: ");
//             Console.ResetColor();
//             Console.ForegroundColor = ConsoleColor.Gray;
//             Console.WriteLine("┌───────────────────────────────────────────────────────┐");
//             Console.Write("│ ");
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.Write("Bipin");
//             Console.ForegroundColor = ConsoleColor.Gray;
//             Console.Write(": ");
//             Console.WriteLine(replyTuple.Item1.PadRight(46) + "│ " + GetTimeStamp());
//             Console.WriteLine("│ " + replyTuple.Item2.PadRight(46) + "│ " + GetTimeStamp());
//             Console.WriteLine("└───────────────────────────────────────────────────────┘");
//             Console.WriteLine();
//             Console.ResetColor();

//             // Write the chatbot's response to the chat.csv file
//             string botEntry = $"{GetTimeStamp()},Bipin,{replyTuple.Item1}";
//             file.WriteLine(botEntry);

//             if (!string.IsNullOrEmpty(replyTuple.Item2))
//             {
//                 botEntry = $"{GetTimeStamp()},Bipin,{replyTuple.Item2}";
//                 file.WriteLine(botEntry);
//             }

//             // Flush the StreamWriter to ensure the data is written to the file immediately
//             file.Flush();
//         }
//     }
// }



//     static Dictionary<string, string> LoadReplies(string filePath)
//     {
//         var replies = new Dictionary<string, string>();
//         try
//         {
//             using (var reader = new StreamReader(filePath))
//             {
//                 while (!reader.EndOfStream)
//                 {
//                     var line = reader.ReadLine();
//                     var values = line.Split(',');
//                     if (values.Length >= 2)
//                     {
//                         string csvMessage = values[0].Trim().ToLower();
//                         string csvReply = values[1].Trim();
//                         replies[csvMessage] = csvReply;
//                     }
//                 }
//             }
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("Error loading replies from file: " + e.Message);
//         }
//         return replies;
//     }

//     static Tuple<string, string> GetReplyFromCSV(string message)
//     {
//         Tuple<string, string> reply = new Tuple<string, string>("Sorry, I don't understand. Can you please rephrase or ask a different question?", "");
//         var replies = LoadReplies("replies.csv");
//         if (replies.TryGetValue(message.ToLower(), out string value))
//         {
//             string[] lines = value.Split(new char[] { '\n' }, 2, StringSplitOptions.RemoveEmptyEntries);
//             if (lines.Length > 0)
//             {
//                 reply = new Tuple<string, string>(message, lines[0]);
//                 if (lines.Length > 1)
//                 {
//                     reply = new Tuple<string, string>(message, lines[0] + "\n" + lines[1]);
//                 }
//             }
//         }
//         return reply;
//     }

//     static string GetTimeStamp()
//     {
//         return DateTime.Now.ToString("HH:mm:ss tt");
//     }
// }

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to the BipinBot!");
        Console.ResetColor();

        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        BipinBOT                       ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Thread receiveThread = new Thread(() => ReceiveMessages(name));
        receiveThread.Start();

        Dictionary<string, string> replies = LoadReplies("replies.csv");

        while (true)
        {
            Console.Write(" > ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string message = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine();

            if (message.ToLower() == "exit")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BipinBot: Goodbye, " + name + "! Have a great day!");
                break;
            }

            string reply;
            if (replies.TryGetValue(message.ToLower(), out reply))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BipinBot: " + reply);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ChatBot: Sorry, I don't have an answer for that. Can you please ask a different question?");
            }

            Console.ResetColor();
        }
    }

    static void ReceiveMessages(string name)
    {
        using (StreamWriter file = new StreamWriter("chat.csv", true))
        {
            while (true)
            {
                string message = Console.ReadLine();

                if (message.ToLower() == "exit")
                {
                    file.WriteLine($"{GetTimeStamp()},{name},{message}");
                    file.Flush();
                    break;
                }

                file.WriteLine($"{GetTimeStamp()},{name},{message}");
                file.Flush();
            }
        }
    }

    static Dictionary<string, string> LoadReplies(string filePath)
    {
        var replies = new Dictionary<string, string>();
        try
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length >= 2)
                    {
                        string csvMessage = values[0].Trim().ToLower();
                        string csvReply = values[1].Trim();
                        replies[csvMessage] = csvReply;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading replies from file: " + e.Message);
        }
        return replies;
    }

    static string GetTimeStamp()
    {
        return DateTime.Now.ToString("HH:mm:ss tt");
    }
}
