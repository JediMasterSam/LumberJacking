using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject.Interfaces
{
    public interface IComponent
    {
        public void UpdateComponent();
        public Type Type { get; }
    }
}
