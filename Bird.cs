using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Flappy_Birds
{
    public class Bird
    {
        public Sound fly;   
        public Vector2 position;
        public Vector2 birdCenter;
        public float birdRadius;
        // tajmer za menjanje teksture
        public float aniTimer;
        public float x;
        public float y;
        // brzina pada, tj y osa
        public float VelocityY;
        public float JumpForce = -250f;
        public float Gravity = 400f;

        public float rotation;
        
        // pravimo 3 tekstue, jedna koja pokazuje pravo pticu, druga pokazuje kada rasiri krila kada stisnemo space, a trecu da menja izmedju te dve kada se radnja dogodi
        public Texture2D textureMid;
        public Texture2D textureUp;
        public Texture2D CurrentTexture;

        public Bird()
        {
            
            // postavljamo vrednosti gde ce se ptica stvoriti, na osi x i osi y
            x = 100;
            y = 200;
            // prvobitno je jednako 0
            VelocityY = 0f;

            
            rotation = 0;
            // dodajemo teksture ovom komandom i kopiramo path naseg fajla
            textureMid = Raylib.LoadTexture("flappy-bird-assets-master\\sprites\\yellowbird-midflap.png");
            textureUp = Raylib.LoadTexture("flappy-bird-assets-master\\sprites\\yellowbird-upflap.png");

            // na pocetku odma dodajemo vrednost mid-a kako ptica pada (slika tj tekstura)
            CurrentTexture = textureMid;
            
            UpdateCollision();
            fly = Raylib.LoadSound("flappy-bird-assets-master\\audio\\wing.wav");

        }
        public void Reset()
        {
            x = 100;
            y = 200;
            VelocityY = 0f;
            CurrentTexture = textureMid;
            UpdateCollision();
        }

        public void UpdateCollision()
        {
            birdCenter = new Vector2(x, y);
            birdRadius = CurrentTexture.Width / 2f;
        }
        // ovom metodom iscrtavamo pticu
        public void DrawBird()
        {
            rotation = VelocityY * 0.15f;

            if (rotation < -20f)
            {
                rotation = -20f;
            }
            if (rotation > 70f)
            {
                rotation = 70f;
            }
            Rectangle srcRec = new Rectangle(0, 0, CurrentTexture.Width, CurrentTexture.Height);
            Rectangle decRec = new Rectangle(x, y, CurrentTexture.Width, CurrentTexture.Height);
            Vector2 origin = new Vector2(CurrentTexture.Width / 2f, CurrentTexture.Height / 2f);

            position = new Vector2(x, y);
            Raylib.DrawTexturePro(CurrentTexture, srcRec, decRec, origin, rotation, Color.White);
        }

        // ovde racunamo desavanja kod ptice, koristimo deltaTime koji je ustv fps koji dobija igrac
        public void Update(float deltaTime)
        {
            
            // kada se stisne space, brzina pada ce biti jednaka jacini skoka, sto je trenutno -200f
            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {

                VelocityY = JumpForce;
                CurrentTexture = textureUp;
                rotation += 10f;
                // dajemo tajmeru vrednost da bi mogli videti tu malu teksturu (da izgleda kao animacija)
                aniTimer = 0.2f;
                Raylib.PlaySound(fly);
            }
            // dodajemo uslov koliko dugo ce trajati ta tekstura i vracamo na staro. Oduzimamo je sa fps-om da bi radilo glatko na svakom racunalu
            if(aniTimer > 0)
            {
                
                aniTimer -= deltaTime;
                if(aniTimer <= 0)
                {
                    CurrentTexture = textureMid;
                    rotation = 0;
                }
            }
            
            // tu snagu pada sabiramo sa pomnozenom gravitacijom (koja je tenutno 400f) i frejmom i dajemo tu vrednost VelocityY-nu
            // stavljamo += da bi to radilo glatko, a ne da bi seckalo, radi brzeg i boljeg rada.
            VelocityY += Gravity * deltaTime;

            // napokon sabiramo vrednost y (za sada 100f) sa pomnozenim jacinom pada i frejmom.
            // tu stavljamo takodje += da bi radilo glatko, a ne seckalo ka dole ili ka gore i radi brzeg i boljeg rada.
            y += VelocityY * deltaTime;

            UpdateCollision();

           
            
        }

         
    }
}
