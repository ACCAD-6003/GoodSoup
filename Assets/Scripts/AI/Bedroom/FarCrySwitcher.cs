using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.AI.Bedroom
{
    public class FarCrySwitcher : MonoBehaviour
    {
        private float time = 0f;
        private void Update()
        {
            if (time > 2f)
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
