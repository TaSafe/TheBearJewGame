using System.Collections;

public class NodePursueCheck : BTNode
{
    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (root.GetComponent<Enemy>().PursueState)
            status = Status.SUCCESS;

        yield break;
    }
}