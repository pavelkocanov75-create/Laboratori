using UnityEditor;
using UnityEngine;
 
public class ApplySelectedPrefabs : EditorWindow
{
    [MenuItem ("Tools/Apply all selected prefabs %#w",false,5)]
    private static void ApplyPrefabs()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            PrefabUtility.ApplyPrefabInstance(go,InteractionMode.UserAction);
        }
    }
   
    [MenuItem ("Tools/Revert all selected prefabs %#r",false,6)]
    private static void ResetPrefabs()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            PrefabUtility.RevertObjectOverride(go, InteractionMode.UserAction);
        }
    }
}