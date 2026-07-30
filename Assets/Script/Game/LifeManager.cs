using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LifeManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject lifeParent;
        [SerializeField] private List<GameObject> lifeObjects;
        private int defaultLives = 3;
        private int maxLives = 5;
        private int currentLives;
        public void Initialize()
        {
            lifeObjects = new List<GameObject>();
            currentLives = defaultLives;
            for (int i = 0; i < maxLives; i++)
            {
                GameObject life = lifeParent.transform.GetChild(i).gameObject;
                lifeObjects.Add(life);

                if (i < defaultLives)
                {
                    life.SetActive(true);
                }
                else
                {
                    life.SetActive(false);
                }
            }
            
        }

        public bool decreaseLife()
        {
            bool isGameOver = false;
            currentLives--;
            if (currentLives <= 0)
            {
                isGameOver = true;
            } 
            else
            {
                lifeObjects[currentLives].SetActive(false);
            }
            return isGameOver;
        }

    }
}