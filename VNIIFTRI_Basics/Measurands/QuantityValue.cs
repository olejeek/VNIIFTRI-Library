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
        protected readonly bool relative = false;

        protected QuantityValue(bool relative) { this.relative = relative; }
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
        public static T CreateValue<T>(string src)
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
        public static T CreateValue<T>(string src, Dimension dimension)
            where T : QuantityValue, new()
        {
            T t = new T();
            string[] temps = src.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (temps.Length > 1) throw new ArgumentException("Невозможно представить строку \"" + src +
                "\" в виде числа");
            t.SetValue(src, dimension);
            return t;
        }

        public abstract string Name { get; }
    }

    /// <summary>
    /// Обобщенный абстрактный класс, представляющий измеряемую величину
    /// </summary>
    /// <typeparam name="T">Тип числа, в котором измеряется величина 
    /// (целое, с плавающей точкой, комплексное)</typeparam>
    public abstract class QuantityValue<T> : QuantityValue, IFormattable
        where T : struct
    {
        protected T value;

        /// <summary>
        /// Конструктор без параметров, необходим для конструкторов без параметров для наследуемых
        /// классов
        /// </summary>
        protected QuantityValue() : base(false) { }
        protected QuantityValue(bool relative) : base (relative) { }
        public abstract string ToString(Dimension dimension);
        public abstract void SetValue(T value, Dimension dimension);

        /// <summary>
        /// Присваивание значение value измеренному значению
        /// </summary>
        /// <param name="value">Присваиваемое значение</param>
        public void SetValue(T value)
        {
            this.value = value;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) return ToString();
            char dimension = '\0';
            int length;
            StringBuilder sb = new StringBuilder(40);
            if (format[0] == 'N')
            {
                sb.Append(Name + ": ");
                format = format.Substring(1);
            }
            if (char.IsLetter(format.Last()))
            {
                dimension = format.Last();
                format = format.Substring(0, format.Length - 1);
            }
            int.TryParse(format, out length);
            sb.Append(FormatString(length, dimension));
            return sb.ToString();
        }

        protected abstract string FormatString(int length, char dimension);
        public abstract T GetValue(Dimension dimension);
    }
}
