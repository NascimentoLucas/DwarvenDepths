#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Nascimento.Dev
{
    public class GuiStyles : EditorWindow
    {
        private Vector2 _scroll;
        private static GUIStyle DefaultPanelStyle
        {
            get
            {
                GUIStyle _defaultPanelStyle;
                _defaultPanelStyle = new GUIStyle();
                int value = (int)(Screen.width * 0.01f);
                _defaultPanelStyle.margin = new RectOffset(value, value, value, value);
                _defaultPanelStyle.fontSize = 12;

                return _defaultPanelStyle;
            }
        }

        protected GUIStyle _verticalPanel;
        protected GUIStyle VerticalPanel
        {
            get
            {
                if (_verticalPanel == null)
                {
                    _verticalPanel = CreateColorPanel(new Color(32 / (float)255, 67 / (float)255, 99 / (float)255));
                }

                return _verticalPanel;
            }
        }

        protected GUIStyle _horizontalPanel;
        protected GUIStyle HorizontalPanel
        {
            get
            {
                if (_horizontalPanel == null)
                {
                    _horizontalPanel = CreateColorPanel(new Color(0.1f, 0.1f, 0.1f));
                }

                return _horizontalPanel;
            }
        }

        protected GUIStyle CreateColorPanel(Color color)
        {
            GUIStyle panelStyle = DefaultPanelStyle;
            Texture2D texture2D = new Texture2D(5, 5);
            for (int x = 0; x < texture2D.width; x++)
            {
                for (int y = 0; y < texture2D.height; y++)
                {
                    texture2D.SetPixel(x, y, color);
                }
            }
            texture2D.Apply();
            panelStyle.normal.background = texture2D;
            return panelStyle;
        }

        public void UseVertical(UnityAction action)
        {
            GUILayout.BeginVertical(VerticalPanel);
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            GUILayout.EndVertical();
        }

        public void UseVertical(UnityAction action, GUIStyle style)
        {
            GUILayout.BeginVertical(style);
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            GUILayout.EndVertical();
        }

        public void UseHorizontal(UnityAction action)
        {
            GUILayout.BeginHorizontal(HorizontalPanel);
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            GUILayout.EndHorizontal();
        }

        public void UseHorizontal(UnityAction action, GUIStyle style)
        {
            GUILayout.BeginHorizontal(style);
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            GUILayout.EndHorizontal();
        }

        public void UseScroll(UnityAction action)
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll,
                GUILayout.Width(Screen.width * 0.95f),
                GUILayout.Height(Screen.height * 0.95f));
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            EditorGUILayout.EndScrollView();
        }
    }

}


#endif