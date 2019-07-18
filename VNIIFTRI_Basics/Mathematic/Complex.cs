using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Mathematic;

namespace VNIIFTRI.Basics.Mathematic
{
    public struct Complex : IFormattable
    {
        public double Re { get; private set; }
        public double Im { get; private set; }

        public Complex (double re, double im)
        {
            Re = re;
            Im = im;
        }

        #region Methods
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(Re * Re + Im * Im);
            }
        }
        public double Phase
        {
            get
            {
                return Math.Atan2(Im, Re);
            }
        }
        public Complex Conjugate
        {
            get
            {
                return new Complex(Re, -Im);
            }
        }
        
        public override string ToString()
        {
            return Re + (Im > 0 ? ("+" + Im + "i") : (Im + "i"));
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) return ToString();
            char dimension = '\0';
            int length = 0;
            StringBuilder sb = new StringBuilder(40);
            if (char.IsLetter(format[format.Length - 1]))
            {
                dimension = format[format.Length - 1];
                format = format.Substring(0, format.Length - 1);
            }
            if (!int.TryParse(format, out length)) length = 4;
            dimension = char.ToUpper(dimension);
            switch(dimension)
            {
                case '\0':
                case 'D':
                    sb.Append(MeasMath.SignifyString(Re, length));
                    if (Im > 0) sb.Append('+');
                    sb.Append(MeasMath.SignifyString(Im, length));
                    sb.Append('i');
                    break;
                case 'P':
                    sb.Append(MeasMath.SignifyString(Magnitude, length));
                    sb.Append("∙exp(");
                    sb.Append(MeasMath.SignifyString(Phase, length));
                    sb.Append("i)");
                    break;
                default:
                    return ToString();
            }
            return sb.ToString();
        }
        public double ConjugateMultiply()
        {
            return (this * Conjugate).Re;
        }
        public override int GetHashCode()
        {
            return (Re.GetHashCode() + Im.GetHashCode());
        }
        public override bool Equals(object obj)
        {
            return !(obj is Complex c) ? false : (Re == c.Re && Im == c.Im);
        }

        public static Complex Parse(string src)
        {
            src.Replace(',', '.');

            StringBuilder first = new StringBuilder(20), second = new StringBuilder(20);
            bool next = false;
            StringBuilder image = second;
            for (int i = 0; i < src.Length; ++i)
            {
                if ((src[i] == '-' && i != 0) || src[i] == '+') next = !next;
                if (!next) first.Append(src[i]);
                else second.Append(src[i]);
                if (src[i] == 'i')
                {
                    if (!next) image = first;
                    else image = second;
                }
            }
            double re = (first != image) ? double.Parse(first.ToString()) : double.Parse(second.ToString());
            double im = (second == image) ? double.Parse(second.ToString()) : double.Parse(first.ToString());
            return new Complex(re, im);
        }
        #endregion
        #region Operators
        public static Complex operator +(Complex lv, Complex rv)
        {
            return new Complex(lv.Re + rv.Re, lv.Im + rv.Im);
        }
        public static Complex operator -(Complex lv, Complex rv)
        {
            return new Complex(lv.Re - rv.Re, lv.Im - rv.Im);
        }
        public static Complex operator *(Complex lv, Complex rv)
        {
            return new Complex(lv.Re * rv.Re - lv.Im * rv.Im, lv.Re * rv.Im + lv.Im * rv.Re);
        }
        public static Complex operator /(Complex lv, double rv)
        {
            return new Complex(lv.Re / rv, lv.Im / rv);
        }
        public static Complex operator /(Complex lv, Complex rv)
        {
            return (lv * rv.Conjugate) / rv.ConjugateMultiply();
        }
        public static implicit operator Complex(double re)
        {
            return new Complex(re, 0);
        }

        public static bool operator ==(Complex lv, Complex rv)
        {
            return lv.Re == rv.Re && lv.Im == rv.Im;
        }
        public static bool operator !=(Complex lv, Complex rv)
        {
            return !(lv==rv);
        }
        #endregion
    }
}
