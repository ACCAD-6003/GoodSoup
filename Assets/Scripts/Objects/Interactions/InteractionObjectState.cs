using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Interactions
{
    internal class InteractionObjectState : MonoBehaviour
    {
        [SerializeField] GameObject gameObj;
        public virtual void SetState(bool enabled) {
            gameObj.SetActive(enabled);
        }
    }
}
