 
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
 
namespace UnityEditor.UI
{
    [CustomEditor(typeof(GestureBuilder))]
    public class GestureBuilderEditor : Editor {

        public string[] options = new string[] {"fingerpoint", "fist", "handgun", "handopen", "rocknroll", "thumbsup", "victory"};
        public int index = 0;

        bool buildButton = false;
        bool addTaskButton = false;

        GestureBuilder ex;

        private void OnEnable() {
            ex = (GestureBuilder) target;
        }
   
        public override void OnInspectorGUI()
        {
            index = EditorGUILayout.Popup(index, options);

            this.serializedObject.Update();
            SerializedProperty sp = this.serializedObject.FindProperty("onGestureBuild");
            EditorGUILayout.PropertyField(sp, true);
            this.serializedObject.ApplyModifiedProperties();

            EditorGUILayout.BeginHorizontal();
            addTaskButton = GUILayout.Button("add", GUILayout.Width(50));
            buildButton = GUILayout.Button("build");
            EditorGUILayout.EndHorizontal();
            
            if(addTaskButton){
                ex.addTask();
            }
            if(buildButton){
                ex.onGestureBuild.Invoke();
            }
        }
    }
}
