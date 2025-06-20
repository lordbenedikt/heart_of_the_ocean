using UnityEngine;
using UnityEditor;

public static class QuickEditHotkeys
{
    [MenuItem("Tools/QuickEdit/Set Z = 0 for Selected Objects &z")] // ALT+Z hotkey
    public static void SetZPositionToZero()
    {
        MoveSelectionTo(null, null, 0f);
    }

    [MenuItem("Tools/QuickEdit/Set X = 0 for Selected Objects &x")] // ALT+Z hotkey
    public static void SetXPositionToZero()
    {
        MoveSelectionTo(null,0f, null);
    }

    [MenuItem("Tools/QuickEdit/Set Y = 0 for Selected Objects &y")] // ALT+Z hotkey
    public static void SetYPositionToZero()
    {
        MoveSelectionTo(0f, null, null);
    }

    public static void MoveSelectionTo(float? x, float? y, float? z)
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj.transform, "Set Z Position to 0");
            Vector3 pos = obj.transform.position;
            obj.transform.position = new Vector3(x ?? pos.x, y ?? pos.y, z ?? pos.z);
            EditorUtility.SetDirty(obj);
        }
    }
}
