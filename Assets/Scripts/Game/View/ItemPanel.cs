using Nascimento.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nascimento.View
{
    public class ItemPanel : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private ItemView _prefab;
        [SerializeField]
        private LayoutGroup _content;

        private readonly Dictionary<ItemSO, ItemView> _items = new Dictionary<ItemSO, ItemView>();

        public void Setup(ItemSO item, int amount)
        {
            if (!_items.ContainsKey(item))
            {
                var itemView = Instantiate(_prefab, _content.transform);
                itemView.Setup(item.Icon, amount.ToString("00"));
                _items[item] = itemView;
            }
            else
            {
                _items[item].UpdateText(amount.ToString("00"));
            }
        }
    }
}
