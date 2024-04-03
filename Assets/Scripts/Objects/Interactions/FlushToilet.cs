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
        private StoryData<float> _temperature;
        public override void LoadData(StoryDatastore data)
        {
            _temperature = data.ShowerTemperature;
        }

        public override void SaveData(StoryDatastore data)
        {

        }

        protected override void DoAction()
        {
            StoryDatastore.Instance.Annoyance.Value += 0.2f;
            _temperature.Value += Globals.FLUSH_SHOWER_TEMP_IMPACT;
            _temperature.Value = MathF.Max(0f, _temperature.Value);
            EndAction();
        }
    }
}
