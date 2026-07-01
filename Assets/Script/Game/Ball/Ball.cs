using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    public class Ball : MonoBehaviour
    {
        public event Action<Collision> OnCollided;

        private Rigidbody myRB;
        void Awake()
        {
            myRB = GetComponent<Rigidbody>();
        }
        void OnCollisionEnter(Collision collision)
        {
            OnCollided?.Invoke(collision);
        }
    }
}