using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return value.ToString() + " " + Frequency.Hz.ToString();
        }

        protected override void SetValue(string src)
        {
            if (src.Contains(".")) src = src.Replace(".", ",");
            value = Convert.ToDouble(src);
        }

        protected override void SetValue(string src, Dimension dimension)
        {
            CheckAndSetStandartValue(Convert.ToDouble(src), dimension);
            if (dimension.Id % 3 == 0) value = value * Math.Pow(10, dimension.Id);

            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Frequency.");
        }

        public override string Name { get { return name; } }
    }
}
