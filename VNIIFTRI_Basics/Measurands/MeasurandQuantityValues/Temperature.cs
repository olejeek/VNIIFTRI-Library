using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using VNIIFTRI.Basics.Mathematic;

namespace VNIIFTRI.Basics.Measurands
{
    public class Temperature : QuantityValue<double>
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

        public override string ToString()
        {
            return value.ToString() + " " + DefaultDimension.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            return ((dimension == Temperature.C) ? value + AbsoluteNullInC : value).ToString() + 
                " " + dimension.ToString();
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
        protected override string FormatString(int length, char dimension)
        {
            Dimension dim = Dimensions.ContainsKey(dimension.ToString()) ? Dimensions[dimension.ToString()] : DefaultDimension;
            
            double val = GetValue(dim);
            return MeasMath.SignifyString(val, length) + " " + dim.ToString();
        }
        protected override double GetValue(Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension == K) return value;
            else if (dimension == C) return value + AbsoluteNullInC;
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

        #region Operators
        public static double operator /(Temperature lv, Temperature rv)
        {
            return lv.value / rv.value;
        }

        public static Temperature operator +(Temperature lv, Temperature rV)
        {
            return new Temperature(lv.value + rV.value);
        }

        public static Temperature operator -(Temperature lv, Temperature rV)
        {
            if (lv.value - rV.value < 0)
                throw new ArithmeticException(name + " не может быть меньше 0.");
            return new Temperature(lv.value - rV.value);
        }
        #endregion
    }
}
