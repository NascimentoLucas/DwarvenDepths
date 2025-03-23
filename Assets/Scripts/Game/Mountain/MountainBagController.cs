using Nascimento.Model;
using System.Collections.Generic;
using UnityEngine;
using Nascimento.View;
using Nascimento.Game.Level.Controller;


namespace Nascimento.Game.Mountain
{
    public class MountainBagController : MonoBehaviour, ICraftHandler
    {
        [Header("Setup")]
        [SerializeField] private ItemPanel _itemPanel;

        private readonly Dictionary<ItemSO, int> _items = new Dictionary<ItemSO, int>();
        private readonly object _lock = new object();

        public void AddItem(ItemSO item, int amount)
        {
            lock (_lock)
            {
                if (_items.ContainsKey(item))
                {
                    _items[item] += amount;
                }
                else
                {
                    _items[item] = amount;
                }

                _itemPanel.Setup(item, _items[item]);
            }
        }

        public bool GetItem(ItemSO item, int amount)
        {
            lock (_lock)
            {
                if (_items.ContainsKey(item))
                {
                    if (_items[item] >= amount)
                    {
                        _items[item] -= amount;
                        _itemPanel.Setup(item, _items[item]);
                        return true;
                    }
                    else
                    {
                        _itemPanel.Setup(item, _items[item]);
                    }
                }
                return false;
            }
        }
    }
}
