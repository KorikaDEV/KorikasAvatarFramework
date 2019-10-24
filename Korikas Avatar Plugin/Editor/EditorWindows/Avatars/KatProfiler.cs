using UnityEngine;
using UnityEngine.SceneManagement;
using KAPStuff;

[UnityEditor.InitializeOnLoad]
static class KAPProfiler
{
    static KAPProfiler()
    {
        UnityEditor.SceneManagement.EditorSceneManager.sceneSaving += OnSceneSaved;
    }

    static void OnSceneSaved(Scene scene, string s)
    {
        if(GestureDisplay.getVRCSceneAvatar() != null){
            KAPProfile kp = new KAPProfile(GestureDisplay.getVRCSceneAvatar());
            kp.saveFile();
        }else{
            Debug.LogWarning("your avatar is hidden! you might make him visible again, so that KAP can save his performance statistics...");
        }
    }
}
