using UnityEngine;

public class BackgroundLayerOrdering : MonoBehaviour
{
    public void SortBackgroundElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.GetComponent<SpriteRenderer>().sortingOrder = i * 10;
        }
    }
}
