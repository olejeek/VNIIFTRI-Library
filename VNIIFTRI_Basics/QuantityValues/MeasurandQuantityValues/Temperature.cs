using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Dimensions;

namespace VNIIFTRI.Basics.QuantityValues
{
    public class Temperature : QuantityValue<double>
    {
        public Temperature(double value) : base(value) { }
        public Temperature() { }

        public override string ToString()
        {
            return value.ToString() + " " + DimensionList.Temperature.K.ToString();
        }
        protected override void SetValue(string src)
        {
            value = Convert.ToDouble(src);
        }
        protected override void SetValue(string src, Dimension dimension)
        {
            CheckAndSetStandartValue(Convert.ToDouble(src), dimension, DimensionList.Temperature);
            if (dimension == DimensionList.Temperature.K) return;
            if (dimension == DimensionList.Temperature.C) value += 275.15;
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Temperature.");

        }
        


    }
}
