using Spline;

namespace TowerDefence
{
    internal class Enemies
    {
        private int SourceRowIndex, OnePercentHP;
        private float Speed, StartAngle;
        private Texture2D Tex, RedBar, GreenBar;
        private Rectangle HealthBarRectRed, HealthBarRectGreen;
        private Vector2 Origin;
        private SimplePath Path;

        internal bool IsBoss{get; private set;}
        internal int Level, Health, GoldValue, Damage;
        internal float PathIndex;
        internal Rectangle Rect;
        internal Vector2 Pos;

        public Enemies(int index, SimplePath path)
        {
            Level = index + 1;
            Damage = Level;
            Health = Level * 100;
            OnePercentHP = Health / 100;

            GoldValue = Level * 30;
            SourceRowIndex = index;
            Path = path;
            PathIndex = 0;
            Speed = 0.5f;

            if(Level == 5)
            {
                IsBoss = true;
            }
            else
            {
                IsBoss = false;
            }

            Tex = Assets.Enemies;
            RedBar = Assets.RedBar;
            GreenBar = Assets.GreenBar;

            //Sätter startpunkten utanför fönstret beroende på start punkten i pathen
            if(Path.GetPos(0).X <= 0)
            {
                Pos = new Vector2(Path.GetPos(0).X - Tex.Width, Path.GetPos(0).Y);
            }
            else if(Path.GetPos(0).Y <= 0)
            {
                Pos = new Vector2(Path.GetPos(0).X, Path.GetPos(0).Y - Tex.Height);
            }
            else if(Path.GetPos(0).X <= Globals.WindowSize.X - Tex.Width)
            {
                Pos = new Vector2(Globals.WindowSize.X + Tex.Width, Path.GetPos(0).Y);
            }
            else if(Path.GetPos(0).Y <= Globals.WindowSize.Y - Tex.Height)
            {
                Pos = new Vector2(Path.GetPos(0).X, Globals.WindowSize.Y + Tex.Height);
            }

            Rect = new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width / 2, Tex.Width / 2);
            Origin = new Vector2(Rect.Width / 2, Rect.Width / 2);
            HealthBarRectRed = new Rectangle((int)Pos.X - RedBar.Width / 2, (int)Pos.Y - RedBar.Height * 2, RedBar.Width, RedBar.Height);
            HealthBarRectGreen = new Rectangle((int)Pos.X - RedBar.Width / 2 + 1, (int)Pos.Y - RedBar.Height * 2 + 1, RedBar.Width, RedBar.Height);

        }

        internal void Update()
        {
            //Flyttar fienden och sätter rotationen efter pathens gång
            PathIndex += Speed;
            if((int)PathIndex <= Path.AntalPunkter - 1)
            {
                Pos = Path.GetPos((int)PathIndex);
                Pos.X += Origin.X;
                Pos.Y += Origin.Y;

                if(PathIndex < Path.AntalPunkter - 1)
                {
                    Vector2 AnglePos = Path.GetPos((int)PathIndex + 1) - Path.GetPos((int)PathIndex);
                    StartAngle = (float)Math.Atan2(AnglePos.Y, AnglePos.X);
                }
            }
            else
            {
                Pos += CalcExit();
            }

            Rect.X = (int)Pos.X;
            Rect.Y = (int)Pos.Y;

            //Flyttar healthbarens position
            HealthBarRectGreen.X = (int)Pos.X - RedBar.Width / 2 + 1;
            HealthBarRectGreen.Y = (int)Pos.Y - RedBar.Height * 2 + 1;
            HealthBarRectRed.X = (int)Pos.X - RedBar.Width / 2;
            HealthBarRectRed.Y = (int)Pos.Y - RedBar.Height * 2;

            //uppdaterar healthbaren beroende på hur mycket liv fienden har kvar
            HealthBars();
        }

        internal void Draw()
        {
            Globals.SpriteBatch.Draw(Tex, Pos, new Rectangle(0, SourceRowIndex * Tex.Width / 2, Tex.Width / 2, Tex.Width / 2), Color.White, StartAngle, Origin, 1f, SpriteEffects.None, 1f);
            Globals.SpriteBatch.Draw(RedBar, HealthBarRectRed, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            Globals.SpriteBatch.Draw(GreenBar, HealthBarRectGreen, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        //Räknar ut vilket håll fienderna ska fortsätta gå när dem är vid slutet av pathen
        private Vector2 CalcExit()
        {
            Vector2 SpeedEnhancer = Path.GetPos(Path.AntalPunkter - 1) - Path.GetPos(Path.AntalPunkter - 2);
            float left = Path.GetPos(Path.AntalPunkter - 1).X;
            float right = Globals.WindowSize.X - Path.GetPos(Path.AntalPunkter - 1).X;
            float up = Path.GetPos(Path.AntalPunkter - 1).Y;
            float down = Globals.WindowSize.Y - Path.GetPos(Path.AntalPunkter - 1).Y;

            float result = Math.Min(Math.Min(left, right), Math.Min(up, down));

            Vector2 Result = Vector2.Zero;

            if(result == left)
            {
                Result = new Vector2(SpeedEnhancer.X, 0);
            }
            else if (result == right)
            {
                Result = new Vector2(SpeedEnhancer.X, 0);
            }
            else if(result == up)
            {
                Result = new Vector2(0, SpeedEnhancer.Y);
            }
            else if(result == down)
            {
                Result = new Vector2(0, SpeedEnhancer.Y);
            }

            return Result;

        }

        //Uppdaterar bredden på den gröna healthbaren beroende på hur mycket liv som är kvar;
        private void HealthBars()
        {

            double healthBarWidth = 0;

            for (int i = 0; i < Health; i += OnePercentHP)
            {
                healthBarWidth += 0.5;
            }

            HealthBarRectGreen.Width = (int)healthBarWidth;

        }

        internal void SetHealthFull()
        {
            Health = Level * 100;
            OnePercentHP = Level;
            SourceRowIndex -= 1;
        }

    }
}
