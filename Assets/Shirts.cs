using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Shirts : SerializedMonoBehaviour
{
    public Dictionary<ClothingOption, GameObject> AllShirts = new Dictionary<ClothingOption, GameObject>();
}