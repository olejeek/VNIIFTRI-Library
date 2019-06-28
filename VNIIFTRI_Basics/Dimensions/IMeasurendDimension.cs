using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNIIFTRI.Basics.Dimensions
{
    public interface IMeasurendDimension
    {
        IEnumerator<Dimension> GetEnumerator();
        bool Contains(Dimension dimension);
        string Name { get; }
    }
}
