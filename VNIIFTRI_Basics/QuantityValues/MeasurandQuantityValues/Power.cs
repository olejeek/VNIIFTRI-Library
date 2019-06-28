using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Dimensions;
namespace VNIIFTRI.Basics.QuantityValues
{
    public class Power : QuantityValue<double>
    {
        public Power() { }

        public override string ToString()
        {
            return value.ToString() + " " + DimensionList.Power.W.ToString();
        }

        protected override void SetValue(string src)
        {
            value = Convert.ToDouble(src);
        }

        protected override void SetValue(string src, Dimension dimension)
        {
            CheckAndSetStandartValue(Convert.ToDouble(src), dimension, DimensionList.Power);
            if (dimension.Id % 3 == 0) value = value * Math.Pow(10, dimension.Id);
            else if (dimension.Id == DimensionList.Power.dBm.Id)
                value = Math.Pow(10, value / 10) * Math.Pow(10, DimensionList.Power.mW.Id);
            else
                throw new ArgumentException("Неизвестная или неучтенная размерность в классе Power.");
        }
    }
}
