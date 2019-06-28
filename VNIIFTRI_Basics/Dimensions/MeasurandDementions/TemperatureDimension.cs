using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Dimensions
{
    public class TemperatureDimension :IMeasurendDimension
    {
        private static TemperatureDimension Singleton;

        public readonly Dimension K = new Dimension(Measurand.Temperature, 0, "K");
        public readonly Dimension C = new Dimension(Measurand.Temperature, 1, "C");



        readonly Dictionary<string, Dimension> dims;

        private TemperatureDimension()
        {
            dims = new Dictionary<string, Dimension>()
            {
                {K.Text, K },
                {C.Text, C },
            };
        }

        public IEnumerator<Dimension> GetEnumerator()
        {
            return dims.Values.GetEnumerator();
            //yield return K;
            //yield return C;
        }
        public bool Contains(Dimension dimension)
        {
            return dims.ContainsValue(dimension);
        }
        public string Name { get { return "Температура"; } }

        public static TemperatureDimension Intance
        {
            get { return Singleton ?? (Singleton = new TemperatureDimension()); }
        }
    }
}
