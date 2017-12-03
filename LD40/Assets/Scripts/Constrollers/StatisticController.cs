﻿using Core.Movement;
using UnityEngine;

namespace Constrollers
{
    public class StatisticController : MonoBehaviour
    {
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

        private void Start()
        {
            var weightlyObject = PlayerController.Instance.PLayerWeightyObject;
            weightlyObject.WeightWasChanged += OnWeightWasChanged;
            var movementObject = weightlyObject.GetComponent<BaseMovementObject>();
            movementObject.Moved += OnMoved;
        }

        private void OnDisable()
        {
            var weightlyObject = PlayerController.Instance.PLayerWeightyObject;
            weightlyObject.WeightWasChanged -= OnWeightWasChanged;
            var movementObject = weightlyObject.GetComponent<BaseMovementObject>();
            movementObject.Moved -= OnMoved;
        
            Debug.LogFormat("{0}, {1}, {2}, {3}", Distance, Enemies, WeightAdded, WeightRemoved);
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