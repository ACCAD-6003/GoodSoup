using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class AmberMount : MonoBehaviour
    {
        public Action CompletedMounting;
        [SerializeField] private readonly GameObject _mount;
        [SerializeField] private readonly bool _enabledOnLoad = false;
        void Awake() {
            _mount.SetActive(_enabledOnLoad);
        }
        public void Mount() {
            StartCoroutine("MountWithDelay");
        }
        private IEnumerator MountWithDelay() {
            yield return new WaitForSeconds(0.3f);
            _mount.SetActive(true);
            foreach (AmberMount mount in FindObjectsOfType<AmberMount>())
            {
                if (mount != this)
                {
                    mount.UnMount();
                }
            }
            yield return new WaitForSeconds(0.3f);
            CompletedMounting?.Invoke();
        }
        public void UnMount() {
            _mount.SetActive(false);
        }
    }
}