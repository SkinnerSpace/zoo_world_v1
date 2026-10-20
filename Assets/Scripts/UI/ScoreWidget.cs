using System;
using R3;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using ZooWorld.Application;

namespace ZooWorld.UI
{
    public class ScoreWidget : MonoBehaviour
    {
        public UnityEvent<int> OnDeadPreyCountChanged;
        public UnityEvent<int> OnDeadPredatorsCountChanged;

        private StatisticsService _statisticsService;

        private IDisposable _deadPreyCountSubscription;
        private IDisposable _deadPredatorsCountSubscription;

        [Inject]
        private void Construct(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        private void OnEnable()
        {
            _deadPreyCountSubscription = _statisticsService.DeadPreyCount.Subscribe(count =>
            {
                OnDeadPreyCountChanged.Invoke(count);
            });

            _deadPredatorsCountSubscription = _statisticsService.DeadPredatorsCount.Subscribe(count =>
            {
                OnDeadPredatorsCountChanged.Invoke(count);
            });
        }

        private void OnDisable()
        {
            _deadPreyCountSubscription?.Dispose();
            _deadPredatorsCountSubscription?.Dispose();
        }
    }
}
