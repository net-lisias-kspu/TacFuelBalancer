/**
 * MainWindow.cs
 * 
 * Thunder Aerospace Corporation's Fuel Balancer for the Kerbal Space Program, by Taranis Elsu
 * 
 * (C) Copyright 2013, Taranis Elsu
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 * 
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 * 
 * Non-commercial - You may not use this work for commercial purposes.
 * 
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tac
{
    class MainWindow : Window<TacFuelBalancer>
    {
        private readonly FuelBalanceController controller;
        private readonly Settings settings;
        private readonly SettingsWindow settingsWindow;
        private readonly HelpWindow helpWindow;

        private Vector2 headerScrollPosition = Vector2.zero;
        private Vector2 scrollPosition = Vector2.zero;

        private GUIStyle buttonStyle;
        private GUIStyle coloredButtonStyle;
        private GUIStyle labelStyle;
        private GUIStyle sectionStyle;
        private GUIStyle popupButtonStyle;
        private GUIStyle editStyle;

        private GUIContent settingsContent;
        private GUIContent helpContent;
        private GUIContent resetContent;

        public ResourceInfo lastResourceClicked;
        public ResourcePartMap lastPartClicked;
        private double newAmount;
        private bool isControllable;


        public MainWindow(FuelBalanceController controller, Settings settings, SettingsWindow settingsWindow, HelpWindow helpWindow)
            : base("TAC Fuel Balancer", 500, 500)
        {
            Debug.Log("MainWindow");
            this.controller = controller;
            this.settings = settings;
            this.settingsWindow = settingsWindow;
            this.helpWindow = helpWindow;
            //SetVisible(true);

            //var settingstexture = TextureHelper.FromResource("Tac.icons.settings.png", 16, 16);
            Texture2D settingstexture = null;
            try
            {
                settingstexture = TextureHelper.FromResource("icons.settings.png", 16, 16);
            }
            catch { }
            settingsContent = (settingstexture != null) ? new GUIContent(settingstexture, "Settings window") : new GUIContent("S", "Settings window");

            //var helptexture = TextureHelper.FromResource("Tac.icons.help.png", 16, 16);
            Texture2D helptexture = null;
            try
            {
                helptexture = TextureHelper.FromResource("icons.help.png", 16, 16);
            }
            catch { }
            helpContent = (helptexture != null) ? new GUIContent(helptexture, "Help window") : new GUIContent("?", "Help window");

            //var resettexture = TextureHelper.FromResource("Tac.icons.reset.png", 16, 16);
            Texture2D resettexture = null;
            try
            {
                resettexture = TextureHelper.FromResource("icons.reset.png", 16, 16);
            }
            catch { }
            resetContent = (resettexture != null) ? new GUIContent(resettexture, "Reset resource lists") : new GUIContent("?", "Reset resource lists");
        }

        public override void SetVisible(bool newValue)
        {
            base.SetVisible(newValue);
            if (!newValue)
            {
                settingsWindow.SetVisible(false);
                helpWindow.SetVisible(false);
                foreach (ResourcePartMap part in EnumerateSelectedParts()) // hide automatic highlights when the window is closed
                {
                    if (!part.isHighlighted)
                    {
                        part.part.SetHighlightDefault();
                    }
                }
            }
        }

        protected override void ConfigureStyles()
        {
            base.ConfigureStyles();

            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(_skin.button);
                buttonStyle.alignment = TextAnchor.LowerCenter;
                buttonStyle.fontStyle = FontStyle.Normal;
                buttonStyle.padding.top = 3;
                buttonStyle.padding.bottom = 1;
                buttonStyle.stretchWidth = false;
                buttonStyle.stretchHeight = false;

                coloredButtonStyle = new GUIStyle(buttonStyle);
                coloredButtonStyle.normal.textColor = Color.yellow;
                coloredButtonStyle.hover.textColor = Color.yellow;
                coloredButtonStyle.active.textColor = Color.yellow;
                coloredButtonStyle.fontStyle = FontStyle.Bold; ;

                labelStyle = new GUIStyle(_skin.label);
                labelStyle.alignment = TextAnchor.MiddleRight;
                labelStyle.fontStyle = FontStyle.Normal;
                labelStyle.wordWrap = false;

                sectionStyle = new GUIStyle(_skin.label);
                sectionStyle.alignment = TextAnchor.LowerLeft;
                sectionStyle.fontStyle = FontStyle.Bold;
                sectionStyle.padding.top += 2;
                sectionStyle.normal.textColor = Color.white;
                sectionStyle.wordWrap = false;

                popupButtonStyle = new GUIStyle(_skin.button);
                popupButtonStyle.alignment = TextAnchor.MiddleCenter;
                popupButtonStyle.margin = new RectOffset(2, 2, 2, 2);
                popupButtonStyle.padding = new RectOffset(3, 3, 3, 0);

                editStyle = new GUIStyle(_skin.textField);
                editStyle.fontStyle = FontStyle.Normal;
            }
        }

        // avoid allocating new options and arrays for every cell
        static GUILayoutOption[] width20 = new[] { GUILayout.Width(20) }, width46 = new[] { GUILayout.Width(46) }, width60 = new[] { GUILayout.Width(60) };

        protected override void DrawWindowContents(int windowID)
        {
            headerScrollPosition = GUILayout.BeginScrollView(headerScrollPosition, GUILayout.ExpandHeight(false));
            GUILayout.BeginHorizontal();
            List<ResourceInfo> sortedResources = controller.GetResourceInfo().Values.ToList();
            sortedResources.Sort((a, b) => a.title.CompareTo(b.title));
            foreach (ResourceInfo resource in sortedResources)
            {
                if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().hideNontransferableResources &&
                        (resource.parts[0].resource.TransferMode != ResourceTransferMode.PUMP || !isControllable)
                                 )
                    continue;

                bool toggled = (GUILayout.Toggle(resource.isShowing, resource.title, buttonStyle) != resource.isShowing);
                if (toggled)
                {
                    SetResourceVisibility(resource, !resource.isShowing);
                    if (resource.isShowing && settings.OneTabOnly)
                    {
                        foreach (ResourceInfo otherResource in sortedResources)
                        {
                            if (otherResource != resource)
                            {
                                SetResourceVisibility(otherResource, false);
                            }
                        }
                    }
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.BeginVertical();

            // cache the value so we only do it once per call
            isControllable = controller.IsControllable();

            // avoid allocating new options and arrays for every cell
            //GUILayoutOption[] width20 = new[] { GUILayout.Width(20) }, width46 = new[] { GUILayout.Width(46) }, width60 = new[] { GUILayout.Width(60) };
            foreach (ResourceInfo resource in sortedResources)
            {
                if (resource.isShowing)
                {
                    if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().hideNontransferableResources &&
                        (resource.parts[0].resource.TransferMode != ResourceTransferMode.PUMP || ! isControllable)
                                 )
                        continue;

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    GUILayout.Label(resource.title, sectionStyle, GUILayout.Width(100));
                    if (resource.parts[0].resource.TransferMode == ResourceTransferMode.PUMP && isControllable)
                    {
                        resource.balance = GUILayout.Toggle(resource.balance, "Balance All", buttonStyle);
                    }
                    if (GUILayout.Button("Select All", buttonStyle))
                    {
                        bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
                        SelectParts(resource, resource.parts, resource.parts.Any(p => !p.isSelected), ctrl);
                        ResetRangeSelection();
                    }
                    PopupWindow.Draw("Sort", windowPos, DrawSortMenu, buttonStyle, null);


                    GUILayout.EndHorizontal();

                    foreach (ResourcePartMap partInfo in resource.parts)
                    {
                        PartResourceInfo partResource = partInfo.resource;
                        double percentFull = partResource.PercentFull * 100.0;

                        if (percentFull < settings.FuelCriticalLevel)
                        {
                            labelStyle.normal.textColor = new Color(0.88f, 0.20f, 0.20f, 1.0f);
                        }
                        else if (percentFull < settings.FuelWarningLevel)
                        {
                            labelStyle.normal.textColor = Color.yellow;
                        }
                        else
                        {
                            labelStyle.normal.textColor = Color.white;
                        }

                        labelStyle.fontStyle = partInfo.isSelected ? FontStyle.Bold : FontStyle.Normal;

                        GUILayout.BeginHorizontal();
                        string partTitle = partInfo.part.partInfo.title;

                        if (GUILayout.Button(partTitle.Substring(0, Math.Min(30, partTitle.Length)), labelStyle)) // if the user clicked an item name...
                        {
                            bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                            bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
                            bool alt = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
                            if (alt) // alt-click selects either all parts on the same ship or all parts of the same type
                            {
                                List<ResourcePartMap> parts;
                                if (shift)
                                {
                                    parts = resource.parts.FindAll(p => p.part.partInfo.title == partTitle);
                                }
                                else
                                {
                                    parts = resource.parts.FindAll(p => p.shipId == partInfo.shipId);
                                }
                                SelectParts(resource, parts, parts.Any(p => !p.isSelected), ctrl); // select all those parts if any is unselected
                                ResetRangeSelection();
                            }
                            else // otherwise, select and deselect items normally
                            {
                                ICollection<ResourcePartMap> parts;
                                // shift-click selects a range of items in the view                               
                                if (shift && resource == lastResourceClicked && partInfo != lastPartClicked && lastPartClicked != null)
                                {
                                    int partIndex = resource.parts.IndexOf(partInfo), lastIndex = resource.parts.IndexOf(lastPartClicked);
                                    if (lastIndex < 0) lastIndex = partIndex;
                                    parts = resource.parts.GetRange(Math.Min(partIndex, lastIndex), Math.Abs(partIndex - lastIndex) + 1);
                                }
                                else
                                {
                                    parts = new ResourcePartMap[] { partInfo };
                                }
                                SelectParts(resource, parts, !partInfo.isSelected || !ctrl && resource.parts.Count(p => p.isSelected) > parts.Count, ctrl);
                                lastResourceClicked = resource;
                                lastPartClicked = partInfo;
                            }
                        }

                        GUILayout.FlexibleSpace();
                        if (settings.ShowShipNumber)
                        {
                            GUILayout.Label(partInfo.shipId.ToString(), labelStyle, width20);
                        }
                        if (settings.ShowStageNumber)
                        {
                            GUILayout.Label(partInfo.part.inverseStage.ToString(), labelStyle, width20);
                        }
                        if (settings.ShowMaxAmount)
                        {
                            GUILayout.Label(partResource.MaxAmount.ToString("N1"), labelStyle, width60);
                        }
                        if (settings.ShowCurrentAmount)
                        {
                            GUILayout.Label(partResource.Amount.ToString("N1"), labelStyle, width60);
                        }
                        if (settings.ShowPercentFull)
                        {
                            GUILayout.Label(percentFull.ToString("N1") + "%", labelStyle, width46);
                        }

                        GUILayout.Space(10);
                        //  Lock, Transfer In, Transfer Out, Balance, Dump, Highlight, then maybe Edit...??

                        if (settings.ShowToggles)
                        {
                            List<ResourcePartMap> selectedParts = partInfo.isSelected ? EnumerateSelectedParts().ToList() : new List<ResourcePartMap>(1) { partInfo };
                            bool canPump = true, allHighlighted = true;
                            foreach (ResourcePartMap part in selectedParts)
                            {
                                canPump &= part.resource.TransferMode == ResourceTransferMode.PUMP;
                                allHighlighted &= part.isHighlighted;
                            }
                            for (int toggleNum = 1; toggleNum < 6; toggleNum++)
                            {
                                if (canPump && isControllable)
                                {
                                    if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().lockedPos == toggleNum)
                                        DrawToggleButton(partInfo, TransferDirection.LOCKED, "L", "Lock");
                                    if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().transferInPos == toggleNum)
                                        DrawToggleButton(partInfo, TransferDirection.IN, "I", "Transfer In");
                                    if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().transferOutPos == toggleNum)
                                        DrawToggleButton(partInfo, TransferDirection.OUT, "O", "Transfer Out");
                                    if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().balancePos == toggleNum)
                                        DrawToggleButton(partInfo, TransferDirection.BALANCE, "B", "Balance");
                                    if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().dumpPos == toggleNum)
                                    {
                                        DrawToggleButton(partInfo, TransferDirection.DUMP, "D", "Dump");
                                    }
                                }
                                if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().lockedPos == toggleNum)
                                {
                                    bool highlight = GUILayout.Toggle(allHighlighted, new GUIContent("H", "Highlight"), buttonStyle, width20);
                                    if (highlight != allHighlighted)
                                    {
                                        foreach (ResourcePartMap part in selectedParts)
                                        {
                                            if (!highlight && part.isHighlighted && !part.isSelected)
                                            {
                                                part.part.SetHighlightDefault();
                                            }
                                            part.isHighlighted = highlight;
                                        }
                                        //guiChanged = true;
                                    }
                                }
                            }
                        }
                        if (HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().popupMenu)
                            PopupWindow.Draw(GetControlText(partInfo), windowPos, DrawPopupContents, coloredButtonStyle, partInfo, width20);

                        GUILayout.EndHorizontal();
                    }
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            if (GUILayout.Toggle((settings.RateMultiplier == 100.0), "x100", buttonStyle))
            {
                settings.RateMultiplier = 100.0;
            }
            if (GUILayout.Toggle((settings.RateMultiplier == 10.0), "x10", buttonStyle))
            {
                settings.RateMultiplier = 10.0;
            }
            if (GUILayout.Toggle((settings.RateMultiplier == 1.0), "x1", buttonStyle))
            {
                settings.RateMultiplier = 1.0;
            }
            if (GUILayout.Toggle((settings.RateMultiplier == 0.1), "x0.1", buttonStyle))
            {
                settings.RateMultiplier = 0.1;
            }
            GUILayout.EndHorizontal();



            // Extra title bar buttons
            if (GUI.Button(new Rect(windowPos.width - 72, 4, 20, 20), resetContent, closeButtonStyle))
            {
                controller.RebuildActiveVesselLists();
            }
            if (!HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().disableOldSettings)
            {
                if (GUI.Button(new Rect(windowPos.width - 48, 4, 20, 20), settingsContent, closeButtonStyle))
                {
                    settingsWindow.ToggleVisible();
                }
            }
            if (GUI.Button(new Rect(windowPos.width - 24, 4, 20, 20), helpContent, closeButtonStyle))
            {
                helpWindow.ToggleVisible();
            }
        }



        private string GetControlText(ResourcePartMap partInfo)
        {
            switch (partInfo.direction)
            {
                case TransferDirection.IN:
                    return "I";
                case TransferDirection.OUT:
                    return "O";
                case TransferDirection.BALANCE:
                    return "B";
                case TransferDirection.DUMP:
                    return "D";
                case TransferDirection.LOCKED:
                    return "L";
                default:
                    return partInfo.isHighlighted ? "H" : "-";
            }
        }

        private IEnumerable<ResourcePartMap> EnumerateSelectedParts()
        {
            return from resource in controller.GetResourceInfo().Values
                   where resource.isShowing
                   from part in resource.parts
                   where part.isSelected
                   select part;
        }

        private bool DrawPopupContents(int windowId, object parameter)
        {
            _lastTooltip = string.Empty;
            ResourcePartMap clickedPart = (ResourcePartMap)parameter;

            List<ResourcePartMap> parts = clickedPart.isSelected ?
                EnumerateSelectedParts().ToList() : new List<ResourcePartMap>(1) { clickedPart };
            bool canPump = true, allHighlighted = true, guiChanged = false;
            foreach (ResourcePartMap part in parts)
            {
                canPump &= part.resource.TransferMode == ResourceTransferMode.PUMP;
                allHighlighted &= part.isHighlighted;
            }

            bool highlight = GUILayout.Toggle(allHighlighted, "Highlight", popupButtonStyle);
            if (highlight != allHighlighted)
            {
                foreach (ResourcePartMap part in parts)
                {
                    if (!highlight && part.isHighlighted && !part.isSelected)
                    {
                        part.part.SetHighlightDefault();
                    }
                    part.isHighlighted = highlight;
                }
                guiChanged = true;
            }

            if (HighLogic.CurrentGame.Mode == Game.Modes.SANDBOX)
            {
                if (controller.IsPrelaunch() && parts.Count == 1) // only allow editing when a single part is selected
                {
                    newAmount = clickedPart.resource.Amount;
                    PopupWindow.Draw("Edit", windowPos, DrawEditPopupContents, popupButtonStyle, clickedPart);
                }
            }

            if (canPump && isControllable)
            {
                TransferDirection direction = clickedPart.direction;
                bool? toggleChange = null; // how a toggle was changed, if at all
                foreach (ResourcePartMap part in parts)
                {
                    if (part.direction != direction)
                    {
                        direction = TransferDirection.VARIOUS;
                        break;
                    }
                }

                DrawPopupToggle(TransferDirection.NONE, "Stop", ref direction, ref toggleChange);
                DrawPopupToggle(TransferDirection.IN, "Transfer In", ref direction, ref toggleChange);
                DrawPopupToggle(TransferDirection.OUT, "Transfer Out", ref direction, ref toggleChange);
                DrawPopupToggle(TransferDirection.BALANCE, "Balance", ref direction, ref toggleChange);
                if (settings.ShowDump)
                {
                    DrawPopupToggle(TransferDirection.DUMP, "Dump", ref direction, ref toggleChange);
                }
                DrawPopupToggle(TransferDirection.LOCKED, "Lock", ref direction, ref toggleChange);

                if (toggleChange.HasValue)
                {
                    foreach (ResourcePartMap part in parts)
                    {
                        if (!toggleChange.Value) // if the user turned a direction off...
                        {
                            if (part.direction == direction)
                            {
                                if (direction == TransferDirection.LOCKED)
                                {
                                    part.resource.Locked = false;
                                }
                                part.direction = TransferDirection.NONE;
                            }
                        }
                        else if (part.direction != direction)
                        {
                            if (direction == TransferDirection.LOCKED)
                            {
                                part.resource.Locked = true;
                            }
                            part.direction = direction;
                        }
                    }

                    guiChanged = true;
                }
            }

            return guiChanged;
        }

        private void DrawToggleButton(ResourcePartMap partInfo, TransferDirection toggleDirection, string text, string toolTip)
        {
            bool b = (partInfo.direction == toggleDirection);
            bool bout = GUILayout.Toggle(b, new GUIContent(text, toolTip), buttonStyle, width20);
            if (b && !bout)
            {
                partInfo.direction = TransferDirection.NONE;
            }
            else
            {
                if (bout)
                    partInfo.direction = toggleDirection;
            }
            if (b!=bout)
            {
                List<ResourcePartMap> parts = partInfo.isSelected ?  EnumerateSelectedParts().ToList() : new List<ResourcePartMap>(1) { partInfo };
                foreach (ResourcePartMap rmp in parts)
                {
                    if (partInfo.direction == TransferDirection.NONE) // if the user turned a direction off...
                    {
                        //if (rmp.direction != toggleDirection)
                        {
                            if (toggleDirection == TransferDirection.LOCKED)
                            {
                                rmp.resource.Locked = false;

                                rmp.direction = TransferDirection.NONE;
                            }
                        }
                    }
                    else
                    {
                        //if (rmp.direction != toggleDirection)
                        {
                            if (toggleDirection == TransferDirection.LOCKED)
                            {
                                rmp.resource.Locked = true;

                                rmp.direction = toggleDirection;
                            }
                        }
                    }
                }
            }
        }

        private void DrawPopupToggle(TransferDirection toggleDirection, string name, ref TransferDirection currentDirection,
                                     ref bool? toggleChange)
        {
            if (GUILayout.Toggle((currentDirection == toggleDirection), name, popupButtonStyle))
            {
                if (currentDirection != toggleDirection)
                {
                    currentDirection = toggleDirection;
                    toggleChange = true;
                }
            }
            else if (currentDirection == toggleDirection)
            {
                toggleChange = false;
            }
        }

        private bool DrawEditPopupContents(int windowId, object parameter)
        {
            ResourcePartMap partInfo = (ResourcePartMap)parameter;
            bool shouldClose = false;

            if (newAmount > partInfo.resource.MaxAmount || newAmount < 0)
            {
                editStyle.normal.textColor = Color.red;
                editStyle.focused.textColor = Color.red;
                labelStyle.normal.textColor = Color.red;
            }
            else
            {
                editStyle.normal.textColor = Color.white;
                editStyle.focused.textColor = Color.white;
                labelStyle.normal.textColor = Color.white;
            }

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Empty", buttonStyle))
            {
                newAmount = 0;
            }
            if (GUILayout.Button("Fill", buttonStyle))
            {
                newAmount = partInfo.resource.MaxAmount;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Enter new amount:", labelStyle);
            newAmount = Utilities.ShowTextField(newAmount, 10, editStyle, GUILayout.MinWidth(60));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("OK", buttonStyle) && newAmount <= partInfo.resource.MaxAmount && newAmount >= 0)
            {
                partInfo.resource.SetAmount(newAmount);
                shouldClose = true;
            }
            if (GUILayout.Button("Cancel", buttonStyle))
            {
                shouldClose = true;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            return shouldClose;
        }

        private bool DrawSortMenu(int windowId, object parameter)
        {
            Comparison<ResourcePartMap> comparer = null;
            if (GUILayout.Button("Name", popupButtonStyle))
            {
                comparer = (a, b) => a.part.partInfo.title.CompareTo(b.part.partInfo.title);
            }
            else if (settings.ShowShipNumber && GUILayout.Button("Ship ID", popupButtonStyle))
            {
                comparer = (a, b) => a.shipId - b.shipId;
            }
            else if (settings.ShowStageNumber && GUILayout.Button("Stage", popupButtonStyle))
            {
                comparer = (a, b) => a.part.inverseStage - b.part.inverseStage;
            }
            else if (settings.ShowMaxAmount && GUILayout.Button("Capacity", popupButtonStyle))
            {
                comparer = (a, b) => a.resource.MaxAmount.CompareTo(b.resource.MaxAmount);
            }
            else if (settings.ShowCurrentAmount && GUILayout.Button("Amount", popupButtonStyle))
            {
                comparer = (a, b) => a.resource.Amount.CompareTo(b.resource.Amount);
            }
            else if (settings.ShowPercentFull && GUILayout.Button("Percent Full", popupButtonStyle))
            {
                comparer = (a, b) => a.resource.PercentFull.CompareTo(b.resource.PercentFull);
            }

            if (comparer != null)
            {
                controller.SortParts(comparer);
            }
            return comparer != null;
        }

        private void ResetRangeSelection()
        {
            lastResourceClicked = null;
            lastPartClicked = null;
        }

        private void SelectPart(ResourceInfo resource, ResourcePartMap part, bool selected)
        {
            part.isSelected = selected;
            if ((!selected || !resource.isShowing) && !part.isHighlighted)
            {
                part.part.SetHighlightDefault();
            }
        }

        private void SelectParts(ResourceInfo resource, IEnumerable<ResourcePartMap> parts, bool select, bool dontClear)
        {
            if (!dontClear)
            {
                foreach (ResourceInfo res in controller.GetResourceInfo().Values)
                {
                    foreach (ResourcePartMap part in res.parts)
                    {
                        SelectPart(res, part, false);
                    }
                }
            }

            if (select || dontClear)
            {
                foreach (ResourcePartMap part in parts)
                {
                    SelectPart(resource, part, select);
                }
            }
        }

        private void SetResourceVisibility(ResourceInfo resource, bool visible)
        {
            if (resource.isShowing != visible)
            {
                resource.isShowing = visible;
                if (!visible)
                {
                    if (resource == lastResourceClicked)
                    {
                        ResetRangeSelection();
                    }
                    foreach (ResourcePartMap part in resource.parts)
                    {
                        SelectPart(resource, part, false);
                    }
                }
            }
        }
    }
}
