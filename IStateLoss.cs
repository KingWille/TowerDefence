using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class IStateLoss : IStateHandler
    {
        private LoseScreen Loss;
        public IStateLoss() 
        {
            Loss = new LoseScreen();
        }

        internal override void Update()
        {
            Loss.Update();
        }

        internal override void Draw()
        {
            Globals.SpriteBatch.Begin();
            Loss.Draw();
            Globals.SpriteBatch.End();
        }
    }
}
