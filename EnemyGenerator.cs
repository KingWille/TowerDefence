using Spline;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class EnemyGenerator
    {
        private float EnemyReleaseTimer, EnemyReleaseInterval, EnemyReleaseMin;
        private int EnemyLevelMax, EnemyLevelMin, NumberOfEnemiesWave, EnemyTracker;
        private bool Upgraded;
        private Enemies[] EnemyArray;
        private List<ParticleGenerator> PG;
        private SimplePath Path;
        private Random rnd;

        internal static int TurnTracker;
        internal bool TurnActivated;


        public EnemyGenerator(SimplePath path) 
        {
            Path = path;
            TurnActivated = false;
            Upgraded = true;
            EnemyReleaseInterval = 1;
            EnemyReleaseTimer = EnemyReleaseInterval;
            EnemyReleaseMin = 0.3f;
            EnemyLevelMax = 0;
            EnemyLevelMin = 0;
            EnemyTracker = 0;
            NumberOfEnemiesWave = 10;

            TurnTracker = 0;

            EnemyArray = new Enemies[NumberOfEnemiesWave];
            rnd = new Random();
            PG = new List<ParticleGenerator>();
        }

        public void Update()
        {
            //Gör det svårare nästa runda
            if(TurnActivated && !Upgraded)
            {
                IncreaseIntensity();
                Upgraded = true;
            }

            CheckDead();

            //Uppdaterar fienderna
            foreach(var e in EnemyArray)
            {
                if(e != null)
                {
                    e.Update();
                }
            }

            //Släpper lös fienderna
            if (TurnActivated)
            {
                ReleaseEnemies();
            }

            foreach(var p in PG)
            {
                p.Update();
            }
        }

        public void Draw()
        {
            foreach(var e in EnemyArray)
            {
                if (e != null)
                {
                    e.Draw();
                }
            }

            foreach(var p in PG)
            {
                p.Draw();
            }

        }

        //Släpper lös fienderna
        public void ReleaseEnemies()
        {
            EnemyReleaseTimer -= Globals.DeltaTime;

            if(EnemyReleaseTimer < 0 )
            {
                if (EnemyTracker == NumberOfEnemiesWave)
                {
                    if (!CheckEnemyStatus())
                    {
                        TurnActivated = false;
                        Upgraded = false;
                        EnemyTracker = 0;
                        TurnTracker++;
                    }
                }
                else
                {
                    EnemyReleaseTimer = EnemyReleaseInterval;
                    EnemyArray[EnemyTracker] = new Enemies(rnd.Next(EnemyLevelMin, EnemyLevelMax + 1), Path);
                    EnemyTracker++;
                    
                }
            }

            RemoveOutOfBounds();
        }

        //Öker svårighetsgraden efterhand som man spelar spelet
        public void IncreaseIntensity()
        {
            if(TurnTracker % 1 == 0)
            {
                if(EnemyReleaseInterval > EnemyReleaseMin) 
                {
                    EnemyReleaseInterval -= 0.1f;
                }

                NumberOfEnemiesWave++;
                EnemyArray = new Enemies[NumberOfEnemiesWave];
            }

            if(TurnTracker % 4 == 0)
            {
                if(EnemyLevelMax < 4)
                {
                    EnemyLevelMax++;
                }
            }

            if (TurnTracker % 6 == 0)
            {
                if (EnemyLevelMin < 4)
                {
                    EnemyLevelMin++;
                }
            }

        }

        //Ser till så att alla fiender är döda eller utanför banan innan man kan starta nästa runda
        public bool CheckEnemyStatus()
        {
            bool result = false;
            
            for(int i = 0; i < EnemyArray.Length; i++)
            {
                if(EnemyArray[i] != null)
                {
                    result = true;
                }
            }


            return result;
        }

        //Kollar om fiender är döda
        public void CheckDead()
        {
            for(int i = 0; i < EnemyArray.Length;i++)
            {
                if (EnemyArray[i] != null && EnemyArray[i].Health <= 0)
                {
                    if (EnemyArray[i].IsBoss)
                    {
                        EnemyArray[i].Level -= 1;
                        EnemyArray[i].SetHealthFull();

                        if (EnemyArray[i].Level < 1)
                        {
                            PG.Add(new ParticleGenerator(EnemyArray[i].Pos));
                            Resources.Gold += EnemyArray[i].GoldValue;
                            EnemyArray[i] = null;
                            Assets.EnemyKilled.Play();
                        }
                    }
                    else
                    {
                        PG.Add(new ParticleGenerator(EnemyArray[i].Pos));
                        Resources.Gold += EnemyArray[i].GoldValue;
                        EnemyArray[i] = null;
                        Assets.EnemyKilled.Play();
                    }

                }
                
            }
        }

        //Tar bort fiender som är utanför banan
        private void RemoveOutOfBounds()
        {
            for (int i = 0; i < EnemyArray.Length; i++)
            {
                if (EnemyArray[i] != null)
                {
                    if (EnemyArray[i].PathIndex > 2)
                    {
                        if (EnemyArray[i].Pos.X > Globals.WindowSize.X || EnemyArray[i].Pos.Y > Globals.WindowSize.Y || EnemyArray[i].Pos.X < 0 || EnemyArray[i].Pos.Y < 0)
                        {
                            Resources.Lives -= EnemyArray[i].Damage;
                            Assets.LoseLife.Play();
                            EnemyArray[i] = null;
                        }
                    }
                }
            }
        }

        //Hämtar fiende arrayn
        public Enemies[] GetEnemyArray()
        {
            return EnemyArray;
        }
    }
}
