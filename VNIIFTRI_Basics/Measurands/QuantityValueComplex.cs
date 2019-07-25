using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNIIFTRI.Basics.Mathematic;

namespace VNIIFTRI.Basics.Measurands
{
    public abstract class QuantityValueComplex : QuantityValue
    {
        #region Vars
        protected Complex value;
        #endregion
        #region Constructors
        protected QuantityValueComplex() : base() { }
        protected QuantityValueComplex(bool relative) : base (relative) { }
        #endregion
        #region Methods
        public abstract void SetValue(Complex complex, Dimension dimension);
        public abstract Complex GetValue(Dimension dimension);
        protected abstract QuantityValueComplex Creator(Complex value);
        #endregion
        #region Operators

        public static QuantityValueComplex operator +(QuantityValueComplex lv, QuantityValueComplex rv)
        {
            if (lv.Measurand != rv.Measurand)
                throw new ArgumentException("Невозможно произмести операцию сложения величин: " +
                   lv.Name + " и " + rv.Name + ". См. класс QuantityValueComplex");
            else
                return lv.Creator(lv.value + rv.value);

        }
        public static QuantityValueComplex operator -(QuantityValueComplex lv, QuantityValueComplex rv)
        {
            if (lv.Measurand != rv.Measurand)
                throw new ArgumentException("Невозможно произмести операцию вычитания величин: " +
                   lv.Name + " и " + rv.Name + ". См. класс QuantityValueComplex");
            else
                return lv.Creator(lv.value - rv.value);

        }

        public static Complex operator /(QuantityValueComplex lv, QuantityValueComplex rv)
        {
            if (lv.Measurand != rv.Measurand)
                throw new ArgumentException("Невозможно произмести операцию деления величин: " +
                   lv.Name + " и " + rv.Name + ". См. класс QuantityValueComplex");
            else
                return lv.value / rv.value;

        }
        #endregion
    }
}
