using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Measurands
{
    /// <summary>
    /// Перечисление, содержащее измеряемые величины
    /// </summary>
    public enum Measurand
    {
        /// <summary>
        /// Мощность
        /// </summary>
        Power,
        /// <summary>
        /// Температура
        /// </summary>
        Temperature,
        /// <summary>
        /// Частота
        /// </summary>
        Frequency,
        /// <summary>
        /// Коэффициент шума
        /// </summary>
        NoiseFactor
    }
}
