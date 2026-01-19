using UnityEngine;
using UnityEngine.AI;

namespace ArmyClashLike.Gameplay
{
    public class UnitContainer : MonoBehaviour, IUnitContainer, IColorModifiable, ISizeModifiable
    {
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        [field: SerializeField] public Modification[] FormModifications { get; private set; }
        
        public Vector3 Position => transform.position;
        public Rigidbody Rigidbody => rigidbody;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public float Size => renderer.bounds.size.x;

        public void PlayDead()
        {
            gameObject.SetActive(false);
        }
        
        public void Modify(ColorModification modification)
        {
            renderer.material.color = modification.color;
        }

        public void Modify(SizeModification modification)
        {
            transform.localScale = new Vector3(modification.sizeChange, modification.sizeChange, modification.sizeChange);
        }
    }
}