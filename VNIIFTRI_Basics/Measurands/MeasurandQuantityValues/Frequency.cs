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
        public Frequency() { }
        public Frequency(double value)
        {
            this.value = value;
        }
        public Frequency(double value, Dimension dimension)
        {
            SetValue(value, dimension);
        }

        public override string ToString()
        {
            return value.ToString() + " " + DefaultDimension.ToString();
        }
        public override string ToString(Dimension dimension)
        {
            return (value / Math.Pow(10, dimension.Id)).ToString() + " " + dimension.ToString();
        }

        public override void SetValue(double value, Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension.Id % 3 == 0)
                this.value = value * Math.Pow(10, dimension.Id);
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Frequency.");
        }
        protected override void SetValue(string src)
        {
            src = src.Replace(',', '.');
            value = double.Parse(src, CultureInfo.InvariantCulture);
        }
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
        protected override double GetValue(Dimension dimension)
        {
            if (!Dimensions.Values.Contains(dimension))
                throw new ArgumentException(dimension.ToString() +
                    " не является размерностью для измеряемой величины " + Name);
            if (dimension.Id % 3 == 0)
                return (value / Math.Pow(10, dimension.Id));
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе " + Name);
        }

        protected override void SetValue(string src, Dimension dimension)
        {
            src = src.Replace(',', '.');
            SetValue(double.Parse(src, CultureInfo.InvariantCulture), dimension);
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
    }
}