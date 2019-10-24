using UnityEngine;
using UnityEditor;
public class AnimatorsWindow : EditorWindow {
	[MenuItem("Korikas Avatar Plugin/Animator")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow<AnimatorsWindow>("KAPAnimator");
        window.minSize = new Vector2(265, 265);
    }
}