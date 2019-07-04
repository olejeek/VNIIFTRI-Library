using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Measurands
{
    /// <summary>
    /// Класс, описывающий размерность измеряемой величины
    /// </summary>
    public class Dimension
    {
        #region Fields

        /// <summary>
        /// Словарь, состоящий из словарей, содержащие размерности для всех величин
        /// </summary>
        public static readonly Dictionary<Measurand, Dictionary<string, Dimension>> AllDimensions =
            new Dictionary<Measurand, Dictionary<string, Dimension>>()
            {
                { Measurand.Frequency, Frequency.Dimensions},
                { Measurand.Power, Power.Dimensions },
                { Measurand.Temperature, Temperature.Dimensions }
            };


        /// <summary>
        /// Указывает к какой измеряемой величине относится данная размерность
        /// </summary>
        private readonly Measurand measurand;

        /// <summary>
        /// Уникальный идентификатор размерности (так же используется для указания степени)
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Текстовое отображение размерности
        /// </summary>
        public readonly string Text;

        #endregion
        #region Methods

        /// <summary>
        /// Представляет размерность
        /// </summary>
        /// <param name="measurand">Измеряемая величина</param>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="value">Строковое представление</param>
        public Dimension(Measurand measurand, int id, string value)
        {
            this.measurand = measurand;
            this.Id = id;
            this.Text = value;
        }

        //public bool Check(Measurand measurand)
        //{
        //    return AllDimensions[measurand].Values.Contains(this);

        //}

        /// <summary>
        /// Преобразует строку в размерность
        /// </summary>
        /// <param name="src">Исходная строка</param>
        /// <returns>Размерность, преобразованная из строки</returns>
        public static Dimension Convert(string src)
        {
            foreach (var dicts in AllDimensions.Values)
            {
                foreach (Dimension dm in dicts.Values)
                    if (src == dm.Text) return dm;
            }
            throw new ArgumentException("Невозможно строку \""+ src +"\" преобразовать в размерность");
        }

        /// <summary>
        /// Преобразует строку с размерностью в размерность определенного типа
        /// </summary>
        /// <param name="src">Строка, которую необходимо преобразовать</param>
        /// <param name="measurand">Измеряемая величина, которая измеряется в единицах, представленных в src</param>
        /// <returns></returns>
        public static Dimension Convert(string src, Measurand measurand)
        {
            foreach (Dimension dm in AllDimensions[measurand].Values)
                if (src == dm.Text) return dm;
            throw new ArgumentException("Невозможно строку \"" + src + "\" преобразовать в размерность");
        }

        /// <summary>
        /// Возвращает массив размерностей, удовлетворяющих строке поиска
        /// </summary>
        /// <param name="src">Строка поиска</param>
        /// <returns>Массив с размерностями, удовлетворяющие строке поиска</returns>
        public static Dimension[] FindDimension(string src)
        {
            List<Dimension> dimensions = new List<Dimension>();
            foreach (var dicts in AllDimensions.Values)
            {
                foreach (Dimension dm in dicts.Values)
                    if (src == dm.Text) dimensions.Add(dm);
            }
            return dimensions.ToArray();
        }

        public override int GetHashCode()
        {
            return (int)measurand * 1000000 + Id;
        }
        public override bool Equals(object obj)
        {
            if (obj is Dimension)
                return GetHashCode() == ((Dimension)obj).GetHashCode();
            else return false;
        }
        public override string ToString()
        {
            return Text;
        }
        #endregion
        #region Operators

        public static bool operator ==(Dimension lv, Dimension rv)
        {
            return lv.GetHashCode() == rv.GetHashCode();
        }

        public static bool operator !=(Dimension lv, Dimension rv)
        {
            return lv.GetHashCode() != rv.GetHashCode();
        }
        #endregion
    }
}
