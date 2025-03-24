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
        private bool _isBasicResource = false;

        public LevelItemCraft(ItemSO item)
        {
            _item = item;
            _craftItems = new CraftItem[item.Components.Length];
            for (int i = 0; i < item.Components.Length; i++)
            {
                _craftItems[i] = new CraftItem(item.Components[i].Item, item.Components[i].Amount);
            }
            _isBasicResource = item.Components.Length < 1;
        }

        public void GetItem(ICraftHandler handler, int minionsRatio)
        {
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
                        _steps = 0;
                        return;
                    }

                }
            }

            _steps += (uint)(1 * minionsRatio);
            if (_steps >= _item.Steps)
            {
                _steps = 0;
                for (int i = 0; i < _craftItems.Length; i++)
                {
                    _craftItems[i].AmountHave = 0;
                }
                if (_isBasicResource)
                {
                    handler.AddItem(_item, 1 * minionsRatio);
                }
                else
                {
                    handler.AddItem(_item, 1);
                }
            }
        }
    }
}