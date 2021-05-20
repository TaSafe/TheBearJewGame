using System.Collections;

public class NodePuersueCheck : BTNode
{
    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (root.GetComponent<Enemy>().PursueState)
            status = Status.SUCCESS;

        yield break;
    }
}