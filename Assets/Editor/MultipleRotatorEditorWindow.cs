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
        private string _editorIdentifier;                   //Store variables of the fields
        public float _editorTimeBeforeStoppingInSeconds;
        private bool _editorShouldReverseRotation;
        private RotationSettings _editorRotationsSettings;

        private bool _setId, _setTimeStop, _setReverseRot, _setObjectToRotate, _setAngleRot, _setTimeRot;   //Checkboxes

        [SerializeField]
        private Rotator[] _rotatorsToEdit;  //selected rotators

        private Vector2 _scrollPosition;
        #endregion

        //Open editor window in the Window menu
        [MenuItem("Window/Custom/Rotators Mass Setter")]
        public static void ShowWindow() {
            GetWindow<MultipleRotatorEditorWindow>("Rotators Mass Setter");
        }

        private void OnGUI() {
            #region Rotators to edit
            GUILayout.BeginVertical("helpbox");
            
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty rotatorsProperty = so.FindProperty("_rotatorsToEdit");

            EditorGUILayout.PropertyField(rotatorsProperty, true); // True to show children
            so.ApplyModifiedProperties(); // Apply modified properties

            GUILayout.EndVertical();
            #endregion

            #region Editor
            GUILayout.BeginVertical("box");
            GUILayout.Label("EDITOR", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            _setId = EditorGUILayout.Toggle(_setId, GUILayout.MaxWidth(20));
            _editorIdentifier = EditorGUILayout.TextField("Identifier", _editorIdentifier);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _setTimeStop = EditorGUILayout.Toggle(_setTimeStop, GUILayout.MaxWidth(20));
            _editorTimeBeforeStoppingInSeconds = EditorGUILayout.FloatField("Time Before Stopping In Seconds", _editorTimeBeforeStoppingInSeconds);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _setReverseRot = EditorGUILayout.Toggle(_setReverseRot, GUILayout.MaxWidth(20));
            _editorShouldReverseRotation = EditorGUILayout.Toggle("Should Reverse Rotation", _editorShouldReverseRotation);
            GUILayout.EndHorizontal();

            GUILayout.Space(10f);
            GUILayout.Label("Rotations Settings");

            GUILayout.BeginHorizontal();
            _setObjectToRotate = EditorGUILayout.Toggle(_setObjectToRotate, GUILayout.MaxWidth(20));
            _editorRotationsSettings.ObjectToRotate = (Transform)EditorGUILayout.ObjectField("Object To Rotate", _editorRotationsSettings.ObjectToRotate, typeof(Transform), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _setAngleRot = EditorGUILayout.Toggle(_setAngleRot, GUILayout.MaxWidth(20));
            _editorRotationsSettings.AngleRotation = EditorGUILayout.Vector3Field("Angle Rotation", _editorRotationsSettings.AngleRotation);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _setTimeRot = EditorGUILayout.Toggle(_setTimeRot, GUILayout.MaxWidth(20));
            _editorRotationsSettings.TimeToRotateInSeconds = EditorGUILayout.FloatField("Time To Rotate In Seconds", _editorRotationsSettings.TimeToRotateInSeconds);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Validate Changes")) {
                ValidateChanges();
            }

            GUILayout.EndVertical();
            #endregion

            #region Selected rotators
            GUILayout.BeginVertical("groupbox");
            GUILayout.Label("SELECTED ROTATORS", EditorStyles.boldLabel);

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DisplaySelectedRotators();
            GUILayout.EndScrollView();

            GUILayout.EndVertical();
            #endregion
        }

        //Set the parameters of the selected Rotators from the Editor
        private void ValidateChanges() {
            foreach (Rotator rotator in _rotatorsToEdit) {
                UpdateRotatorInformations(rotator);
            }
        }

        private void UpdateRotatorInformations(Rotator rotator) {
            SerializedObject so = new SerializedObject(rotator);

            if (_setId) {
                SerializedProperty id = so.FindProperty("_identifier");
                id.stringValue = _editorIdentifier;
            }
            if (_setTimeStop) {
                SerializedProperty timeStop = so.FindProperty("_timeBeforeStoppingInSeconds");
                timeStop.floatValue = _editorTimeBeforeStoppingInSeconds;
            }
            if (_setReverseRot) {
                SerializedProperty reverseRot = so.FindProperty("_shouldReverseRotation");
                reverseRot.boolValue = _editorShouldReverseRotation;
            }

            SerializedProperty rotationsSettings = so.FindProperty("_rotationsSettings");
            if (_setObjectToRotate) {
                SerializedProperty objectToRotate = rotationsSettings.FindPropertyRelative("ObjectToRotate");
                objectToRotate.objectReferenceValue = _editorRotationsSettings.ObjectToRotate;
            }
            if (_setAngleRot) {
                SerializedProperty angleRot = rotationsSettings.FindPropertyRelative("AngleRotation");
                angleRot.vector3Value = _editorRotationsSettings.AngleRotation;
            }
            if (_setTimeRot) {
                SerializedProperty timeRot = rotationsSettings.FindPropertyRelative("TimeToRotateInSeconds");
                timeRot.floatValue = _editorRotationsSettings.TimeToRotateInSeconds;
            }

            so.ApplyModifiedProperties();
        }

        private void DisplaySelectedRotators() {
            if (_rotatorsToEdit != null) {
                foreach (Rotator rotator in _rotatorsToEdit) {  //in playmode "rotator" becomes null for some reason so the rotators are not displayed
                    if (rotator != null) {
                        Editor rotatorEditor = Editor.CreateEditor(rotator);

                        GUILayout.BeginVertical("box");
                        GUILayout.Label(rotator.ToString());
                        GUILayout.Space(10f);
                        rotatorEditor.DrawDefaultInspector();
                        GUILayout.EndVertical();
                    }
                }
            }
        }
    }
}