using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Check out http://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header/
/// </summary>
namespace SamuraiGame
{
    [InitializeOnLoad]
    public class HierarchyLabel : MonoBehaviour
    {
        static HierarchyLabel()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (obj != null && obj.name.StartsWith("---", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.black);
                EditorGUI.DropShadowLabel(selectionRect, obj.name.Replace("-", "").ToString());
            }
            //HighlightObj(obj, "LedgeChecker", selectionRect, Color.green);
            //HighlightObj(obj, "BoxColliderUpdater", selectionRect, Color.yellow);
        }
        static void HighlightObj(GameObject obj, string name, Rect selectionRect, Color color)
        {
            if (obj != null && obj.name.Equals(name))
            {
                EditorGUI.DrawRect(selectionRect, color);
                EditorGUI.DropShadowLabel(selectionRect, obj.name);
            }
        }
    }
}