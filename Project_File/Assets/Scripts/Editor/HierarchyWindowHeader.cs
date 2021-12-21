using UnityEngine;
using UnityEditor;

//Simply re-styles a gameObject name in the Hiearchy window to be black and all caps.
//Allows us to seperate our gameObjects and not lose our minds.

[InitializeOnLoad]
public static class HierarchySectionHeader
{
    static HierarchySectionHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null && 
            (gameObject.name.StartsWith("/r/", System.StringComparison.Ordinal) || 
            gameObject.name.StartsWith("/y/", System.StringComparison.Ordinal) ||
            gameObject.name.StartsWith("/b/", System.StringComparison.Ordinal) ||
            gameObject.name.StartsWith("/c/", System.StringComparison.Ordinal) ||
            gameObject.name.StartsWith("/m/", System.StringComparison.Ordinal)))
        {
            if (gameObject.name.StartsWith("/y/", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.yellow);
                EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("/y/", "").ToUpperInvariant());
            }
            else if (gameObject.name.StartsWith("/r/", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.red);
                EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("/r/", "").ToUpperInvariant());
            }
            else if (gameObject.name.StartsWith("/b/", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.blue);
                EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("/b/", "").ToUpperInvariant());
            }
            else if (gameObject.name.StartsWith("/c/", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.cyan);
                EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("/c/", "").ToUpperInvariant());
            }
            else if (gameObject.name.StartsWith("/m/", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.magenta);
                EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("/m/", "").ToUpperInvariant());
            }
        }
    }
}