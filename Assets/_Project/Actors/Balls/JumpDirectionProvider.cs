using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actors.Balls
{
    public class JumpDirectionProvider
    {
        private Vector3[] _contacts;
        
        public Vector3 GetJumpDirection(Vector3 jumpingObjectPosition)
        {
            var jumpDirection = _contacts
                .Aggregate(Vector3.zero,
                    (current, point) => current + (jumpingObjectPosition - point) // Add up all directions
                );
            
            return jumpDirection.normalized;
        }

        public void SetCollisionContacts(Collision collision)
        {
            _contacts = collision
                .contacts
                .Select(x => x.point)
                .ToArray();
        }
        
        private void ClearCollisions()
        {
            _contacts = new Vector3[0];
        }
    }
}