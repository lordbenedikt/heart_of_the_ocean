using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;

[InitializeOnLoad]
public static class BlenderTransformations
{
    // private static Vector3 grabStartWorldPos;
    // private static Vector3 grabOffset;
    // private static bool isGrabbing = false;

    // private static Plane movementPlane;
    // private static Vector2 lastMousePos;

    // static BlenderTransformations()
    // {
    //     SceneView.duringSceneGui += OnSceneGUI;
    // }

    // public static Quaternion GetSceneCameraRotation()
    // {
    //     SceneView sceneView = SceneView.lastActiveSceneView;
    //     if (sceneView != null && sceneView.camera != null)
    //     {
    //         return sceneView.camera.transform.rotation;
    //     }
    //     return Quaternion.identity; // fallback
    // }

    // [Shortcut("BlenderLike/Grab", KeyCode.G, ShortcutModifiers.Shift)]
    // private static void ToggleGrab()
    // {
    //     isGrabbing = !isGrabbing;
    // }

    // private static void OnSceneGUI(EditorWindow window)
    // {
    //     Vector2 mousePos = Event.current.mousePosition;
    //     Vector2 deltaOrig = mousePos - lastMousePos;
    //     lastMousePos = mousePos;

    //     Vector3 delta = new Vector3(deltaOrig.x, -deltaOrig.y, 0) * 0.1f; // Convert to world space

    //     if (Selection.activeTransform == null)
    //     {
    //         isGrabbing = false;
    //         return;
    //     }

    //     Event e = Event.current;
    //     Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

    //     if (isGrabbing)
    //     {
    //         foreach (var t in Selection.transforms)
    //         {
    //             Undo.RecordObject(t, "Blender Grab");
    //             t.position += delta;
    //             Debug.Log($"Moving {t.name} by {delta}");
    //         }
    //     }

    //     switch (e.type)
    //     {
    //         case EventType.MouseDown:
    //             isGrabbing = false;
    //             break;
    //     }
    // }
}
