using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Raylib_cs;

namespace Flappy_Birds
{
    public class Coallisions
    {
        public Sound yay;
        public Sound fail;

        public int points;
        
        public bool GameOver = false;

        public Coallisions()
        {
            points = 0;
            yay = Raylib.LoadSound("flappy-bird-assets-master\\audio\\point.wav");
            fail = Raylib.LoadSound("flappy-bird-assets-master\\audio\\die.wav");
        }
        public void Collisions(Bird bird, Pipes pipes)
        {
            if (GameOver) return;
            bool HitDown = Raylib.CheckCollisionCircleRec(bird.birdCenter, bird.birdRadius, pipes.pipeDownRec);
            bool HitUp = Raylib.CheckCollisionCircleRec(bird.birdCenter, bird.birdRadius, pipes.pipeUpRec);

            bool Hit = Raylib.CheckCollisionCircleRec(bird.birdCenter, bird.birdRadius, pipes.ScoreZone);

            bool GroundColl = bird.y >= 800;
            bool SkyColl = bird.y <= -30;

            if(HitDown || HitUp || GroundColl || SkyColl)
            {
                Raylib.PlaySound(fail);
                GameOver = true;
            }

            if(Hit && !pipes.passed)
            {
                points += 1;
                Raylib.PlaySound(yay);
                pipes.passed = true;
            }

        }

        
        public void Drawing()
        {
            Raylib.DrawText($"{points}", 20, 20, 30, Color.Black);
            if (GameOver)
            {
                
                Raylib.DrawText("GGS BUDDY", 300, 400, 30, Color.Black);
                Raylib.DrawText("Press R to play again", 220, 430, 30, Color.Black);
                
            }
            
        }

        public void Reset()
        {
            points = 0;
        }

        
    }
}
