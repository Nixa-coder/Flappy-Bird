using Raylib_cs;
using System.Numerics;

namespace Flappy_Birds
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(600, 800, "Flappy Bird");
            Raylib.InitAudioDevice();
            Vector2 v = new Vector2(0, -100);
            Texture2D background = Raylib.LoadTexture("flappy-bird-assets-master\\sprites\\background-day.png");
            Bird b = new Bird();
            Pipes p = new Pipes();
            Coallisions Game = new Coallisions();
            
            

            
            
            

            Raylib.SetTargetFPS(60);
            while(!Raylib.WindowShouldClose())
            {
                
                float frame = Raylib.GetFrameTime();

                if (!Game.GameOver)
                {
                    b.Update(frame);
                    p.PipesUpdate(frame);
                    
                    Game.Collisions(b, p);
                }
                else
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.R))
                    {
                        b.Reset();
                        p.Reset();
                        Game.GameOver = false;
                        Game.Reset();
                    }
                }


                Raylib.BeginDrawing();
                

                Raylib.ClearBackground(Color.White);
                Raylib.DrawTextureEx(background, v, 0, 2.1f, Color.White);
                
                b.DrawBird();
                p.PipesDraw();
                Game.Drawing();

                
                
                

                Raylib.EndDrawing();

                
            }

            Raylib.UnloadSound(b.fly);
            Raylib.UnloadSound(Game.yay);
            Raylib.UnloadSound(Game.fail);
            Raylib.UnloadTexture(p.pipeUp);
            Raylib.UnloadTexture(p.pipeDown);
            Raylib.UnloadTexture(b.CurrentTexture);
            Raylib.UnloadTexture(b.textureMid);
            Raylib.UnloadTexture(b.textureUp);
            Raylib.UnloadTexture(background);
            Raylib.CloseWindow();

        }
    }
}
