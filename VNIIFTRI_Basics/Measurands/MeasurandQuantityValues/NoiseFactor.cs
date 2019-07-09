using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Mathematic;
using System.Globalization;

namespace VNIIFTRI.Basics.Measurands
{
    public class NoiseFactor:QuantityValue<double>
    {
        #region Static
        public static readonly Dimension unit = new Dimension(Measurand.NoiseFactor, 0, "");
        public static readonly Dimension dB = new Dimension(Measurand.Power, 1, "dB");

        private static readonly string name = "Коэффициент шума";
        public static readonly Measurand measurand = Measurand.NoiseFactor;
        public static readonly Dictionary<string, Dimension> Dimensions = new Dictionary<string, Dimension>()
            {
                { unit.Text, unit },
                { dB.Text, dB }
            };
        public static readonly Dimension DefaultDimension = NoiseFactor.unit;

        #endregion

        #region Constructors
        public NoiseFactor() { }
        public NoiseFactor(double value)
        {
            this.value = value;
        }
        public NoiseFactor(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return value.GetHashCode() + (int)measurand;
        }
        public override bool Equals(object obj)
        {
            return !(obj is NoiseFactor o) ? false : value == o.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            if (dimension == dB) return value.ToString() + " " + dB.ToString();
            else return ToString();
        }
        protected override string FormatString(int length, char dimension)
        {
            Dimension dim = dimension == 'd' ? dB : unit;
            double val = GetValue(dim);
            return MeasMath.SignifyString(val, length) + " " + dim.ToString();
        }

        protected override double GetValue(Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension == unit)
                return value;
            else if (dimension == dB)
                return (10 * Math.Log10(this / new NoiseFactor(1, NoiseFactor.unit)));
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе " + Name);
        }
        protected override void SetValue(string src)
        {
            src = src.Replace(',', '.');
            value = double.Parse(src, CultureInfo.InvariantCulture);
        }
        protected override void SetValue(string src, Dimension dimension)
        {
            src = src.Replace(',', '.');
            SetValue(double.Parse(src, CultureInfo.InvariantCulture), dimension);
        }
        public override void SetValue(double value, Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension == unit)
                this.value = value;
            else if (dimension == dB)
                this.value = Math.Pow(10, value / 10);
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Power.");
        }

        public override bool Contains(Dimension dimension)
        {
            return Dimensions.Values.Contains(dimension);

        }
        public override IEnumerator<Dimension> GetEnumerator()
        {
            return Dimensions.Values.GetEnumerator();
        }

        public override string Name { get { return name; } }

        #endregion

        #region Operators

        public static NoiseFactor operator +(NoiseFactor lv, NoiseFactor rv)
        {
            return new NoiseFactor(lv.value + rv.value);
        }

        public static NoiseFactor operator -(NoiseFactor lv, NoiseFactor rv)
        {
            return new NoiseFactor(lv.value - rv.value);
        }

        public static NoiseFactor operator *(NoiseFactor lv, double rv)
        {
            return new NoiseFactor(lv.value * rv);
        }
        public static NoiseFactor operator *(double lv, NoiseFactor rv)
        {
            return rv * lv;
        }

        public static double operator /(NoiseFactor lv, NoiseFactor rv)
        {
            return lv.value / rv.value;
        }

        public static bool operator >(NoiseFactor lv, NoiseFactor rv)
        {
            return lv.value > rv.value;
        }
        public static bool operator <(NoiseFactor lv, NoiseFactor rv)
        {
            return lv.value < rv.value;
        }

        public static bool operator >=(NoiseFactor lv, NoiseFactor rv)
        {
            return lv.value >= rv.value;
        }
        public static bool operator <=(NoiseFactor lv, NoiseFactor rv)
        {
            return lv.value <= rv.value;
        }

        public static bool operator ==(NoiseFactor lv, NoiseFactor rv)
        {
            return lv.value == rv.value;
        }
        public static bool operator !=(NoiseFactor lv, NoiseFactor rv)
        {
            return lv.value != rv.value;
        }
        #endregion
    }
}
