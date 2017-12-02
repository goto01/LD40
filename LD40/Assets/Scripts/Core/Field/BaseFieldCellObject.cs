using Assets.Scripts.Constrollers;
using UnityEngine;

namespace Assets.Scripts.Core.Field
{
    public class BaseFieldCellObject : MonoBehaviour
    {
        [Tooltip("Сколько осталось до обрушения")]
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private int currentHealth;

        protected virtual void Awake()
        {
            FieldCellsController.Instance.Register(this);
            
            currentHealth = maxHealth;
        }

        protected virtual void OnDestroy()
        {
            if (!FieldCellsController.WasDestoyed)
                FieldCellsController.Instance.Remove(this);
        }

        protected virtual void OnDisable()
        {
            if (!FieldCellsController.WasDestoyed)
                FieldCellsController.Instance.Remove(this);
        }

        public virtual void TestWeight()
        {
            if (currentHealth <= 0)
            {
                DestroyCell();
            }
        }

        private void DestroyCell()
        {
            // todo
            Debug.Log("DestroyCell");
        }
    }
}