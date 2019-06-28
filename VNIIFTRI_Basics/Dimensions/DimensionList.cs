﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Dimensions
{
    /// <summary>
    /// Класс, содержащий все размерности
    /// </summary>
    public class DimensionList
    {
        #region Fields
        /// <summary>
        /// Статическая переменная для реализации шаблона Одиночка
        /// </summary>
        private static DimensionList Singleton;

        /// <summary>
        /// Представляет размерности мощности
        /// </summary>
        public static PowerDimension Power = PowerDimension.Intance;

        /// <summary>
        /// Представляет размерности частоты
        /// </summary>
        public static FrequencyDimension Frequency = FrequencyDimension.Intance;

        /// <summary>
        /// Представляет размерности температуры
        /// </summary>
        public static TemperatureDimension Temperature = TemperatureDimension.Intance;

        /// <summary>
        /// Словарь, представляющий все размерности
        /// </summary>
        private Dictionary<Measurand, IMeasurendDimension> Dimensions = new Dictionary<Measurand, IMeasurendDimension>()
        {
            {Measurand.Power, PowerDimension.Intance },
            {Measurand.Frequency, FrequencyDimension.Intance },
            {Measurand.Temperature, TemperatureDimension.Intance }
        };
        #endregion
        #region Methods        
        public IEnumerator<Dimension> GetEnumerator()
        {
            foreach (IMeasurendDimension md in Dimensions.Values)
                foreach (Dimension dm in md) yield return dm;
        }

        /// <summary>
        /// Предоставляет доступ к размерностям конкретной измеряемой величины
        /// </summary>
        /// <param name="measurand">Измеряемая величина</param>
        /// <returns>Класс, содержащий размерности для указанной измеряемой величины</returns>
        public IMeasurendDimension this[Measurand measurand]
        {
            get { return Dimensions[measurand]; }
        }

        /// <summary>
        /// Предоставляет ссылку на экземпляр-одиночку
        /// </summary>
        public static DimensionList Instance { get { return Singleton ?? (Singleton = new DimensionList()); } }
        #endregion
    }
}
