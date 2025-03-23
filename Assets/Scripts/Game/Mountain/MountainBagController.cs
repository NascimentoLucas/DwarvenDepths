using Nascimento.Model;
using System.Collections.Generic;

namespace Nascimento.Game.Mountain
{
    public class MountainBagController : ICraftHandler
    {
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
            }
        }

        public bool HasItem(ItemSO item, int amount)
        {
            lock (_lock)
            {
                if (_items.ContainsKey(item) && _items[item] >= amount)
                {
                    _items[item] -= amount;
                    return true;
                }
                return false;
            }
        }
    }
}
