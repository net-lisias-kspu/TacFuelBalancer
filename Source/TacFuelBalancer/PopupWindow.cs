/*
	This file is part of Thunder Aerospace Corporation's (TAC) Fuel Balancer /L Unleashed
		? 2019-2021 Lisias T : http://lisias.net <support@lisias.net>
		? 2019 linuxgurugamer
		? 2016-2018 thwebbooth
		? 2013-2015 Taranis Elsu

	TAC Fuel Balancer /L Unleashed is licensed as follows:

		* CC BY-NC-SA 3.0 : http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode

	TAC Fuel Balancer /L Unleashed is distributed in the hope that
	it will be useful, but WITHOUT ANY WARRANTY; without even the implied
	warranty of	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

*/
using System;
using UnityEngine;

using GUI = KSPe.UI.GUI;
using GUILayout = KSPe.UI.GUILayout;

namespace Tac
{
    public class PopupWindow : MonoBehaviour
    {
        private static GameObject go;
        private static PopupWindow instance;
        private readonly int windowId;
        private bool showPopup;
        private Rect popupPos;
        private Func<int, object, bool> callback;
        private object parameter;

        private static PopupWindow GetInstance()
        {
            if (go == null)
            {
                go = new GameObject("TacPopupWindow");
                instance = go.AddComponent<PopupWindow>();
            }
            return instance;
        }

        PopupWindow()
        {
            windowId = "Tac.PopupWindow".GetHashCode();
        }

        void Awake()
        {
            showPopup = false;
        }

        void OnGUI()
        {
            if (showPopup)
            {
                GUI.skin = HighLogic.Skin;
                popupPos = Utilities.EnsureCompletelyVisible(popupPos);
                popupPos = GUILayout.Window(windowId, popupPos, DrawPopupContents, "");
            }
        }

        private void DrawPopupContents(int windowId)
        {
            GUI.BringWindowToFront(windowId);

            var pos = popupPos;
            var c = callback;

            bool shouldClose = callback(windowId, parameter);

            if (shouldClose && c == callback)
            {
                showPopup = false;
            }

            // Close the popup window if clicked somewhere outside it
            if (c == callback && (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
            {
                var mousePos = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
                if (!pos.Contains(mousePos))
                {
                    showPopup = false;
                }
            }
        }

        public static void Draw(string buttonText, Rect windowPos, Func<int, object, bool> popupDrawCallback, GUIStyle buttonStyle, object parameter, params GUILayoutOption[] options)
        {
            PopupWindow pw = PopupWindow.GetInstance();
            GUIContent content;
            if (buttonText.Length == 1)
                content = new GUIContent(buttonText, "Menu");
            else
                content = new GUIContent(buttonText);
            var rect = GUILayoutUtility.GetRect(content, buttonStyle, options);
            if (GUI.Button(rect, content, buttonStyle))
            {
                pw.showPopup = true;

                var mouse = Input.mousePosition;
                pw.popupPos = new Rect(mouse.x - 10, Screen.height - mouse.y - 10, 10, 10);

                pw.callback = popupDrawCallback;
                pw.parameter = parameter;
            }
        }
    }
}
