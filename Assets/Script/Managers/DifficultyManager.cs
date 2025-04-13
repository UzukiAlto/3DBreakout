using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public GM gm;
    public GameObject assistPanel;
    
    public void SetAssistPanel(GM.Difficulty difficulty)
    {
        switch (difficulty)
        {
            case GM.Difficulty.Normal:
                break;
            case GM.Difficulty.Hard:
                break;
            case GM.Difficulty.ScoreAttack:
                break;
        }
    }
}
