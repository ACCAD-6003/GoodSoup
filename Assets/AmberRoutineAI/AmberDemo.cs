using UnityEngine;

public class AmberDemo : MonoBehaviour {
    [SerializeField] grid_manager grid;
    [SerializeField] tile fridge;
    [SerializeField] InteractableObject burner;
    private StoryDatastore data;
    enum AmberFocus { Fridge, Burner }
    private AmberFocus focus = AmberFocus.Fridge;
    public void Start()
    {
        data = FindObjectOfType<StoryDatastore>();
        data.BurnerHeat.Changed += CheckIfICareAboutBurner;
        grid.Target(fridge, Arrived, Vector3.forward);
    }
    private void Arrived() {
        Debug.Log("Arrived at fridge.");
    }
    private void CheckIfICareAboutBurner(float oldVal, float newVal) {
        if (newVal >= Globals.HEAT_THRESHOLD && focus != AmberFocus.Burner) {
            focus = AmberFocus.Burner;
            grid.Target(burner.AssociatedTile, burner.AmberInteraction.DoAction, Vector3.forward);
        }
    }
}