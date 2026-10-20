using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Animals.Core;
using ZooWorld.Application;
using ZooWorld.Infrastructure;
using ZooWorld.UI;

namespace ZooWorld.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraService _cameraService;

        [SerializeField]
        private SpeechBubble _speechBubblePrefab;

        [SerializeField]
        private SpeechBubblesLayer _speechBubblesLayer;

        [SerializeField]
        private int _speechBubblesPoolSize;

        [SerializeField]
        private Transform _animalsContainer;

        [SerializeField]
        private List<AnimalDefinitionSO> _animals;

        public override void InstallBindings()
        {
            foreach (var definition in _animals)
            {
                Container
                    .BindMemoryPool<Animal, Animal.Pool>()
                    .WithInitialSize(definition.InitialPoolSize)
                    .WithFactoryArguments(definition)
                    .FromComponentInNewPrefab(definition.AnimalPrefab)
                    .UnderTransform(_animalsContainer);
            }

            Container.Bind<AnimalPoolsRegistry>().AsSingle().NonLazy();
            Container.Bind<StatisticsService>().AsSingle().NonLazy();
            Container.Bind<SpeechBubbleService>().AsSingle().NonLazy();
            Container.BindInstance(_cameraService);
            Container.BindInstance(_speechBubblesLayer);

            Container
                .BindMemoryPool<SpeechBubble, SpeechBubble.Pool>()
                .WithInitialSize(_speechBubblesPoolSize)
                .FromComponentInNewPrefab(_speechBubblePrefab)
                .UnderTransform(_speechBubblesLayer.Root);
        }
    }
}
