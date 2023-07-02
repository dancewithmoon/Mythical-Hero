using System.Reflection;
using Scripts.Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CharacterDeath))]
    public class CharacterDeathEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Die"))
            {
                MethodInfo dieMethod = typeof(CharacterDeath).GetMethod("Die", BindingFlags.Instance | BindingFlags.NonPublic);
                dieMethod.Invoke(target, null);
            }
        }
    }
}