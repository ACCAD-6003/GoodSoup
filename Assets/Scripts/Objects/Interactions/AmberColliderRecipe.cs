using UnityEngine;

public class AmberColliderRecipe : MonoBehaviour {
    [SerializeField] StartRecipeSequence srs;
    private void OnCollisionEnter(Collision collision)
    {
        // layer NEEDS to be changed
        if (collision.collider.gameObject.layer == 1259) {
            srs.CollideWithAmber();
        }
    }
}