using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace VNIIFTRI.Basics
{
    public class Temperature : QuantityValue<double>
    {
        #region Static
        public static readonly Dimension K = new Dimension(Measurand.Temperature, 0, "K");
        public static readonly Dimension C = new Dimension(Measurand.Temperature, 1, "C");

        private static readonly string name = "Температура";

        static Temperature()
        {
            Dimensions = new Dictionary<string, Dimension>()
            {
                {K.Text, K },
                {C.Text, C }
            };
        }
        #endregion

        public Temperature() { }
        public Temperature(double value)
        {
            this.value = value;
        }
        public Temperature(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }

        public override string ToString()
        {
            return value.ToString() + " " + Temperature.K.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            return ((dimension == Temperature.C) ? value + 275.15 : value).ToString() + 
                " " + dimension.ToString();
        }

        public override void SetValue(double value, Dimension dimension)
        {
            if (!CheckDimension(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            this.value = value;
            if (dimension == K) return;
            else if (dimension == C) value += 275.15;
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Temperature.");
        }
        protected override void SetValue(string src)
        {
            value = double.Parse(src, CultureInfo.InvariantCulture);
        }
        protected override void SetValue(string src, Dimension dimension)
        {
            SetValue(double.Parse(src, CultureInfo.InvariantCulture), dimension);
        }

        public override string Name { get { return name; } }

    }
}
