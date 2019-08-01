using System;
using UnityEngine;
using VRCSDK2;
namespace KatStuff
{
    public class KatProfile{
    public string name;
    public int polys;
    public int boneamount;
    public int dynboneamount;
    public KatProfile(GameObject obj){
        this.name = obj.name;
        this.polys = 0;
        this.boneamount = 0;
        this.dynboneamount = 0;
        SkinnedMeshRenderer[] meshes = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer smr in meshes){
            this.polys = this.polys + smr.sharedMesh.triangles.Length/3;
            foreach(Transform bone in smr.bones){
                this.boneamount = this.boneamount + 1;
                if(bone.gameObject.GetComponent<DynamicBone>()){
                    this.dynboneamount = this.dynboneamount + 1;
                }
            }
        }
    }
    public void makeFile(){
        string json = JsonUtility.ToJson(this);
        Debug.Log(json);
    }
}
}