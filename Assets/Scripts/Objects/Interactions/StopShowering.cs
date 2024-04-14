using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Objects.Interactions
{
    internal class StopShowering : Interaction
    {
        public override void DoAction()
        {
            StoryDatastore.Instance.PlayerTurnedShowerOff.Value = true;
            EndAction();
            Destroy(this);
        }

        public override void LoadData(StoryDatastore data)
        {

        }

        public override void SaveData(StoryDatastore data)
        {

        }
    }
}
