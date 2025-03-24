using Nascimento.Game.Mountain;
using Nascimento.Model;
using Nascimento.View;
using TMPro;
using UnityEngine;

namespace Nascimento.Game
{
    public class GameLoopController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private MountainController _mountainController;
        [SerializeField]
        private MountainBagController _bagController;
        [SerializeField]
        private ItemView _itemView;
        [SerializeField]
        private TimerImage _timerImage;
        [SerializeField]
        private TextMeshProUGUI _scoreText;

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


        [Header("Debug")]
        [SerializeField]
        private float _timer;
        [SerializeField]
        private float _waitTime;
        [SerializeField]
        private ItemSO _lastItem;
        [SerializeField]
        private int _goal;
        [SerializeField]
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
        }

    }
}