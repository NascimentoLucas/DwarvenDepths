using Nascimento.Game.Mountain;
using Nascimento.Model;
using Nascimento.View;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using Nascimento.Game.Minion;

namespace Nascimento.Game
{
    public class GameLoopController : MonoBehaviour
    {
        [Header("Setup.Ref")]
        [SerializeField]
        private MountainController _mountainController;
        [SerializeField]
        private MountainBagController _bagController;
        [SerializeField]
        private BagPanel _itemPanel;
        [SerializeField]
        private MinionControllerPool _minionControllerPool;

        [Header("Setup.UI")]
        [SerializeField]
        private ItemView _itemView;
        [SerializeField]
        private TimerImage _timerImage;
        [SerializeField]
        private TextMeshProUGUI _scoreText;
        [SerializeField]
        private TextMeshProUGUI _minionPoolText;

        [Header("Game Design.Goal")]
        [SerializeField]
        private int _minGoal = 1;
        [SerializeField]
        private int _maxGoal = 20;

        [Header("Design.Time")]
        [SerializeField]
        private float _firstTurnWaitTime = 5;
        [SerializeField]
        private float _waitTimePerItemAmount = 1;

        private float _timer;
        private float _waitTime;
        private ItemSO _lastItem;
        private int _goal;
        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                _scoreText.text = _score.ToString("000");
            }
        }

        private void Start()
        {
            _timer = _firstTurnWaitTime;
            _waitTime = _timer;
            Score = 0;
        }

        private void FixedUpdate()
        {
            _minionPoolText.text = $"{_minionControllerPool.GetPoolSize()}";
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            _timerImage.SetTimer(_timer / _waitTime);
            if (_timer <= 0)
            {
                if (_lastItem != null)
                {
                    if (_bagController.HasItem(_lastItem, _goal))
                    {
                        Score += _goal;
                        _minionControllerPool.IncreasePoolSize(transform);
                    }
                }

                SetNextItem();
                _timer = _waitTime;
            }
        }

        private void SetNextItem()
        {
            _lastItem = _mountainController.GetRandomItem();
            _goal = UnityEngine.Random.Range(_minGoal, _maxGoal);
            _itemView.Setup(_lastItem.Icon, $"Objetivo: {_goal.ToString("00")}");

            float difficulty = 1;

            for (int i = 0; i < _lastItem.Components.Length; i++)
            {
                difficulty *= _lastItem.Components[i].Amount;
            }

            _waitTime = (_waitTimePerItemAmount * _goal) * difficulty;
            _itemPanel.SetAsFirstItem(_lastItem);
        }

#if UNITY_EDITOR
        [Button]
        public void FinishTime()
        {
            _timer = 1;
        }
#endif

    }
}