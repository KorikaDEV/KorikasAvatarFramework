using System;
using UnityEngine;
using UnityEditor;
using VRCSDK2;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

namespace KAPStuff
{
    public class KAPProfile{
    public string name;
    public int polys;
    public int boneamount;
    public int dynboneamount;
    public int dynbonecolliders;
    public int meshrenderers;
    public int particle_systems;
    public int cloth;
    public int animators;
    public int lights;
    public int audio_sources;
	public PerformanceProfile perfP;

    public KAPProfile(GameObject obj){
        initProfile(obj);
    }
    public void delete(){
        FileUtil.DeleteFileOrDirectory("Assets/KAPAvatars/" + name);
        AssetDatabase.Refresh();
    }
    public void saveFile(){
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/KAPAvatars/" + name + "/" + name + ".KAPprofile", json);
    }
    public static KAPProfile fromFile(string name){
        string json = File.ReadAllText(Application.dataPath + "/KAPAvatars/" + name + "/" + name + ".KAPprofile");
        KAPProfile result = JsonUtility.FromJson<KAPProfile>(json);
		result.perfP = PerformanceProfile.fromFile(Application.dataPath + "/KAPAvatars/" + name + "/");
		return result;
    }
    public void openScene(){
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/KAPAvatars/" + name + "/" + name + ".unity");
    }
    public void openFolder(){
        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath("Assets/KAPAvatars/" + name + "/" + name + ".unity", typeof(UnityEngine.Object));
        Selection.activeObject = obj;
        EditorGUIUtility.PingObject(obj);
    }
    public void initProfile(GameObject obj){
        this.name = obj.name;
        this.polys = 0;
        this.boneamount = 0;
        this.dynboneamount = 0;
        SkinnedMeshRenderer[] meshes = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer smr in meshes){
            this.polys = this.polys + (smr.sharedMesh.triangles.Length/3);
            foreach(Transform bone in smr.bones){
                this.boneamount = this.boneamount + 1;
                DynamicBone dyn = bone.gameObject.GetComponent<DynamicBone>();
                if(dyn){
                    this.dynboneamount = this.dynboneamount + dyn.m_Root.GetComponentsInChildren<Transform>().Length;
                }
            }
        }
        this.meshrenderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>().Length;
        this.particle_systems = obj.GetComponentsInChildren<ParticleSystem>().Length;
        this.dynbonecolliders = obj.GetComponentsInChildren<DynamicBoneCollider>().Length;
        this.cloth = obj.GetComponentsInChildren<Cloth>().Length;
        this.animators = obj.GetComponentsInChildren<Animator>().Length - 1;
        this.audio_sources = obj.GetComponentsInChildren<AudioSource>().Length;
        this.lights = obj.GetComponentsInChildren<Light>().Length;
		
		this.perfP = new PerformanceProfile(this);
		this.perfP.saveFile(Application.dataPath + "/KAPAvatars/" + name + "/");
		saveFile();
    }

    public static KAPProfile[] getAllInProject(){
        string path = "Assets/KAPAvatars/";
        List<string> files = new List<string>();
        List<KAPProfile> result = new List<KAPProfile>();
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles();
        foreach (FileInfo f in fileInfo)
        {
            if (f.ToString().Contains(".meta"))
            {
                string fname = f.ToString().Split(new string[] { "KAPAvatars\\" }, StringSplitOptions.None)[1].Replace(".meta", "");
                if(AssetDatabase.IsValidFolder(path + fname) && File.Exists(f.ToString().Replace(".meta", "") + "\\" + fname + ".KAPprofile")){
                    files.Add(fname);
                }
            }
        }
        foreach (string foldername in files)
        {
            result.Add(fromFile(foldername));
        }
        return result.ToArray();
    }
}
}