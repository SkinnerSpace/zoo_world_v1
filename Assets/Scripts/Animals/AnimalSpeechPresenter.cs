using System;
using R3;
using UnityEngine;
using Zenject;
using ZooWorld.Animals.Core;
using ZooWorld.Application;

namespace ZooWorld.Animals
{
    public class AnimalSpeechPresenter : MonoBehaviour
    {
        [SerializeField]
        private Animal _animal;

        [SerializeField]
        private Transform _anchorPoint;

        [SerializeField]
        private float _speechDurationSeconds;

        private SpeechBubbleService _bubbleService;
        private IDisposable _subscription;
        private IDisposable _activeBubble;

        [Inject]
        private void Construct(SpeechBubbleService bubbleService)
        {
            _bubbleService = bubbleService;
        }

        private void Say(SpeechCues speechCue)
        {
            _activeBubble?.Dispose();

            _activeBubble = _bubbleService.Show(_anchorPoint, speechCue, _speechDurationSeconds);
        }

        private void OnEnable()
        {
            _subscription = _animal.OnAte.Subscribe(_ => Say(SpeechCues.Tasty));
        }

        private void OnDisable()
        {
            _subscription?.Dispose();
            _activeBubble?.Dispose();

            _subscription = null;
            _activeBubble = null;
        }
    }
}
