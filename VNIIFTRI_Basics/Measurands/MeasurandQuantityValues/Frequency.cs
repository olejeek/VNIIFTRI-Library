﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using VNIIFTRI.Basics.Mathematic;

namespace VNIIFTRI.Basics.Measurands
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
        public static readonly Measurand measurand = Measurand.Frequency;
        public static readonly Dictionary<string, Dimension> Dimensions = new Dictionary<string, Dimension>()
            {
                {mHz.Text, mHz},
                {Hz.Text, Hz },
                {kHz.Text, kHz },
                {MHz.Text, MHz },
                {GHz.Text, GHz },
                {THz.Text, THz },
            };
        public static readonly Dimension DefaultDimension = Hz;
        #endregion

        #region Constructors
        public Frequency() { }
        public Frequency(double value)
        {
            if (value < 0) throw new ArgumentException(Name + " не может иметь отрицательное значение");
            this.value = value;
        }
        public Frequency(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }
        protected Frequency(double value, bool relative = false) : base(relative)
        {
            this.value = value;
        }
        #endregion

        #region Fields
        public override string Name { get { return name; } }
        #endregion

        #region Methods
        protected override string FormatString(int length, char dimension)
        {
            Dimension dim = DefaultDimension;
            switch (dimension)
            {
                case 'm':
                case 'k':
                case 'M':
                case 'G':
                case 'T':
                    dim = Dimensions[dimension + "Hz"];
                    break;
            }
            double val = GetValue(dim);
            return MeasMath.SignifyString(val, length) + " " + dim.ToString();
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

        public override void SetValue(double value, Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension.Id % 3 == 0)
            {
                if (value < 0) throw new ArgumentException(Name + " не может иметь отрицательное значение");
                this.value = value * Math.Pow(10, dimension.Id);
            }
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Frequency.");
        }

        public override double GetValue(Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension.Id % 3 == 0)
                return (value / Math.Pow(10, dimension.Id));
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе " + Name);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() + (int)measurand;
        }
        public override bool Equals(object obj)
        {
            return !(obj is Frequency o) ? false : value == o.value;
        }
        public override string ToString()
        {
            return value.ToString() + " " + DefaultDimension.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            return (value / Math.Pow(10, dimension.Id)).ToString() + " " + dimension.ToString();
        }
        #endregion

        #region Operators
        public static Frequency operator +(Frequency lv, Frequency rV)
        {
            return new Frequency(lv.value + rV.value, true);
        }

        public static Frequency operator -(Frequency lv, Frequency rV)
        {
            return new Frequency(lv.value - rV.value, true);
        }

        public static Frequency operator *(Frequency lv, double rv)
        {
            return new Frequency(lv.value * rv, true);
        }
        public static Frequency operator *(double lv, Frequency rv)
        {
            return rv * lv;
        }

        public static double operator /(Frequency lv, Frequency rv)
        {
            return lv.value / rv.value;
        }

        public static bool operator >(Frequency lv, Frequency rv)
        {
            return lv.value > rv.value;
        }
        public static bool operator <(Frequency lv, Frequency rv)
        {
            return lv.value < rv.value;
        }

        public static bool operator >=(Frequency lv, Frequency rv)
        {
            return lv.value >= rv.value;
        }
        public static bool operator <=(Frequency lv, Frequency rv)
        {
            return lv.value <= rv.value;
        }

        public static bool operator ==(Frequency lv, Frequency rv)
        {
            return lv.value == rv.value;
        }
        public static bool operator !=(Frequency lv, Frequency rv)
        {
            return lv.value != rv.value;
        }

        #endregion
    }
}
