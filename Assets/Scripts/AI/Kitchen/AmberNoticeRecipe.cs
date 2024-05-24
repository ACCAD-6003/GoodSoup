using Assets.Scripts.AI;
using Assets.Scripts.UI;
using BehaviorTree;
using UnityEngine;

class AmberNoticeRecipe : Node
{
    bool _increased = false;
    bool _coolingDown = false;
    float timePassed = 0f;
    ObjectInteraction objInteraction;
    public InteractableObject _floatingRecipe;
    void Awake()
    {
        objInteraction = FindObjectOfType<ObjectInteraction>();
    }
    public override NodeState Evaluate()
    {
        if (!StoryDatastore.Instance.GoodSoupPuzzleSolved.Value)
        {
            if (!_coolingDown && objInteraction.IsInAmberSightlines(_floatingRecipe) && _floatingRecipe.gameObject.activeInHierarchy)
            {
                if (!_increased)
                {
                    _increased = true;
                    StoryDatastore.Instance.Paranoia.Value += 3f;
                }
                UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.PARANOID, 2f);
                _coolingDown = true;
            }
            else
            {
                timePassed += Time.deltaTime;
                if (timePassed > 7f)
                {
                    timePassed = 0f;
                    _coolingDown = false;
                }
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}