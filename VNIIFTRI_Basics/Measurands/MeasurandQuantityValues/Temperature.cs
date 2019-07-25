using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using VNIIFTRI.Basics.Mathematic;

namespace VNIIFTRI.Basics.Measurands
{
    public class Temperature : QuantityValueDouble
    {
        #region Static
        public static readonly Dimension K = new Dimension(Measurand.Temperature, 0, "K");
        public static readonly Dimension C = new Dimension(Measurand.Temperature, 1, "C");

        private static readonly string name = "Температура";
        public static readonly Measurand measurand = Measurand.Temperature;
        public static readonly Dictionary<string, Dimension> Dimensions = new Dictionary<string, Dimension>()
            {
                {K.Text, K },
                {C.Text, C }
            };
        public static readonly Dimension DefaultDimension = K;
        #endregion
        const double AbsoluteNullInC = -275.15;

        #region Constructors
        public Temperature() { }
        public Temperature(double value)
        {
            if (value < 0) throw new ArgumentException(Name + " не может иметь отрицательное значение");
            this.value = value;
        }
        public Temperature(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }
        protected Temperature(double value, bool relative = false) : base(relative)
        {
            this.value = value;
        }
        #endregion

        #region Fields
        public override string Name { get { return name; } }
        public override Measurand Measurand { get { return measurand; } }
        #endregion

        #region Methods
        protected override string FormatString(int length, char dimension)
        {
            Dimension dim = Dimensions.ContainsKey(dimension.ToString()) ? Dimensions[dimension.ToString()] : DefaultDimension;

            double val = GetValue(dim);
            return MeasMath.SignifyString(val, length) + " " + dim.ToString();
        }

        protected override QuantityValueDouble Creator(double value)
        {
            return new Temperature(value);
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
            if (dimension == C) value -= AbsoluteNullInC;
            if (value < 0) throw new ArgumentException(Name + " не может иметь отрицательное значение в К " +
                "или меньше " + AbsoluteNullInC + " в C");
            this.value = value;
            if (dimension == K || dimension == C) return;
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Temperature.");
        }

        public override double GetValue(Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension == K) return value;
            else if (dimension == C) return value + AbsoluteNullInC;
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе " + Name);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() + (int)measurand;
        }
        public override bool Equals(object obj)
        {
            return !(obj is Temperature o) ? false : value == o.value;
        }
        public override string ToString()
        {
            return value.ToString() + " " + DefaultDimension.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            return ((dimension == Temperature.C) ? value + AbsoluteNullInC : value).ToString() +
                " " + dimension.ToString();
        }
        #endregion

        #region Operators
        public static Temperature operator +(Temperature lv, Temperature rV)
        {
            return new Temperature(lv.value + rV.value, true);
        }

        public static Temperature operator -(Temperature lv, Temperature rV)
        {
            return new Temperature(lv.value - rV.value, true);
        }

        public static Temperature operator *(Temperature lv, double rv)
        {
            return new Temperature(lv.value * rv, true);
        }
        public static Temperature operator *(double lv, Temperature rv)
        {
            return rv * lv;
        }

        public static double operator /(Temperature lv, Temperature rv)
        {
            return lv.value / rv.value;
        }

        public static bool operator >(Temperature lv, Temperature rv)
        {
            return lv.value > rv.value;
        }
        public static bool operator <(Temperature lv, Temperature rv)
        {
            return lv.value < rv.value;
        }

        public static bool operator >=(Temperature lv, Temperature rv)
        {
            return lv.value >= rv.value;
        }
        public static bool operator <=(Temperature lv, Temperature rv)
        {
            return lv.value <= rv.value;
        }

        public static bool operator ==(Temperature lv, Temperature rv)
        {
            return lv.value == rv.value;
        }
        public static bool operator !=(Temperature lv, Temperature rv)
        {
            return lv.value != rv.value;
        }
        #endregion
    }
}
