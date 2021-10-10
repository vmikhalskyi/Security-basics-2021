using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(1234);

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(random.Next(4, 20));
            }

        }
    }
}
