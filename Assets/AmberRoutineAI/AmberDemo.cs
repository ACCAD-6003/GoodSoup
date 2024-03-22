using UnityEngine;

public class AmberDemo : MonoBehaviour {
    [SerializeField] grid_manager grid;
    [SerializeField] tile fridge;
    public void Start()
    {
        grid.Target(fridge, Arrived, Vector3.forward);
    }
    private void Arrived() {
        Debug.Log("Arrived at fridge.");
    }
}