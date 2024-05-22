using BehaviorTree;

public class DuplicateAndRunNode : Node
{
    public Node nodeToCopy;
    Node newNode;
    public void Awake()
    {
        newNode = Instantiate(nodeToCopy.gameObject).GetComponent<Node>();
    }
    public override NodeState Evaluate()
    {
        state = newNode.Evaluate();
        return state;

    }
}
