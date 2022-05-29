using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OrderDataBase))]

public class OderDataBaseInsp : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (OrderDataBase)target;

        if (GUILayout.Button("Add random Order", GUILayout.Height(40)))
        {
            Debug.Log("add Order");
            script.AddOrder(1);
        }

    }
}
