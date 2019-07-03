using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace VNIIFTRI.Basics
{
    public class Frequency : QuantityValue<double>
    {
        #region Static
        public static readonly Dimension mHz = new Dimension(Measurand.Frequency, -3, "mHz");
        public static readonly Dimension Hz = new Dimension(Measurand.Frequency, 0, "Hz");
        public static readonly Dimension kHz = new Dimension(Measurand.Frequency, 3, "kHz");
        public static readonly Dimension MHz = new Dimension(Measurand.Frequency, 6, "MHz");
        public static readonly Dimension GHz = new Dimension(Measurand.Frequency, 9, "GHz");
        public static readonly Dimension THz = new Dimension(Measurand.Frequency, 12, "TGz");

        private static readonly string name = "Частота";

        static Frequency()
        {
            Dimensions = new Dictionary<string, Dimension>()
            {
                {mHz.Text, mHz},
                {Hz.Text, Hz },
                {kHz.Text, kHz },
                {MHz.Text, MHz },
                {GHz.Text, GHz },
                {THz.Text, THz },
            };
        }
        #endregion
        public Frequency() { }
        public Frequency(double value)
        {
            this.value = value;
        }
        public Frequency(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }

        public override string ToString()
        {
            return value.ToString() + " " + Frequency.Hz.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            return (value / Math.Pow(10, dimension.Id)).ToString() + " " + dimension.ToString();
        }

        public override void SetValue(double value, Dimension dimension)
        {
            if (!CheckDimension(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension.Id % 3 == 0)
                this.value = value * Math.Pow(10, dimension.Id);
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Frequency.");
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
