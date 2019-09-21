using UnityEngine;
using System.Collections.Generic;

public class BoneHelper : MonoBehaviour {
    
    public static DynamicBone[] getAllDynBones(Transform avatar){
        List<DynamicBone> result = new List<DynamicBone>();

        int count = avatar.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform t = avatar.GetChild(i);
            if(t.gameObject.GetComponent<DynamicBone>()){
                result.Add(t.gameObject.GetComponent<DynamicBone>());
            }
        }

        return result.ToArray();
    }
}