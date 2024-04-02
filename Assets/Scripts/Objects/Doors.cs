using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainSceneLoading;

namespace Assets.Scripts.Objects
{
    public class Doors : SerializedMonoBehaviour
    {
        public void Awake()
        {
            Entrance = doors[StoryDatastore.Instance.CurrentAmberRoom.Value];
        }
        public Dictionary<AmberRoom, tile> doors;
        public tile Entrance;
    }
}
