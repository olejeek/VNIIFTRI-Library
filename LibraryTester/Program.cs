using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Dimensions;
using VNIIFTRI.Basics.QuantityValues;

namespace LibraryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            try
            {
                Power a = QuantityValue.CreateValue<Power>("2,5", DimensionList.Power.mW);
                Console.WriteLine(a);
                Power b = QuantityValue.CreateValue<Power>("0", DimensionList.Power.dBm);
                Console.WriteLine(b);
                Frequency c = QuantityValue.CreateValue<Frequency>("435,56", DimensionList.Frequency.kHz);
                Console.WriteLine(c);
                //Frequency d = QuantityValue.CreateValue<Frequency>("1", DimensionList.Power.dBm);
                //Console.WriteLine(d);
                Temperature e = QuantityValue.CreateValue<Temperature>("10", DimensionList.Temperature.C);
                Console.WriteLine(e);
                Temperature f = QuantityValue.CreateValue<Temperature>("0", DimensionList.Temperature.K);
                Console.WriteLine(f);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
