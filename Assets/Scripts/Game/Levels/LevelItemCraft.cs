using Nascimento.Model;
using UnityEngine;

namespace Nascimento.Game.Level.Controller
{
    public class LevelItemCraft
    {
        public class CraftItem
        {
            public readonly ItemSO Item;
            public readonly int AmountNeed;
            public int AmountHave;

            public CraftItem(ItemSO item, int amountNeed)
            {
                Item = item;
                AmountNeed = amountNeed;
            }
        }

        private ItemSO _item;
        private CraftItem[] _craftItems;

        private uint _steps;

        public LevelItemCraft(ItemSO item)
        {
            _item = item;
            _craftItems = new CraftItem[item.Components.Length];
            for (int i = 0; i < item.Components.Length; i++)
            {
                _craftItems[i] = new CraftItem(item.Components[i].Item, item.Components[i].Amount);
            }
        }

        public void GetItem(ICraftHandler handler)
        {
            if (_item.Log)
            {
                Debug.Log($"Crafting {_item.Name} - Step {_steps}");
            }

            for (int i = 0; i < _craftItems.Length; i++)
            {
                if (_craftItems[i].AmountHave < _craftItems[i].AmountNeed)
                {
                    var r = handler.GetItem(_craftItems[i].Item,
                    _craftItems[i].AmountNeed - _craftItems[i].AmountHave);
                    if (r)
                    {
                        _craftItems[i].AmountHave = _craftItems[i].AmountNeed;
                    }
                    else
                    {
                        if (_item.Log)
                        {
                            Debug.Log($"Failed to craft {_item.Name} - doest have {i}");
                        }
                        _steps = 0;
                        return;
                    }

                }
            }

            _steps++;
            if (_item.Log)
            {
                Debug.Log($"Crafted {_item.Name} - Step {_steps}");
            }
            if (_steps >= _item.Steps)
            {
                _steps = 0;
                for (int i = 0; i < _craftItems.Length; i++)
                {
                    _craftItems[i].AmountHave = 0;
                }
                handler.AddItem(_item, 1);
                if (_item.Log)
                {
                    Debug.Log($"Crafted {_item.Name} - Step {_steps}");
                }
            }
        }
    }
}