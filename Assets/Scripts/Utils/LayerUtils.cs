using UnityEngine;

namespace Scripts.Utils
{
    public static class LayerUtils
    {
        public static bool HasLayer(this GameObject gameObject, LayerMask layerMask) => 
            layerMask == (layerMask | (1 << gameObject.layer));
    }
}