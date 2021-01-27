using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SetSpriteSortingOrder : MonoBehaviour
{
    [SerializeField] private int addToCurrentSortingOrder;
    [SerializeField] private List<Transform> gameObjetParentsToSet;

#if UNITY_EDITOR
    public void SetSortingOrder()
    {
        foreach (Transform parent in gameObjetParentsToSet)
        {
            foreach (SpriteRenderer spriteRenderer in parent.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sortingOrder += addToCurrentSortingOrder;
                EditorUtility.SetDirty(spriteRenderer);
            }
        }

        gameObjetParentsToSet.Clear();
    }
#endif
}