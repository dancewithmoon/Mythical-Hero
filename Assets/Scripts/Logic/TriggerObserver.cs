using System;
using System.Collections.Generic;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        private readonly List<GameObject> _triggeredObjects = new List<GameObject>();
        
        [SerializeField] private LayerMask _layerMask;

        public IReadOnlyList<GameObject> TriggeredObjects => _triggeredObjects;
        
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.HasLayer(_layerMask) == false) 
                return;
            
            if(_triggeredObjects.Contains(other.gameObject))
                return;
                
            _triggeredObjects.Add(other.gameObject);
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.HasLayer(_layerMask) == false) 
                return;
            
            if(_triggeredObjects.Contains(other.gameObject) == false)
                return;

            _triggeredObjects.Remove(other.gameObject);
            TriggerExit?.Invoke(other);
        }
    }
}
