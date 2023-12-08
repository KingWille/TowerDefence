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
        internal abstract void Update(Game1 game);
        internal abstract void Draw(Game1 game);
    }
}
