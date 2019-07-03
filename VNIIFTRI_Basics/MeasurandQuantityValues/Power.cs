using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace VNIIFTRI.Basics
{
    public class Power : QuantityValue<double>
    {
        #region Static
        public static readonly Dimension pW = new Dimension(Measurand.Power, -12, "pW");
        public static readonly Dimension nW = new Dimension(Measurand.Power, -9, "nW");
        public static readonly Dimension uW = new Dimension(Measurand.Power, -6, "uW");
        public static readonly Dimension mW = new Dimension(Measurand.Power, -3, "mW");
        public static readonly Dimension W = new Dimension(Measurand.Power, 0, "W");
        public static readonly Dimension kW = new Dimension(Measurand.Power, 3, "kW");
        public static readonly Dimension MW = new Dimension(Measurand.Power, 6, "MW");
        public static readonly Dimension dBm = new Dimension(Measurand.Power, 1, "dBm");

        private static readonly string  name = "Мощность";
        

        static Power()
        {
            DefaultDimenion = Power.W;
            Dimensions = new Dictionary<string, Dimension>()
            {
                { pW.Text, pW },
                { nW.Text, nW },
                {uW.Text, uW },
                { mW.Text, mW },
                { W.Text, W },
                { kW.Text, kW },
                { MW.Text, MW },
                { dBm.Text, dBm },
            };
        }
        #endregion
        public Power() { }
        public Power(double value)
        {
            this.value = value;
        }
        public Power(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }

        public override string ToString(Dimension dimension)
        {
            if (!CheckDimension(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            StringBuilder sb = new StringBuilder(20);
            if (dimension.Id % 3 == 0)
                sb.Append((value / Math.Pow(10, dimension.Id)).ToString());
            else if (dimension == Power.dBm)
                sb.Append((10 * Math.Log10(this /new Power(1, Power.mW))).ToString());
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Power.");
            sb.Append(" " + dimension.ToString());
            return sb.ToString();
        }
        public override void SetValue(double value, Dimension dimension)
        {
            if (!CheckDimension(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            double t = value;
            if (dimension.Id % 3 == 0) value = t * Math.Pow(10, dimension.Id);
            else if (dimension == Power.dBm)
                value = Math.Pow(10, t / 10) * Math.Pow(10, Power.mW.Id);
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Power.");
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
        public static double operator/ (Power lv, Power rv)
        {
            return lv.value / rv.value;
        }
    }
}