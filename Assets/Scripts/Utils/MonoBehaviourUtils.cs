using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Utils
{
    public static class MonoBehaviourUtils
    {
        public static Coroutine Invoke(this MonoBehaviour obj, Action action, float delay)
        {
            IEnumerator DoWithDelayCoroutine()
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }
            
            return obj.StartCoroutine(DoWithDelayCoroutine());
        }
    }
}