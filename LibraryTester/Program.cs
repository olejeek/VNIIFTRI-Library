using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Mathematic;
using VNIIFTRI.Basics;

namespace LibraryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            //try
            //{
                Power a = QuantityValue.CreateValue<Power>("2,5344", Power.mW);
                for (int i = 1; i < 9; ++i)
                    Console.WriteLine("{0:N" + i + "d}\t{0:" + i + "}", a);
                Console.ReadKey();

                Power b = QuantityValue.CreateValue<Power>("-3.2", Power.dBm);
                for (int i = 1; i < 9; ++i)
                    Console.WriteLine("{0:N" + i + "k}\t{0:" + i + "m}", b);
                Console.ReadKey();

                Frequency c = QuantityValue.CreateValue<Frequency>("435,562232", Frequency.kHz);
                for (int i = 1; i < 9; ++i)
                    Console.WriteLine("{0:N" + i + "}\t{0:" + i + "M}", c);
                Console.ReadKey();

                Frequency d = QuantityValue.CreateValue<Frequency>("1.237", Frequency.Hz);
                for (int i = 1; i < 9; ++i)
                    Console.WriteLine("{0:N" + i + "G}\t{0:" + i + "m}", d);
                Console.ReadKey();

                Temperature e = QuantityValue.CreateValue<Temperature>("10,5", Temperature.C);
                for (int i = 1; i < 9; ++i)
                    Console.WriteLine("{0:N" + i + "}\t{0:" + i + "C}", e);
                Console.ReadKey();

                Temperature f = QuantityValue.CreateValue<Temperature>("0.1", Temperature.K);
                for (int i = 1; i < 9; ++i)
                    Console.WriteLine("{0:N" + i + "}\t{0:" + i + "C}", f);
                Console.ReadKey();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //Console.ReadKey();
        }
    }
}
