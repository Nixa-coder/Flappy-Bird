using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Flappy_Birds
{
    public class Pipes
    {
        public bool passed = false;
        public Rectangle ScoreZone;
        public Rectangle pipeUpRec;
        public Rectangle pipeDownRec;
        // pravimo 2 teksture zato sto ce jedna biti nacrtana obrnuto.
        // za svaku teksturu pravimo vektor za te dve da im dodamo vrednost gde bi se stvorili
        // dodajemo brzinu za pipes (trenutno mozda promenim i izbacim)
        // radi lakseg inputa za mene ( jer kada okrecemo teksturu malo mu se menjaju koordinate), stavljam promenljive za svaku osu u vektorima
        public Texture2D pipeUp;
        public Texture2D pipeDown;
        public Vector2 pU;
        public Vector2 pD;
        public float speed = 80f;
        public float x1;
        public float x2;
        public float y1;
        public float y2;

        public Pipes()
        {
            
            // dodajemo pocetne vrednosti osama, vektorima i dajemo teksturu nasim teksturama
            x1 = 400;
            x2 = 400;
            y1 = 300;
            y2 = 500;
            pU = new Vector2(x1, y1);
            pD = new Vector2(x2, y2);
            pipeUp = Raylib.LoadTexture("flappy-bird-assets-master\\sprites\\pipe-green.png");
            
            pipeDown = Raylib.LoadTexture("flappy-bird-assets-master\\sprites\\pipe-green.png");
            UpdatePipeCollision();
        }
        public void Reset()
        {
            pU = new Vector2(400, 300);
            pD = new Vector2(400, 500);
            speed = 80f;
            UpdatePipeCollision();
        }

        public void UpdatePipeCollision()
        {
            pipeUpRec = new Rectangle(pU.X, pU.Y - (pipeUp.Height * 2), pipeUp.Width * 2, pipeUp.Height * 2);
            pipeDownRec = new Rectangle(pD.X, pD.Y, pipeDown.Width * 2, pipeDown.Height * 2);

            float gapUpDown = pD.Y - pU.Y;
            ScoreZone = new Rectangle(pD.X + (pipeDown.Width * 2), pU.Y, 0, gapUpDown);
            
        }
        // ovo ce nam sluziti za iscrtavanje prve prepreke
        public void PipesDraw()
        {
            // Pošto rotacija -180 povlači sliku ulevo za njenu širinu, 
            // samo ovde dodamo (pipeUp.Width * 2) na X poziciju pri crtanju:
            Vector2 offsetPU = new Vector2(pU.X + (pipeUp.Width * 2), pU.Y);

            Raylib.DrawTextureEx(pipeUp, offsetPU, -180, 2, Color.White);
            Raylib.DrawTextureEx(pipeDown, pD, 0, 2, Color.White);

            Raylib.DrawRectangleRec(ScoreZone, Color.White);
            
        }

        // ovo je metoda koja dobija fps od kompjutera, daje random vrednost Y osi zbog prepreka da budu teze.
        // terenutno povecavam brzinu svaki put kada se prepreke vrate
        public void PipesUpdate(float deltaTime)
        {
            pU.X -= speed * deltaTime;
            pD.X -= speed * deltaTime;

            if (pU.X <= 0 && pD.X <= 0)
            {
                speed += 20f;
                if(speed >= 700f)
                {
                    speed = 710f;
                }
                pD.X = 600;
                pU.X = 600;

                passed = false;
                pD.Y = Raylib.GetRandomValue(400, 620);
                pU.Y = Raylib.GetRandomValue(100, 340);

                

            }

            UpdatePipeCollision();
        }

    }
}
