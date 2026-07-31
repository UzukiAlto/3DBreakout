using System.Collections;
using System.Collections.Generic;
namespace Game
{
    public static class GameState
    {
        
        public static float gameSpeed { get; private set; } = 1f;
        public static int currentStage { get; private set; } = 1;
        private static float speedIncreaseRate = 1.1f;
        
        public static void Initialize()
        {
            gameSpeed = 1f;
            currentStage = 1;
        }

        public static void NextStage()
        {
            currentStage++;
            // ステージ6までは補助壁が消えるだけなのでスピードは増加させず、ステージ7からは3ステージごとに増加
            if (currentStage > 6  && (currentStage - 1) % 3 == 0) 
            {
                gameSpeed *= speedIncreaseRate;
            }
        }
        
    }
}