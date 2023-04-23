using System.Collections.Generic;
using System.Reflection;
using Scripts.Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(TriggerObserver))]
    public class TriggerObserverEditor : UnityEditor.Editor
    {
        private TriggerObserver _triggerObserver;

        private void OnEnable()
        {
            _triggerObserver = (TriggerObserver)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            FieldInfo fieldInfo = typeof(TriggerObserver).GetField("_triggeredObjects", BindingFlags.Instance | BindingFlags.NonPublic);
            List<GameObject> triggeredObjects = (List<GameObject>)fieldInfo.GetValue(_triggerObserver);

            if (triggeredObjects == null) 
                return;
            
            GUILayout.Space(10f);
            GUILayout.Label("Triggered Objects:");
            foreach (GameObject obj in triggeredObjects)
            {
                EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
            }
        }
    }
}