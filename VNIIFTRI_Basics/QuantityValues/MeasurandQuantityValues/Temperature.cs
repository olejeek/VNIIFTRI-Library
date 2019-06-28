using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Dimensions;

namespace VNIIFTRI.Basics.QuantityValues
{
    public class Temperature : QuantityValue<double>
    {
        #region Static
        public static readonly Dimension K = new Dimension(Measurand.Temperature, 0, "K");
        public static readonly Dimension C = new Dimension(Measurand.Temperature, 1, "C");

        static Temperature()
        {
            Dimensions = new Dictionary<string, Dimension>()
            {
                {K.Text, K },
                {C.Text, C }
            };
        }
        #endregion

        public Temperature(double value) : base(value) { }
        public Temperature() { }

        public override string ToString()
        {
            return value.ToString() + " " + Temperature.K.ToString();
        }
        protected override void SetValue(string src)
        {
            value = Convert.ToDouble(src);
        }
        protected override void SetValue(string src, Dimension dimension)
        {
            CheckAndSetStandartValue(Convert.ToDouble(src), dimension);
            if (dimension == K) return;
            if (dimension == C) value += 275.15;
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Temperature.");

        }

        public override string Name { get { return "Температура"; } }

    }
}
