using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetPos : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Cool Tools/Shortcuts/Reset Transform #s")]
    public static void ResetTransform()
    {
        var selectedObject = Selection.activeGameObject;

        Undo.RecordObject(selectedObject.transform, "Reset Transform on " + selectedObject.name);

        if (!selectedObject)
        {
            return;
        }

        selectedObject.transform.position = Vector3.zero;

        EditorUtility.SetDirty(selectedObject.transform);
    }
#endif
}
