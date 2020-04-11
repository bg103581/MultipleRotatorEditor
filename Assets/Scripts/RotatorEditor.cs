using UnityEngine;
using UnityEditor;

namespace com.technical.test
{
    /// <summary>
    ///     Enable the user to click on a button in the Rotator script in the inspector
    ///     to open the custom "Rotators Mass Setter" window.
    /// </summary>
    [CustomEditor(typeof(Rotator))]
    public class RotatorEditor : Editor
    {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            //Add a button at the end of the Rotator script to open the custom window
            if (GUILayout.Button("Rotators Mass Setter")) {
                MultipleRotatorEditorWindow.ShowWindow();
            }
        }
    }
}