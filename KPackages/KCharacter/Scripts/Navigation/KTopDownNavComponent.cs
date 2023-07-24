using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#nullable enable

namespace SDKabu.KCharacter.Navigation
{
    [Serializable]
    public struct NavigationData
    {
        public float speed;
        public float acceleration;
        public float maxSpeed;
    }
    
    public class KTopDownNavComponent : MonoBehaviour
    {

        private const float REACH_EPSILON = 0.1f;

        [SerializeField] private NavigationData navigationData;
        private Queue<Vector3> queuedPositions = new Queue<Vector3>();
        private Vector3? currentTargetPosition;
        private Transform characterTransform = null!;

        private bool HasReachTarget 
        {
            get
            {
                if (!currentTargetPosition.HasValue) return true;
                return (currentTargetPosition.Value - characterTransform.position).sqrMagnitude < REACH_EPSILON;   
            }   
        }

        private void Awake()
        {
            characterTransform = transform;
        }

        private void Update()
        {
            if(currentTargetPosition.HasValue && HasReachTarget)
            {
                if(queuedPositions.Count > 0)
                {
                    currentTargetPosition = queuedPositions.Dequeue();
                }
                else
                {
                    currentTargetPosition = null;
                }
            }

            if (currentTargetPosition.HasValue)
            {
                PerformMove(currentTargetPosition.Value, Time.deltaTime);
            }
        }

        private void PerformMove(Vector3 _pos, float _delta)
        {
            var direction = (_pos - characterTransform.position).normalized;
            characterTransform.Translate(navigationData.speed * _delta * direction);
        }

        public void MoveTo(Vector3 _pos, bool _overrideTarget = true)
        {
            if (_overrideTarget)
            {
                queuedPositions.Clear();
                currentTargetPosition = _pos;
                return;
            }
            queuedPositions.Enqueue(_pos);
        }
        
        
        
        
        
    }
}