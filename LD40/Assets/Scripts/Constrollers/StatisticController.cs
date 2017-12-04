using Core.Movement;
using UnityEngine;

namespace Constrollers
{
    public class StatisticController : MonoBehaviour
    {
        public float StartTime;
        public float Duration;
        public float Distance;
        public int Enemies;
        public int WeightAdded;
        public int WeightRemoved;

        public static void AddEnemy()
        {
            var instance = FindObjectOfType<StatisticController>();
            if (instance != null)
                instance.OnEnemyWasDefeated();
        }

        private void OnEnable()
        {
            StartTime = Time.time;
            var weightlyObject = PlayerController.Instance.PLayerWeightyObject;
            weightlyObject.WeightWasChanged += OnWeightWasChanged;
            var movementObject = weightlyObject.GetComponent<BaseMovementObject>();
            movementObject.Moved += OnMoved;
        }

        private void OnDisable()
        {
            if (PlayerController.WasDestoyed) return;
            Duration = Time.time - StartTime;
            var weightlyObject = PlayerController.Instance.PLayerWeightyObject;
            weightlyObject.WeightWasChanged -= OnWeightWasChanged;
            var movementObject = weightlyObject.GetComponent<BaseMovementObject>();
            movementObject.Moved -= OnMoved;

            Debug.LogFormat("Duration={0}, Distance={1}, Enemies={2}, WeightAdded={3} WeightRemoved={4}",
                Duration, Distance, Enemies, WeightAdded, WeightRemoved);
        }

        private void OnWeightWasChanged(int current, int delta)
        {
            if (0 < delta)
                WeightAdded += delta;
            else
                WeightRemoved -= delta;
        }

        private void OnMoved(Vector3 current, Vector3 offset)
        {
            offset.y = 0.0f;
            Distance += offset.magnitude;
        }

        private void OnEnemyWasDefeated()
        {
            ++Enemies;
        }
    }
}