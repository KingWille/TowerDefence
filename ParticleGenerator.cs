using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class ParticleGenerator
    {
        private Vector2 EmitterPos;
        private Particle[] Particles;
        private bool Emitted;

        public ParticleGenerator(Vector2 pos)
        {
            EmitterPos = pos;
            Particles = new Particle[20];
            Emitted = false;
        }

        internal void Update()
        {
            //Slutar lägga till partiklar efter 20
            if(!Emitted)
            {
                for(int i = 0; i < Particles.Length; i++)
                {
                    Particles[i] = new Particle(EmitterPos);
                }
                Emitted = true;
            }

            foreach(var p in Particles)
            {
                p.Update();
            }
        }

        internal void Draw()
        {
            foreach(var p in Particles)
            {
                p.Draw();
            }
        }
    }
}
