using Nascimento.Model;

namespace Nascimento.Game
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
            for (int i = 0; i < _craftItems.Length; i++)
            {
                if (_craftItems[i].AmountHave < _craftItems[i].AmountNeed)
                {
                    var r = handler.GetItem(_craftItems[i].Item,
                    _craftItems[i].AmountNeed - _craftItems[i].AmountHave);
                    if (!r)
                        return;
                }
            }
            handler.AddItem(_item, 1);
        }
    }
}