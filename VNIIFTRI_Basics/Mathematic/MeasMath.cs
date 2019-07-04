using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Mathematic
{
    public static class MeasMath
    {
        /// <summary>
        /// Округляет число до необходимого числа значащих знаков, но не менее, чем до целого
        /// </summary>
        /// <param name="value">Число</param>
        /// <param name="count">Количество значащих знаков</param>
        /// <returns>Число, с необходимым количеством знаков</returns>
        public static double Signify(double value, int count)
        {
            double razryad = count - Math.Log10(Math.Abs(value));
            if (razryad <= 1) return Math.Round(value);
            else
                return Math.Round(value, (int)razryad);
        }

        /// <summary>
        /// Округляет число до необходимого числа значащих знаков, но не менее, чем до целого
        /// </summary>
        /// <param name="value">Число</param>
        /// <param name="count">Количество значащих знаков</param>
        /// <returns>Строка, содержащая число, с необходимым количеством знаков</returns>
        public static string SignifyString(double value, int count)
        {
            double razryad = count - Math.Log10(Math.Abs(value));
            if (razryad <= 1) return Math.Round(value).ToString();
            else
            {
                string format = "{0:0";
                if (razryad <= 1 || count <= 1) return String.Format(format + "}", value);
                --count;
                format += ".";
                for (int i = 0; i < count && i < (int)razryad; ++i)
                    format += "0";
                format += "}";
                return String.Format(format, value);
            }
        }
    }
}
