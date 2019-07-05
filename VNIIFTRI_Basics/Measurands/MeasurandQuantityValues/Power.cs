using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using VNIIFTRI.Basics.Mathematic;

namespace VNIIFTRI.Basics.Measurands
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
        public static readonly Measurand measurand = Measurand.Power;
        public static readonly Dictionary<string, Dimension> Dimensions = new Dictionary<string, Dimension>()
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
        public static readonly Dimension DefaultDimension = Power.W;

        #endregion
        public Power() { }
        public Power(double value)
        {
            if (value < 0) throw new ArgumentException(Name + " не может иметь отрицательное значение");
            this.value = value;
        }
        public Power(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }

        public override string ToString()
        {
            return value.ToString() + " " + DefaultDimension.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            StringBuilder sb = new StringBuilder(20);
            sb.Append(GetValue(dimension));
            sb.Append(" " + dimension.ToString());
            return sb.ToString();
        }
        protected override string FormatString(int length, char dimension)
        {
            Dimension dim = DefaultDimension;
            switch(dimension)
            {
                case 'p':
                case 'n':
                case 'u':
                case 'm':
                case 'k':
                case 'M':
                    dim = Dimensions[dimension + "W"];
                    break;
                case 'd':
                    dim = Dimensions["dBm"];
                    break;
            }
            double val = GetValue(dim);
            return MeasMath.SignifyString(val, length) + " " + dim.ToString();
        }

        public override void SetValue(double value, Dimension dimension)
        {
            //if (!CheckDimension(measurand, dimension))
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            double t = value;
            if (dimension.Id % 3 == 0)
            {
                if (t < 0) throw new ArgumentException(Name + " мощность не может иметь отрицательное значение");
                this.value = t * Math.Pow(10, dimension.Id);
            }
            else if (dimension == Power.dBm)
                this.value = Math.Pow(10, t / 10) * Math.Pow(10, Power.mW.Id);
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Power.");
        }

        protected override void SetValue(string src)
        {
            src = src.Replace(',', '.');
            double t = double.Parse(src, CultureInfo.InvariantCulture);
            if (t < 0) throw new ArgumentException(Name + " не может иметь отрицательное значение");
            value = t;
        }

        protected override void SetValue(string src, Dimension dimension)
        {
            src = src.Replace(',', '.');
            SetValue(double.Parse(src, CultureInfo.InvariantCulture), dimension);
        }
        protected override double GetValue(Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension.Id % 3 == 0)
                return (value / Math.Pow(10, dimension.Id));
            else if (dimension == Power.dBm)
                return (10 * Math.Log10(this / new Power(1, Power.mW)));
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе " + Name);
        }

        public override string Name { get { return name; } }

        public override IEnumerator<Dimension> GetEnumerator()
        {
            return Dimensions.Values.GetEnumerator();
        }

        public override bool Contains(Dimension dimension)
        {
            return Dimensions.Values.Contains(dimension);
        }

        public static double operator /(Power lv, Power rv)
        {
            return lv.value / rv.value;
        }

        public static Power operator +(Power lv, Power rV)
        {
            return new Power(lv.value + rV.value);
        }

        public static Power operator -(Power lv, Power rV)
        {
            if (lv.value - rV.value < 0)
                throw new ArithmeticException(name + " не может быть меньше 0.");
            return new Power(lv.value - rV.value);
        }
    }
}