using Glyph_Game_Engine.Graphics;
using System.Diagnostics;

namespace Glyph_Game_Engine.Runtime;
public abstract class RuntimeLoop
{
    public void GameLoop(bool gameRunning, int desiredFPS, int desiredUPS)
    {
        int frameCount = 0;

        double fOptimalTime = 1_000 / desiredFPS; // Optimal time to draw (1,000ms or 1 second divided by maxFPS/UPS)
        double uOptimalTime = 1_000 / desiredUPS; // Optimal time to update

        double uDeltaTime = 0, fDeltaTime = 0; // Update delta time, fps delta time
        Stopwatch stopwatch = Stopwatch.StartNew();
        long startTime = stopwatch.ElapsedMilliseconds;

        Stopwatch fpsTimer = Stopwatch.StartNew();
        int frames = 0;
        int updates = 0;
        
        while (gameRunning)
        {
            // Calculate difference in time
            long currentTime = stopwatch.ElapsedMilliseconds;
            fDeltaTime += (currentTime - startTime);
            uDeltaTime += (currentTime - startTime);
            startTime = currentTime;
            
            // If amount of time since last frame is the optimal time to update, then update.
            if (uDeltaTime >= uOptimalTime)
            {
                UpdateLoop();

                uDeltaTime -= uOptimalTime;
                updates += 1;
            }

            if (fDeltaTime >= fOptimalTime)
            {
                RenderLoop();
                
                if (frameCount >= desiredFPS)
                {
                    // Calculate frame rate and updates per second.
                    double elapsedSeconds = fpsTimer.Elapsed.TotalSeconds;
                    double fps = frames / elapsedSeconds;
                    double ups = updates / elapsedSeconds;
                    
                    fpsTimer.Restart();

                    Console.WriteLine("FPS: " + fps + "\nUPS: " + ups);
                    
                    frames = 0;
                    updates = 0;
                }
                
                fDeltaTime -= fOptimalTime;
                frames += 1;
            }
            
            Console.CursorVisible = false;

            frameCount++;
        }
    }

    // This method is overrided by the user and called in GameLoop();
    // Use this method to render your assets, etc.
    protected abstract void RenderLoop();
    
    // This method is overrided by the user and called in GameLoop();
    // Use this method to update your game (e.g. positions of player/enemy)
    protected abstract void UpdateLoop();
}