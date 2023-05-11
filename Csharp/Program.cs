// using System;

// namespace Csharp
// {
//     class Rectangle
//     {
//         static void Main(string[] args)
//         {
//             // Console.WriteLine("Hello World!"); // Prints Hello World!
            
//             //member variables
//             double width ;
//             double height ;

//             public void Acceptdetails()
//             {
//                 width = 4.5;
//                 height = 3.5;
//             }
//             public double GetArea()
//             {
//                 return width * height;
//             }
//             public void Display()
//             {
//                 Console.WriteLine("Width: {0}", width);
//                 Console.WriteLine("Height: {0}", height);
//                 Console.WriteLine("Area: {0}", GetArea());
//             }

//         }
//         class ExecuteRectangle
//         {
//             static void Main(string[] args)
//             {
//                 Rectangle r = new Rectangle();
//                 r.Acceptdetails();
//                 r.Display();
//                 Console.ReadLine();
//             }
//         }
//     }
// }

using System;

namespace Csharp
{
    class Rectangle
    {
        //member variables
        double width ;
        double height ;

        public void Acceptdetails()
        {
            width = 4.5;
            height = 3.5;
        }
        public double GetArea()
        {
            return width * height;
        }
        public void Display()
        {
            Console.WriteLine("Width: {0}", width);
            Console.WriteLine("Height: {0}", height);
            Console.WriteLine("Area: {0}", GetArea());
        }
    }
    
    class ExecuteRectangle
    {
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle();
            r.Acceptdetails();
            r.Display();
            Console.ReadLine();
        }
    }
}