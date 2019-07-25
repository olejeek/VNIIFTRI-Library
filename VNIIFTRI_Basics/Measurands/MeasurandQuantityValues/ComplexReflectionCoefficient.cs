using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Mathematic;
using System.Globalization;

namespace VNIIFTRI.Basics.Measurands
{
    public class ComplexReflectionCoefficient : QuantityValueComplex
    {
        #region Static
        public static readonly Dimension unit = new Dimension(Measurand.NoiseFactor, 0, "");
        //public static readonly Dimension dB = new Dimension(Measurand.Power, 1, "dB");

        private static readonly string name = "Комплексный коэффициент отражения";
        public static readonly Measurand measurand = Measurand.ComplexReflectionCoefficient;
        public static readonly Dictionary<string, Dimension> Dimensions = new Dictionary<string, Dimension>()
            {
                { unit.Text, unit }/*,
                { dB.Text, dB }*/
            };
        public static readonly Dimension DefaultDimension = unit;
        #endregion

        #region Constructors
        public ComplexReflectionCoefficient() { }
        public ComplexReflectionCoefficient(Complex value)
        {
            this.value = value;
        }
        public ComplexReflectionCoefficient(Complex value, Dimension dimension)
        {
            SetValue(value, dimension);
        }
        #endregion
        #region Fields
        public override string Name { get { return name; } }
        public override Measurand Measurand { get { return measurand; } }
        #endregion
        #region Methods
        protected override string FormatString(int length, char dimension)
        {
            return value.ToString(length.ToString() + dimension.ToString(), null);
        }

        protected override QuantityValueComplex Creator(Complex value)
        {
            return new ComplexReflectionCoefficient(value);
        }
        protected override void SetValue(string src)
        {
            value = Complex.Parse(src);
        }
        protected override void SetValue(string src, Dimension dimension)
        {
            src = src.Replace(',', '.');
            SetValue(src);
        }

        public override void SetValue(Complex value, Dimension dimension)
        {
            if (Dimensions.ContainsValue(dimension))
                throw new ArgumentException(dimension.ToString() + " не является размерностью для величины " + Name);
            else this.value = value;
        }

        public override Complex GetValue(Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension == unit)
                return value;
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе " + Name);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() + (int)measurand;
        }
        public override bool Equals(object obj)
        {
            return !(obj is ComplexReflectionCoefficient o) ? false : value == o.value;
        }
        public override string ToString()
        {
            return value.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            if (!Dimensions.ContainsValue(dimension))
                throw new ArgumentException(dimension.ToString() + " не является размерностью для величины " + Name);
            else return ToString();
        }
        #endregion
        #region Operators
        public static ComplexReflectionCoefficient operator +(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rV)
        {
            return new ComplexReflectionCoefficient(lv.value + rV.value);
        }

        public static ComplexReflectionCoefficient operator -(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rV)
        {
            return new ComplexReflectionCoefficient(lv.value - rV.value);
        }

        public static ComplexReflectionCoefficient operator *(ComplexReflectionCoefficient lv, double rv)
        {
            if (rv < 0)
                throw new ArithmeticException(name + " не может быть меньше 0.");
            return new ComplexReflectionCoefficient(lv.value * rv);
        }
        public static ComplexReflectionCoefficient operator *(double lv, ComplexReflectionCoefficient rv)
        {
            return rv * lv;
        }

        public static Complex operator /(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rv)
        {
            return lv.value / rv.value;
        }

        public static bool operator >(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rv)
        {
            return lv.value.Magnitude > rv.value.Magnitude;
        }
        public static bool operator <(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rv)
        {
            return lv.value.Magnitude < rv.value.Magnitude;
        }

        public static bool operator >=(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rv)
        {
            return lv.value.Magnitude >= rv.value.Magnitude;
        }
        public static bool operator <=(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rv)
        {
            return lv.value.Magnitude <= rv.value.Magnitude;
        }

        public static bool operator ==(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rv)
        {
            return lv.value == rv.value;
        }
        public static bool operator !=(ComplexReflectionCoefficient lv, ComplexReflectionCoefficient rv)
        {
            return lv.value != rv.value;
        }
        #endregion
    }
}
