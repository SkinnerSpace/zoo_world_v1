using UnityEngine;
using UnityEngine.Localization.Components;

namespace ZooWorld.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField]
        private LocalizeStringEvent _localizeStringEvent;

        public void UpdateContent(int number)
        {
            _localizeStringEvent.StringReference.Arguments = new object[] { number };

            _localizeStringEvent.RefreshString();
        }
    }
}
