using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
/**
 * Reference: https://stackoverflow.com/questions/62795915/unity-custom-drop-down-selection-for-arrays-c-sharp
 */
[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);
        int selectedSceneIndex = GetSceneIndex(property.stringValue);
        if (selectedSceneIndex == -1)
            selectedSceneIndex = 0;
        selectedSceneIndex = EditorGUI.Popup(position, label.text, selectedSceneIndex, GetSceneNames());
        property.stringValue = GetScenePath(selectedSceneIndex);
        EditorGUI.EndProperty();
    }

    private int GetSceneIndex(string scenePath)
    {
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            if (EditorBuildSettings.scenes[i].path == scenePath)
                return i;
        }
        return -1;
    }
    
    private string[] GetSceneNames()
    {
        string[] allBuiltScenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < allBuiltScenes.Length; i++)
        {
            allBuiltScenes[i] = System.IO.Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
        }
        return allBuiltScenes;
    }

    private string GetScenePath(int index) => EditorBuildSettings.scenes[index].path;

}
#endif