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
        PathFindToFridge();
    }
    private void PathFindToFridge() {
        grid.Target(fridge, Arrived, Vector3.back);
    }
    private void Arrived() {
        Debug.Log("Arrived at fridge.");
    }
    private void CheckIfICareAboutBurner(float oldVal, float newVal) {
        if (newVal >= Globals.HEAT_THRESHOLD && focus != AmberFocus.Burner) {
            focus = AmberFocus.Burner;
            grid.Target(burner.AssociatedTile, burner.AmberInteraction.DoAction, Vector3.forward);
            burner.AmberInteraction.OnActionEnding += () => { 
                focus = AmberFocus.Fridge;
                PathFindToFridge(); 
            };
        }
    }

}