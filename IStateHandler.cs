using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal abstract class IStateHandler
    {
        internal abstract void Update();
        internal abstract void Draw();
    }
}
