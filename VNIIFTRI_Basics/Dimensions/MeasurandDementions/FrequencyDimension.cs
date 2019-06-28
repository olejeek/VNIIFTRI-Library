using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Dimensions
{
    public class FrequencyDimension : IMeasurendDimension
    {
        private static FrequencyDimension Singleton;

        public readonly Dimension mHz = new Dimension(Measurand.Frequency, -3, "mHz");
        public readonly Dimension Hz = new Dimension(Measurand.Frequency, 0, "Hz");
        public readonly Dimension kHz = new Dimension(Measurand.Frequency, 3, "kHz");
        public readonly Dimension MHz = new Dimension(Measurand.Frequency, 6, "MHz");
        public readonly Dimension GHz = new Dimension(Measurand.Frequency, 9, "GHz");
        public readonly Dimension THz = new Dimension(Measurand.Frequency, 12, "TGz");


        readonly Dictionary<string, Dimension> dims;

        private FrequencyDimension()
        {
            dims = new Dictionary<string, Dimension>()
            {
                {mHz.Text, mHz },
                {Hz.Text, Hz },
                {kHz.Text, kHz },
                {MHz.Text, MHz },
                {GHz.Text, GHz },
                {THz.Text, THz },
            };
        }

        public IEnumerator<Dimension> GetEnumerator()
        {
            return dims.Values.GetEnumerator();
            //yield return mHz;
            //yield return Hz;
            //yield return kHz;
            //yield return MHz;
            //yield return GHz;
            //yield return THz;
        }
        public bool Contains(Dimension dimension)
        {
            return dims.ContainsValue(dimension);
        }
        public string Name { get { return "Частота"; } }


        public static FrequencyDimension Intance
        {
            get { return Singleton ?? (Singleton = new FrequencyDimension()); }
        }
    }
}
