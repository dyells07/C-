using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter Password :");
            string a = Console.ReadLine();
            if(  a == "code"){
                Console.WriteLine();
                Console.WriteLine(" You are verified");
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                Console.WriteLine();
                Console.WriteLine("Hello Welcome to Our VS based Console App");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Enter Your name ");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("You are very Welcomed, "+name);

                Console.WriteLine("This is Simple Program Which Find Out Ascii value");
                Console.WriteLine();
                Console.Write("Enter Character to find Ascii value: ");
                string userinput = Console.ReadLine();
                byte[] letter = Encoding.ASCII.GetBytes(userinput);
                Console.WriteLine();
                Console.WriteLine("++++++++++++++");
                foreach(byte element in letter)
                {
                    Console.Write("{1} : {0} ", element, (char)element);
                    Console.WriteLine();
                }
                Console.WriteLine("++++++++++++++");

            }
            else
            {
                Console.WriteLine("Error");

            }
    Console.ReadLine();
        }
    }

}
