using UnityEngine;

namespace Utils
{
    public class FollowPoint : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            if(!target) return;
            
            transform.position = target.position;
        }
    }
}