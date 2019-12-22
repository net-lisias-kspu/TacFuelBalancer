﻿/**
 * HelpWindow.cs
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
using System.Linq;
using UnityEngine;

namespace Tac
{
    class HelpWindow : Window<TacFuelBalancer>
    {
        private GUIStyle labelStyle;
        private GUIStyle sectionStyle;
        private Vector2 scrollPosition;

        public HelpWindow()
            : base("TAC Fuel Balancer Help", 500, Screen.height * 0.75f)
        {
            Debug.Log("HelpWindow");
            scrollPosition = Vector2.zero;
        }

        protected override void ConfigureStyles()
        {
            base.ConfigureStyles();

            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(_skin.label);
                labelStyle.wordWrap = true;
                labelStyle.fontStyle = FontStyle.Normal;
                labelStyle.normal.textColor = Color.white;
                labelStyle.stretchWidth = true;
                labelStyle.stretchHeight = false;
                labelStyle.margin.bottom -= 2;
                labelStyle.padding.bottom -= 2;

                sectionStyle = new GUIStyle(labelStyle);
                sectionStyle.fontStyle = FontStyle.Bold;
            }
        }

        protected override void DrawWindowContents(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));

            GUILayout.Label("Fuel Balancer by Taranis Elsu of Thunder Aerospace Corporation.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("Copyright (c) Thunder Aerospace Corporation. Patents pending.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Space(10);
            GUILayout.Label("Now supported by Linuxgurugamer", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Space(20);
            GUILayout.Label("Features", sectionStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Highlight - highlights/marks the part so you can find/remember it.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Edit - edit the amount in the part (only available when playing Sandbox, Prelaunch or when Landed).", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Lock - prevents it from transferring the resource into or out of the part.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* In - transfers the resource into the part, taking an equal amount from each other part.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Out - transfers the resource out of the part, putting an equal amount in each other part.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Balance - transfers the resource around such that all parts being balanced are the same percentage full.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Balance All - balances all parts that are not in one of the other modes (In, Out, Lock, etc).", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Selects All - selects or deselects all parts in the resource. Can be combined with Ctrl to modify the current selection.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Sort - sorts the parts. You can sort by multiple columns by selecting them in reverse order.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Space(20);
            GUILayout.Label("You can either transfer resources from individual parts or select multiple parts and execute the transfer for all of them at once. " +
                            "To select a part, click the name. Shift-clicking will select a contiguous range of parts from the last part clicked. (This is useful with sorting.) " +
                            "Alt-clicking will select all parts belonging to the same \"ship\" (i.e. a subtree of parts not separated by any docking bays). " +
                            "Shift-alt-clicking will select all parts of the same type. All of these can be combined with Ctrl to modify rather than replace " +
                            "the current selection, so a shift-alt-click followed by ctrl-alt-clicks can select all parts of a given type on a given ship, for example.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Space(20);
            GUILayout.Label("Note that it can transfer any resource that uses the \"pump\" resource transfer mode, including liquid fuel, oxidizer, electric charge, Kethane, and RCS fuel; but not resources such as solid rocket fuel.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Space(20);
            GUILayout.Label("Settings", sectionStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* All settings are being moved to the stock settings page.  The old settings window currently available will be retired shortly", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Auto-deselect Tabs - causes the resource selector to behave like a normal tab control, so only one resource can be shown at a time, but only one click is needed to change between them.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Maximum Fuel Flow Rate - controls how quickly fuel is transfered around. This limits each action to only transfer up to the selected amount.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Fuel Warning Level - warns (yellow) when a resource drops below this percentage of capacity.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Fuel Warning Level - warns (red) when a resource drops below this percentage of capacity.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Show <whatever> - toggles the display of the columns on the main window.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Balance In's - when this is enabled, it will balance the resource level between parts that are set to In. Note that this can cause the resource level in a part to drop until it evens out with the other parts, then it will start increasing again.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Balance Out's - when this is enabled, it will balance the resource level between parts that are set to Out. Note that this can cause the resource level in a part to rise until it evens out with the other parts, then it will start decreasing again.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label(" ");
            GUILayout.Label("* Toggle Position & Misc", sectionStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* The first six lines in the Toggle Position & Misc section controls which toggles are shown on the main windiw, and the order they are shown in.", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* To disable a toggle from showing, set the value to 0", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* To change the skin from the KSP skin to a smaller and darker skin, unset the Use KSP Skin", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Since the old settings page is still around, there is an option to disable the old settings page", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("* Finally, there is a Debug mode.  This is currently not used, but is referenced in the code and is reserved for future use", labelStyle, GUILayout.ExpandWidth(true));


            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            GUILayout.Space(8);
        }
    }
}
