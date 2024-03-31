using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class AmberMount : MonoBehaviour
    {
        public Action CompletedMounting;
        public Vector3 bubbleOffset;
        [SerializeField] private GameObject _mount;
        [SerializeField] private bool _enabledOnLoad = false;
        void Awake() {
            if (_enabledOnLoad) {
                PerformMount();
                return;
            }
            _mount.SetActive(false);
        }
        public void Mount() {
            StartCoroutine(nameof(MountWithDelay));
        }
        private void PerformMount() {
            ThoughtBubble.offset = bubbleOffset;
            _mount.SetActive(true);
            ObjectInteraction.Amber = _mount.transform;

        }
        private void UnMountAllOthers() {
            foreach (AmberMount mount in FindObjectsOfType<AmberMount>())
            {
                if (mount != this)
                {
                    mount.UnMount();
                }
            }
        }
        private IEnumerator MountWithDelay() {
            yield return new WaitForSeconds(0.3f);
            PerformMount();
            UnMountAllOthers();
            yield return new WaitForSeconds(0.3f);
            CompletedMounting?.Invoke();
        }
        public void UnMount() {
            _mount.SetActive(false);
        }
    }
}