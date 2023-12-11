﻿using Spline;
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
        private int EnemyLevelMax, EnemyLevelMin, NumberOfEnemiesWave, TurnTracker, EnemyTracker;
        private bool Upgraded;
        public bool TurnActivated;
        private Enemies[] EnemyArray;
        private SimplePath Path;
        private Random rnd;


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
        }

        public void Update()
        {
            //Gör det svårare nästa runda
            if(TurnActivated && !Upgraded)
            {
                IncreaseIntensity();
                Upgraded = true;
            }

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

            Debug.WriteLine(EnemyArray[1] == null);
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
                    RemoveOutOfBounts();
                }
            }
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
            
            foreach(var e in EnemyArray)
            {
                if(e != null)
                {
                    result = true;
                }
            }

            return result;
        }

        private void RemoveOutOfBounts()
        {
            for (int i = 0; i < EnemyArray.Length; i++)
            {
                if (EnemyArray[i] != null)
                {
                    if (EnemyArray[i].PathIndex > 2)
                    {
                        if (EnemyArray[i].Pos.X > Globals.WindowSize.X || EnemyArray[i].Pos.Y > Globals.WindowSize.Y || EnemyArray[i].Pos.X < 0 || EnemyArray[i].Pos.Y < 0)
                        {
                            EnemyArray[i] = null;
                        }
                    }
                }
            }
        }
        public Enemies[] GetEnemyArray()
        {
            return EnemyArray;
        }
    }
}