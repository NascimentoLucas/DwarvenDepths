using Nascimento.Game.Mountain;
using Nascimento.Model;
using Nascimento.View;
using UnityEngine;

namespace Nascimento.Game
{
    public class GameLoopController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private MountainController _mountainController;
        [SerializeField]
        private ItemView _itemView;
        [SerializeField]
        private TimerImage _timerImage;


        [Header("Design.Time")]
        [SerializeField]
        private int _minGoal = 1;
        [SerializeField]
        private int _maxGoal = 20;

        [Header("Design.Time")]
        [SerializeField]
        private float _firstTimeToWait = 30f;
        [SerializeField]
        private float _timeToWaitPerAmount = 1;
        private float _timeToWait;
        private float _timer;
        private int _goal;

        private void Start()
        {
            _timer = _firstTimeToWait;
            _timeToWait = _timer;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = _timeToWait;
                CheckScore();
            }
            _timerImage.SetTimer(_timer / _timeToWait);
        }

        private void CheckScore()
        {
            ItemSO item = _mountainController.GetRandomItem();
            _goal = UnityEngine.Random.Range(_minGoal, _maxGoal);
            _itemView.Setup(item.Icon, $"Objetivo: {_goal.ToString("00")}");

            float difficulty = 1;

            for (int i = 0; i < item.Components.Length; i++)
            {
                difficulty *= item.Components[i].Amount;
            }

            _timeToWait = (_timeToWaitPerAmount * _goal) * difficulty;
        }

    }
}