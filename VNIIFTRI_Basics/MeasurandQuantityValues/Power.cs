using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        static Power()
        {
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

        public override string ToString()
        {
            return value.ToString() + " " + Power.W.ToString();
        }

        protected override void SetValue(string src)
        {
            value = Convert.ToDouble(src);
        }

        protected override void SetValue(string src, Dimension dimension)
        {
            CheckAndSetStandartValue(Convert.ToDouble(src), dimension);
            if (dimension.Id % 3 == 0) value = value * Math.Pow(10, dimension.Id);
            else if (dimension== Power.dBm)
                value = Math.Pow(10, value / 10) * Math.Pow(10, Power.mW.Id);
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Power.");
        }

        public override string Name { get { return "Мощность"; } }
    }
}
