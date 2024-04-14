using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwapShirt : Interaction
{
    [SerializeField] ClothingOption startingShirt;
    [SerializeField] Shirts shirtOptions;
    [SerializeField] Shirts shirtOnBed;
    [SerializeField] int ID;
    private void Awake()
    {
        if (!StoryDatastore.Instance.DisplayedShirts.ContainsKey(ID)) {
            StoryDatastore.Instance.DisplayedShirts.Add(ID, new StoryData<ClothingOption>(startingShirt));
        }
        RefreshShirt();
    }
    private void RefreshShirt()
    {
        foreach (var shirt in shirtOptions.AllShirts) {
            shirt.Value.SetActive(false);
        }
        if (StoryDatastore.Instance.DisplayedShirts[ID].Value == ClothingOption.None) {
            return;
        } 
        shirtOptions.AllShirts[StoryDatastore.Instance.DisplayedShirts[ID].Value].SetActive(true);
    }
    public override void LoadData(StoryDatastore data)
    {
        RefreshShirt();
    }

    public override void SaveData(StoryDatastore data)
    {

    }

    public override void DoAction()
    {
        ClothingOption clothes = StoryDatastore.Instance.DisplayedShirts[ID].Value;
        StoryDatastore.Instance.DisplayedShirts[ID].Value = StoryDatastore.Instance.ChosenClothing.Value;
        RefreshShirt();
        foreach (var shirt in shirtOnBed.AllShirts)
        {
            shirt.Value.SetActive(false);
        }
        if (clothes != ClothingOption.None)
        {
            shirtOnBed.AllShirts[clothes].SetActive(true);
        }
        StoryDatastore.Instance.ChosenClothing.Value = clothes;
        EndAction();
    }
    private void Update()
    {
        if (StoryDatastore.Instance.ShirtPickedUp.Value) {
            Destroy(this);
        }
    }
}
