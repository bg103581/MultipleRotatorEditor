using UnityEngine;
using UnityEditor;

namespace com.technical.test
{
    /// <summary>
    ///     The Rotator editor window.  
    ///     This tool allows to ​select gameObjects ​in the scene ​that contains a Rotator ​component 
    ///     and ​change their variables​ to those set in the tool​. 
    /// </summary>
    public class MultipleRotatorEditorWindow : EditorWindow
    {
        #region Variables
        private string _editorIdentifier;
        private float _editorTimeBeforeStoppingInSeconds;
        private bool _editorShouldReverseRotation;
        private RotationSettings _editorRotationsSettings;

        private bool _setId, _setTimeStop, _setReverseRot, _setObject, _setAngleRot, _setTimeRot;
        #endregion

        [MenuItem("Window/Custom/Rotators Mass Setter")]
        public static void ShowWindow() {
            GetWindow<MultipleRotatorEditorWindow>("Rotators Mass Setter");
        }

        private void OnGUI() {
            #region Rotators to edit
            GUILayout.BeginVertical("helpbox");

            GUILayout.Label("Rotators to edit");

            GUILayout.EndVertical();
            #endregion

            #region Editor
            GUILayout.BeginVertical("box");

            GUILayout.Label("EDITOR", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            _setId = EditorGUILayout.Toggle(_setId, GUILayout.MaxWidth(20));
            _editorIdentifier = EditorGUILayout.TextField("Identifier", _editorIdentifier);
            GUILayout.EndHorizontal();


            _editorTimeBeforeStoppingInSeconds = EditorGUILayout.FloatField("Time Before Stopping In Seconds", _editorTimeBeforeStoppingInSeconds);
            _editorShouldReverseRotation = EditorGUILayout.Toggle("Should Reverse Rotation", _editorShouldReverseRotation);

            _editorRotationsSettings.ObjectToRotate = (Transform)EditorGUILayout.ObjectField("Object To Rotate", _editorRotationsSettings.ObjectToRotate, typeof(Transform), true);
            _editorRotationsSettings.AngleRotation = EditorGUILayout.Vector3Field("Angle Rotation", _editorRotationsSettings.AngleRotation);
            _editorRotationsSettings.TimeToRotateInSeconds = EditorGUILayout.FloatField("Time To Rotate In Seconds", _editorRotationsSettings.TimeToRotateInSeconds);

            if (GUILayout.Button("Validate Changes")) {

            }
            GUILayout.EndVertical();
            #endregion

            #region Selected rotators
            GUILayout.BeginVertical("groupbox");

            GUILayout.Label("SELECTED ROTATORS", EditorStyles.boldLabel);

            GUILayout.EndVertical();
            #endregion
        }
    }
}