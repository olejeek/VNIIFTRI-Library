using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Measurands
{
    public abstract class QuantityValueDouble : QuantityValue
    {
        #region Vars
        protected double value;
        #endregion
        #region Constructors
        protected QuantityValueDouble() : base() { }
        protected QuantityValueDouble(bool relative) : base(relative) { }
        #endregion
        #region Methods
        public abstract void SetValue(double complex, Dimension dimension);
        public abstract double GetValue(Dimension dimension);
        protected abstract QuantityValueDouble Creator(double value);
        #endregion
        #region Operaotrs
        public static QuantityValueDouble operator +(QuantityValueDouble lv, QuantityValueDouble rv)
        {
            if (lv.Measurand != rv.Measurand)
                throw new ArgumentException("Невозможно произмести операцию сложения величин: " +
                   lv.Name + " и " + rv.Name + ". См. класс QuantityValueDouble");
            else
                return lv.Creator(lv.value + rv.value);

        }
        public static QuantityValueDouble operator -(QuantityValueDouble lv, QuantityValueDouble rv)
        {
            if (lv.Measurand != rv.Measurand)
                throw new ArgumentException("Невозможно произмести операцию вычитания величин: " +
                   lv.Name + " и " + rv.Name + ". См. класс QuantityValueDouble");
            else
                return lv.Creator(lv.value - rv.value);

        }

        public static double operator /(QuantityValueDouble lv, QuantityValueDouble rv)
        {
            if (lv.Measurand != rv.Measurand)
                throw new ArgumentException("Невозможно произмести операцию деления величин: " +
                   lv.Name + " и " + rv.Name + ". См. класс QuantityValueDouble");
            else
                return lv.value / rv.value;

        }
        #endregion
    }
}
