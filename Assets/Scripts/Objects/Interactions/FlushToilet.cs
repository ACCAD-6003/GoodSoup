using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Interactions
{
    public class FlushToilet : Interaction
    {
        public AmberUseShower shower;
        private StoryData<float> _temperature;
        public Animator _combspin;
        bool _flushed = false;
        public override void LoadData(StoryDatastore data)
        {
            _temperature = data.ShowerTemperature;
        }

        public override void SaveData(StoryDatastore data)
        {

        }

        public override void DoAction()
        {
            if (!_flushed && shower._showerState == AmberUseShower.ShowerState.SHOWERING) {
                StoryDatastore.Instance.Annoyance.Value += 1.5f;
                _flushed = true;
            }
            FindObjectOfType<CameraShake>().ShakeCamera();
            _combspin.SetTrigger("Flush");

			_temperature.Value += Globals.FLUSH_SHOWER_TEMP_IMPACT;
            _temperature.Value = MathF.Max(0f, _temperature.Value);
            EndAction();
        }
    }
}
