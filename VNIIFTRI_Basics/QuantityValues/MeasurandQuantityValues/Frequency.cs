using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Dimensions;

namespace VNIIFTRI.Basics.QuantityValues
{
    public class Frequency : QuantityValue<double>
    {
        public Frequency() { }

        public override string ToString()
        {
            return value.ToString() + " " + DimensionList.Frequency.Hz.ToString();
        }

        protected override void SetValue(string src)
        {
            if (src.Contains(".")) src = src.Replace(".", ",");
            value = Convert.ToDouble(src);
        }

        protected override void SetValue(string src, Dimension dimension)
        {
            CheckAndSetStandartValue(Convert.ToDouble(src), dimension, DimensionList.Frequency);
            if (dimension.Id % 3 == 0) value = value * Math.Pow(10, dimension.Id);

            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Frequency.");
        }
    }
}
