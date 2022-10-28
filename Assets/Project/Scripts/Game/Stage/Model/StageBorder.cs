using UnityEngine;

namespace GamaPlatform
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class StageBorder : MonoBehaviour
    {
        public void Setup(Border borderParams, Transform parent)
        {
            base.name = borderParams.Name;
            base.gameObject.layer = (int)Mathf.Log(borderParams.Layer.value, 2);;

            Transform colliderTransform = base.transform;
            colliderTransform.SetParent(parent, false);
            colliderTransform.localPosition = borderParams.Position;
            colliderTransform.localScale = borderParams.Scale;
        }
    }
}