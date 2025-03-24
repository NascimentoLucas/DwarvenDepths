using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nascimento.View
{

    public class ItemView : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _text;

        public void Setup(Sprite icon, string text)
        {
            _icon.sprite = icon;
            _text.text = text;
        }

        internal void UpdateText(string text)
        {
            _text.text = text;
            _text.ForceMeshUpdate();
        }
    }
}
