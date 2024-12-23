﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.AI.Bedroom
{
    public class FarCrySwitcher : MonoBehaviour
    {
        private float time = 0f;
        private void Update()
        {
            if (time > 60 * 2f)
            {
                SceneManager.LoadScene("Endings");
            }
            time += Time.deltaTime;
        }
        public void Stop() {
            Destroy(gameObject);
        }
    }
}
