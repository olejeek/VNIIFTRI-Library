using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Dimensions
{
    public class PowerDimension : IMeasurendDimension
    {
        private static PowerDimension Singleton;

        public readonly Dimension pW = new Dimension(Measurand.Power, -12, "pW");
        public readonly Dimension nW = new Dimension(Measurand.Power, -9, "nW");
        public readonly Dimension uW = new Dimension(Measurand.Power, -6, "uW");
        public readonly Dimension mW = new Dimension(Measurand.Power, -3, "mW");
        public readonly Dimension W = new Dimension(Measurand.Power, 0, "W");
        public readonly Dimension kW = new Dimension(Measurand.Power, 3, "kW");
        public readonly Dimension MW = new Dimension(Measurand.Power, 6, "MW");
        public readonly Dimension dBm = new Dimension(Measurand.Power, 1, "dBm");


        readonly Dictionary<string, Dimension> dims;

        private PowerDimension()
        {
            dims = new Dictionary<string, Dimension>()
            {
                {pW.Text, pW },
                {nW.Text, nW },
                {uW.Text, uW },
                {mW.Text, mW },
                {W.Text, W },
                {kW.Text, kW },
                {MW.Text, MW },
                {dBm.Text, dBm },
            };
        }

        public IEnumerator<Dimension> GetEnumerator()
        {
            return dims.Values.GetEnumerator();
            //yield return pW;
            //yield return nW;
            //yield return uW;
            //yield return mW;
            //yield return W;
            //yield return kW;
            //yield return MW;
            //yield return dBm;
        }
        public bool Contains(Dimension dimension)
        {
            return dims.ContainsValue(dimension);
        }
        public string Name { get { return "Мощность"; } }

        public static PowerDimension Intance
        {
            get { return Singleton ?? (Singleton = new PowerDimension()); }
        }
    }
}
