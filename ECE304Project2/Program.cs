using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECE304Project2
{
    class Program
    {
        static PaternRecognition pt1 = new PaternRecognition();
        static PaternRecognition pt2 = new PaternRecognition();
        static void Main(string[] args)
        {
            SampleData test1 = new SampleData(20, 50);
            SampleData test2 = new SampleData(70, 70);
            SampleData test3 = new SampleData(70, 70);
            SampleData test4 = new SampleData(85, 45);
            SampleData test5 = new SampleData(52, 18000);
            SampleData test6 = new SampleData(60, 100000);
            SampleData test7 = new SampleData(48, 142000);
            pt1.ReadIn("Data1.dat");
            Console.WriteLine("=====================Data 1=======================");
            pt1.Print();

            Console.WriteLine("with k = 3: ");
            TestDatak(test1, pt1, 3);
            TestDatak(test2, pt1, 3);
            TestDatak(test3, pt1, 3);
            TestDatak(test4, pt1, 3);
            TestDataM(test1, pt1, 3);
            TestDataM(test2, pt1, 3);
            TestDataM(test3, pt1, 3);
            TestDataM(test4, pt1, 3);

            Console.WriteLine("with k = 5: ");
            TestDatak(test1, pt1, 5);
            TestDatak(test2, pt1, 5);
            TestDatak(test3, pt1, 5);
            TestDatak(test4, pt1, 5);
            TestDataM(test1, pt1, 5);
            TestDataM(test2, pt1, 5);
            TestDataM(test3, pt1, 5);
            TestDataM(test4, pt1, 5);

            Console.WriteLine("Success with euch: " + pt1.Successeuch(3));
            Console.WriteLine("Success with man: " + pt1.Successman(5));

            pt2.ReadIn("Data2.dat");
            Console.WriteLine("=====================Data 2=======================");
            pt2.Print();
            Console.WriteLine();

            Console.WriteLine("with k = 3: ");
            TestDatak(test5, pt2, 3);
            TestDatak(test6, pt2, 3);
            TestDatak(test7, pt2, 3);
            TestDataM(test5, pt2, 3);
            TestDataM(test6, pt2, 3);
            TestDataM(test7, pt2, 3);

            Console.WriteLine("with k = 5: ");
            TestDatak(test5, pt2, 5);
            TestDatak(test6, pt2, 5);
            TestDatak(test7, pt2, 5);
            TestDataM(test5, pt2, 5);
            TestDataM(test6, pt2, 5);
            TestDataM(test7, pt2, 5);

            Console.WriteLine("Success with euch: " + pt2.Successeuch(5));
            Console.WriteLine("Success with man: " + pt2.Successman(3));

            Console.Read();
        }

        static void TestDataM(SampleData test, PaternRecognition pt, int k)
        {
            pt.NNEkman(test, k);

            if (test.GetGroup() == "0" || test.GetGroup() == "N")
                Console.Write("Test with Manhatten is a flop ");
            else
                Console.Write("Test with Manhatten is a hit ");

            Console.WriteLine(test.GetGroup() + " " + test.GetX() + " " + test.GetY());
        }
        static void TestDatak(SampleData test, PaternRecognition pt, int k)
        {
            pt.NNEK(test, k);

            if (test.GetGroup() == "0" || test.GetGroup() == "N")
                Console.Write("Test with Euchlidian is a flop ");
            else
                Console.Write("Test with Euchlidian is a hit ");

            Console.WriteLine(test.GetGroup() + " " + test.GetX() + " " + test.GetY());
        }
    }
}

