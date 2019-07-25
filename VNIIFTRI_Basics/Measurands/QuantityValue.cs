using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Measurands
{
    /// <summary>
    /// Абстрактный класс, представляющий измеряемую величину
    /// </summary>
    public abstract class QuantityValue
    {
        #region Static
        
        #endregion

        #region Vars
        protected readonly bool relative = false;
        #endregion
        #region Fields
        public abstract string Name { get; }
        public abstract Measurand Measurand { get; }
        #endregion
        #region Constructors
        protected QuantityValue() { }

        protected QuantityValue(bool relative) { this.relative = relative; }
        #endregion
        #region Methods
        /// <summary>
        /// Защищенный абстрактный метод для присваивания значения измеряемой величене в единицах по умолчанию.
        /// Данный метод не должен содержать проверку на не число внутри строки.
        /// </summary>
        /// <param name="src">Строка, содержащее число</param>
        protected abstract void SetValue(string src);

        /// <summary>
        /// Защищенный абстрактный метод для присваивания значения измеряемой величине
        /// в указанных единицах. Данный метод не должен содержать проверку на не число внутри строки.
        /// </summary>
        /// <param name="src">Строка, содержащая число</param>
        /// <param name="dimension">Размерность, в которой представлено измеренное значение</param>
        protected abstract void SetValue(string src, Dimension dimension);

        /// <summary>
        /// Возвращает значение величины типа Т из строки
        /// </summary>
        /// <typeparam name="T">Класс, наследуемый от абстрактного класса QuantityValue, 
        /// представляющий физическую величину</typeparam>
        /// <param name="src">Строка, конвертируемая в значение величины. Если указано только число, 
        /// то предполагается, что значение дано в стандартных единицах измерения. Поддерживается
        /// формат строки, где величина представлена ввиде числа с единицами измерения, разделенные
        /// пробелом</param>
        /// <returns>Величина, сконвертированная из значения строки</returns>
        public static T Creator<T>(string src)
            where T : QuantityValue, new()
        {
            T t = new T();
            string[] temps = src.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (temps.Length)
            {
                case 1:
                    t.SetValue(src);
                    break;
                case 2:
                    t.SetValue(temps[0], Dimension.Convert(temps[1]));
                    break;
                default:
                    throw new ArgumentException("Невозможно преобразовать строку \"" + src +
                        "\" в значение величины");
            }
            return t;
        }

        /// <summary>
        /// Возвращает значение величины типа Т
        /// </summary>
        /// <typeparam name="T">Класс, наследуемый от абстрактного класса QuantityValue, 
        /// представляющий физическую величину</typeparam>
        /// <param name="src">Строка, содержащая численное значение величины</param>
        /// <param name="dimension">Единца измерений, в которых представлена величина</param>
        /// <returns>Величина, сконвертированная из значения строки и единицы измерения</returns>
        public static T Creator<T>(string src, Dimension dimension)
            where T : QuantityValue, new()
        {
            T t = new T();
            string[] temps = src.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (temps.Length > 1) throw new ArgumentException("Невозможно представить строку \"" + src +
                "\" в виде числа");
            t.SetValue(src, dimension);
            return t;
        }

        protected abstract string FormatString(int length, char dimension);
        public abstract string ToString(Dimension dimension);
        #endregion
        #region Operators
        public static QuantityValue operator +(QuantityValue lv, QuantityValue rv)
        {
            if ((lv is QuantityValueDouble) && (rv is QuantityValueDouble))
                return (QuantityValueDouble)lv + (QuantityValueDouble)rv;
            else if ((lv is QuantityValueComplex) && (rv is QuantityValueComplex))
                return (QuantityValueComplex)lv + (QuantityValueComplex)rv;
            else throw new ArgumentException("Не предусмотрена реализация сложения величин: "
                + lv.Name + " и " + rv.Name + ". См. класс QuantityValue");
        }
        public static QuantityValue operator -(QuantityValue lv, QuantityValue rv)
        {
            if ((lv is QuantityValueDouble) && (rv is QuantityValueDouble))
                return (QuantityValueDouble)lv - (QuantityValueDouble)rv;
            else if ((lv is QuantityValueComplex) && (rv is QuantityValueComplex))
                return (QuantityValueComplex)lv - (QuantityValueComplex)rv;
            else throw new ArgumentException("Не предусмотрена реализация вычитания величин: "
                + lv.Name + " и " + rv.Name + ". См. класс QuantityValue");
        }
        #endregion
    }
}
