using UnityEngine;

namespace Core.Field
{
    [CreateAssetMenu(fileName = "FieldCell Settings", menuName = "Data/Field Cell")]
    public class FieldCellSettings : ScriptableObject
    {
        public float radius = 0.5f;
        [Header("What weight can be sustained?")]
        public int maxWeight = 5;
        [Header("Falling")]
        public float delayBeforeFalling = 1.0f;
        public float fallingAcceleration = -20.0f;
        public float minYPosition = -30.0f;
        [Header("Bouncing")]
        public float weightToPositionRation = 1.0f;
        public float offsetToAcceleration = 1.0f;
        public float springRate = 1.0f;
    }
}