using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetSpriteSortingOrder))]
public class SetSpriteSortingOrderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetSpriteSortingOrder myTarget = (SetSpriteSortingOrder)target;

        if (GUILayout.Button("Set sorting order"))
        {
            myTarget.SetSortingOrder();
        }
    }
}