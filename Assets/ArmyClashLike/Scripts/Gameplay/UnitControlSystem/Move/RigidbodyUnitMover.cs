using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public class RigidbodyUnitMover : IUnitMover
    {
        public void Initialize(UnitSet unitSet)
        {
            
        }

        public void Move(Unit unit, Vector3 target, float deltaTime)
        {
            var rb = unit.container.Rigidbody;
            var unitPosition = unit.container.Position;
            
            var direction = target - unitPosition;
            direction.y = 0f; 
            
            var distance = direction.magnitude;

            if (distance < 10f)
            {
                return;
            }

            direction.Normalize();
            
            var speed = unit.stats.speed;
            var desiredVelocity = direction * speed;
            var currentVelocity = rb.linearVelocity;

            var force = (desiredVelocity - currentVelocity) / deltaTime;
            
            var maxForce = speed * 10f;
            if (force.magnitude > maxForce)
            {
                force = force.normalized * maxForce;
            }
            
            rb.AddForce(force, ForceMode.Force);
            currentVelocity = rb.linearVelocity;
            
            var horizontalVel = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
            if (horizontalVel.magnitude > speed)
            {
                var limitedVel = horizontalVel.normalized * speed;
                rb.linearVelocity = new Vector3(limitedVel.x, 0, limitedVel.z);
            }
        }
    }
}