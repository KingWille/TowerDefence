using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class Particle
    {
        private Random rnd;
        private Texture2D Tex;
        private Vector2 Position, Velocity, Origin;
        private float Angle, AngleVel;
        private int LifeTime;

        public Particle(Vector2 pos)
        {
            Position = pos;
            Angle = 0;
            rnd = new Random();

            LifeTime = 20 + rnd.Next(40);
            Tex = Assets.Particle;

            Origin = new Vector2(Tex.Width / 2, Tex.Height / 2);

            while(Velocity.X == 0 ||  Velocity.Y == 0)
            {
                Velocity = new Vector2(rnd.Next(-2, 2) * 10, rnd.Next(-2, 2) * 10);

            }

            AngleVel = 0.2f * (float)rnd.NextDouble();
        }

        internal void Update()
        {
            LifeTime--;
            Position += Velocity;
            Angle += AngleVel;
        }
        internal void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Position, null, Color.White, Angle, Origin, 1f, SpriteEffects.None, 1f);
        }
    }
}
