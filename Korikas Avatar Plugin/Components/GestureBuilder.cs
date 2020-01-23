using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
 
namespace UnityEngine.UI
{
    public class GestureBuilder : MonoBehaviour {
   
        [Serializable]
        public class GestureEvent : UnityEvent {}
   
        public GestureEvent onGestureBuild = new GestureEvent();

        public void gb_createAnimatedClone(AnimationClip a){
            Debug.Log("created animation clone");
        }

        public void gb_hideEverything(){
            Debug.Log("hided everything");
        }

        public void gb_showGameObject(GameObject g){
            Debug.Log("showed a gameobject");
        }

        public void addTask(){
            UnityAction methodDelegate = System.Delegate.CreateDelegate (typeof(UnityAction), this, "gb_hideEverything") as UnityAction;
            UnityEditor.Events.UnityEventTools.AddPersistentListener (onGestureBuild, methodDelegate);

            for (var i = 0; i < onGestureBuild.GetPersistentEventCount(); i++)
            {
                onGestureBuild.SetPersistentListenerState(i, UnityEventCallState.EditorAndRuntime);
            }
        }
    }
}