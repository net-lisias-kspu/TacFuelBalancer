# TAC Fuel Balancer :: Change Log

* 2020-0811: 2.21.5.4 (lisias) for KSP >= 1.4.1
	+ Rolling back a bad idea. 
* 2020-0101: 2.21.5.3 (lisias) for KSP >= 1.4.1
	+ Updating for newest KSPe.
* 2019-1223: 2.21.5.2 (lisias) for KSP >= 1.4.1
	+ Making the thing to work again from KSP 1.4.1 and newer
	+ Moving Plugin to `net.lisias.ksp` hierarchy to prevent clashes with the upstream
		- It can be done to partless Add'Ons!
	+ Using KSPe facilities
		- Logging
		- Abstracted File System
		- Installment checks
* 2019-1221: 2.21.5.1 (linuxgurugamer) for KSP 1.8.0
	+ Changed variables into properties to avoid calling unity functions when not allowed to call
* 2019-1109: 2.21.5 (linuxgurugamer) for KSP 1.8.0
	+ updated for KSP 1.8
* 2019-0825: 2.21.4.1 (linuxgurugamer) for KSP 1.7.3
	+ Fixed toolbar icons by removing the file suffic at the toolbarControl creation
* 2019-0720: 2.21.4 (linuxgurugamer) for KSP [1.5.1, 1.6.1, 1.7.2]
	+ 2.21
	+ Adoption by Linuxgurugamer
	+ Rebuilt for 1.5.1, 1.6.1, 1.7.0
	+ Removed all old toolbar code, about 1500 lines
	+ Added support for the ToolbarController
	+ Added support for the ClickThroughBlocker
	+ Added fast toggles to fuel balance window
	+ Added new settings:
	+ ShowToggles
	+ ShowTooltips
	+ Make text in menu button yellow
	+ Added settings option to make tooltips optional
	+ Made edit mode only available when in sandbox mode (beginning of integrating career modes)
	+ Added a stock settings page, works alongside current settings page
	+ Added option to not use the KSP skin A
	+ dded option to disable old settings window
	+ 2.21.1
	+ Fixed InstallChecker to look for mod in PluginData directory
	+ Fixed UI opening by default
	+ Made the option to not show the Dump option only apply to the popup menu
	+ Made the positioning of the toggles configurable and display optional of each
	+ Made the pop-up menu optional
	+ Added option to not show non-transferable resources
	+ Changed location of saved config file from TacFuelBalancer\Plugins/PluginData/TacFuelBalancer\FuelBalancer.cfg to TacFuelBalancer\PluginData\FuelBalancer.cfg
	+ 2.21.2
	+ Fixed build issue with embedded icon resources
	+ Added info to help screens
	+ Moved settings around a little bit
	+ Added code to create the PluginData directory if it doesn't exist
	+ Fixed tooltip not hiding when showing the popup-menu
	+ 2.21.3
	+ Updated InstallChecker slightly
	+ Disabled the old settings screen
	+ 2.21.4
	+ Adjusted position of reset resource lists button
	+ Added code to load images from disk if unable to load from resource
* 2019-0715: 2.21.3 (linuxgurugamer) for KSP [1.5.1, 1.6.1, 1.7.2] PRE-RELEASE
	+ Updated InstallChecker slightly
	+ Disabled the old settings screen
* 2019-0711: 2.21.2 (linuxgurugamer) for KSP [1.5.1, 1.6.1, 1.7.2] PRE-RELEASE
* 2019-0710: 2.21.0 (linuxgurugamer) for KSP [1.5.1, 1.6.1, 1.7.2] PRE-RELEASE
* 2018-0816: 2.20 (zkeyaerospace) for KSP 1.4.5
	+ 06-Aug-2018 Built against KSP V1.4.5
			- Rebuilt for new version of KSP
* 2018-0706: 2.19 (zkeyaerospace) for KSP 1.4.4
	+ 06-July-2018 Built against KSP V1.4.4
			- Rebuilt for new version of KSP
* 2018-0504: 2.18 (zkeyaerospace) for KSP 1.4.3
	+ 30-Apr-2018 Built against KSP V1.4.3
			- Rebuilt for new version of KSP
* 2018-0404: 2.17 (zkeyaerospace) for KSP 1.4.2
	+ 03-Apr-2018 Built against KSP V1.4.2
			- Rebuilt for new version of KSP
* 2018-0317: 2.16 (zkeyaerospace) for KSP 1.4.1
	+ 16-Mar-2018 Built against KSP V1.4.1
			- Rebuilt for new version of KSP
* 2018-0310: 2.15 (zkeyaerospace) for KSP 1.4
	+ 07-Mar-2018 Built against KSP V1.4.0
			- Rebuilt for new version of KSP
* 2017-1008: 2.14 (zkeyaerospace) for KSP 1.3.1
	+ 07-Oct-2017 Built against KSP V1.3.1
			- Rebuilt for new version of KSP, no other changes
* 2017-0528: 2.13 (zkeyaerospace) for KSP 1.3.0
	+ 26-May-2017 Built against KSP V1.3.0
			- Rebuilt for new version of KSP
* 2016-1208: 2.12 (zkeyaerospace) for KSP 1.2.2
	+ 07-Dec-2016 Built against KSP V1.2.2
* 2016-1104: 2.11c (zkeyaerospace) for KSP 1.2.1
	+ Really removed dodgy config file this time.
* 2016-1104: 2.11b (zkeyaerospace) for KSP 1.2.1
	+ Removed bogus XML file
* 2016-1103: 2.11 (zkeyaerospace) for KSP 1.2.1
	+ 03-Nov-2016 Built against KSP V1.2.1
			- Rebuilt for new version of KSP
* 2016-1024: 2.10 (zkeyaerospace) for KSP 1.2
	+ 24-Oct-2016 Built against KSP V1.2
			- Fix for Contract Configurator/Toolbar bug
* 2016-1015: 2.9 (zkeyaerospace) for KSP 1.2
	+ 15-Oct-2016 Built against KSP V1.2
			- Rebuilt for KSP 1.2
* 2016-0622: 2.8 (zkeyaerospace) for KSP 1.1.3
	+ 22-June-2016 Built against KSP V1.1.3
			- Resource buttons act like tabs by default
			- Help and settings buttons toggle visibility
			- ToolTips
* 2016-0615: 2.7 (zkeyaerospace) for KSP 1.1.2
	+ 14-June-2016 Built against KSP V1.1.2
			- Stock toolbar "Launcher" button
			- New icon set
			- F2 support
			- Reset button
			- Support for mods that change tank contents
* 2016-0511: 2.6 (zkeyaerospace) for KSP 1.1.2
	+ 10-May-2016 Built against KSP V1.1.2
			- Added changes by AdamMil
* 2016-0504: 2.5.3 (zkeyaerospace) for KSP 1.1.2
	+ 03/May/2016
			- Updated for KSP V1.1.2
* 2015-0502: 2.5.1 (taraniselsu) for KSP 1.0.2
	+ Changes
		- Updated for KSP 1.0.2
* 2015-0430: 2.5 (taraniselsu) for KSP 1.0
	+ Changes
		- Updated for KSP 1.0
* 2014-1011: 2.4.1 (taraniselsu) for KSP 0.25
	+ Changes
		- Updated for KSP 0.25
		- The fuel transfer rate is now affected by time warp.
		- Reorganized the directory structure so that it is no longer placed in ThunderAerospace.
			- Warning: make sure that you completely uninstall any previous versions. I moved everything from KSP/GameData/ThunderAerospace/TacFuelBalancer to just KSP/GameData/TacFuelBalancer_
* 2014-0817: 2.4.0.3 (_ForgeUser15750166) for KSP 0.24.2
	+ Updated for KSP 0.24.2.
	+ Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
	+ Bug fix: the popup window should now close when the user clicks anywhere outside the popup
	+ window.
	+ Added an Install Checker.
	+ Displays the mod's version number in the Settings window.
	+ Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0817: 2.4 (taraniselsu) for KSP 0.24.2.
	+ Changes
		- Updated for KSP 0.24.2.
		- Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
		- Bug fix: the popup window should now close when the user clicks anywhere outside the popup
		- window.
		- Added an Install Checker.
		- Displays the mod's version number in the Settings window.
		- Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0608: 2.3.0.2 (_ForgeUser15750166) for KSP ['0.23.5', '0.23']
	+ No changelog provided
* 2013-1223: 2.3 (taraniselsu) for KSP 0.23
	+ Changes
		- Integrated with the Toolbar mod.
* 2013-1221: 2.2 (taraniselsu) for KSP 0.23
	+ Changes
		- Updated for 0.23
* 2015-0430: 2.5 (taraniselsu) for KSP 1.0
	+ Changes
		- Updated for KSP 1.0
* 2014-1011: 2.4.1 (taraniselsu) for KSP 0.25
	+ Changes
		- Updated for KSP 0.25
		- The fuel transfer rate is now affected by time warp.
		- Reorganized the directory structure so that it is no longer placed in ThunderAerospace.
			- Warning: make sure that you completely uninstall any previous versions. I moved everything from KSP/GameData/ThunderAerospace/TacFuelBalancer to just KSP/GameData/TacFuelBalancer_
* 2014-1011: 2.4.1 (taraniselsu) for KSP 0.25
	+ Changes
		- Updated for KSP 0.25
		- The fuel transfer rate is now affected by time warp.
		- Reorganized the directory structure so that it is no longer placed in ThunderAerospace.
			- Warning: make sure that you completely uninstall any previous versions. I moved everything from KSP/GameData/ThunderAerospace/TacFuelBalancer to just KSP/GameData/TacFuelBalancer_
* 2014-0817: 2.4.0.3 (_ForgeUser15750166) for KSP 0.24.2
	+ Updated for KSP 0.24.2.
	+ Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
	+ Bug fix: the popup window should now close when the user clicks anywhere outside the popup
	+ window.
	+ Added an Install Checker.
	+ Displays the mod's version number in the Settings window.
	+ Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0817: 2.4 (taraniselsu) for KSP 0.24.2.
	+ Changes
		- Updated for KSP 0.24.2.
		- Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
		- Bug fix: the popup window should now close when the user clicks anywhere outside the popup
		- window.
		- Added an Install Checker.
		- Displays the mod's version number in the Settings window.
		- Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0608: 2.3.0.2 (_ForgeUser15750166) for KSP ['0.23.5', '0.23']
	+ No changelog provided
* 2013-1223: 2.3 (taraniselsu) for KSP 0.23
	+ Changes
		- Integrated with the Toolbar mod.
* 2013-1221: 2.2 (taraniselsu) for KSP 0.23
	+ Changes
		- Updated for 0.23
* 2015-0430: 2.5 (taraniselsu) for KSP 1.0
	+ Changes
		- Updated for KSP 1.0
* 2014-1011: 2.4.1 (taraniselsu) for KSP 0.25
	+ Changes
		- Updated for KSP 0.25
		- The fuel transfer rate is now affected by time warp.
		- Reorganized the directory structure so that it is no longer placed in ThunderAerospace.
			- Warning: make sure that you completely uninstall any previous versions. I moved everything from KSP/GameData/ThunderAerospace/TacFuelBalancer to just KSP/GameData/TacFuelBalancer_
* 2014-1011: 2.4.1 (taraniselsu) for KSP 0.25
	+ Changes
		- Updated for KSP 0.25
		- The fuel transfer rate is now affected by time warp.
		- Reorganized the directory structure so that it is no longer placed in ThunderAerospace.
			- Warning: make sure that you completely uninstall any previous versions. I moved everything from KSP/GameData/ThunderAerospace/TacFuelBalancer to just KSP/GameData/TacFuelBalancer_
* 2014-0817: 2.4.0.3 (_ForgeUser15750166) for KSP 0.24.2
	+ Updated for KSP 0.24.2.
	+ Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
	+ Bug fix: the popup window should now close when the user clicks anywhere outside the popup
	+ window.
	+ Added an Install Checker.
	+ Displays the mod's version number in the Settings window.
	+ Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0817: 2.4 (taraniselsu) for KSP 0.24.2.
	+ Changes
		- Updated for KSP 0.24.2.
		- Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
		- Bug fix: the popup window should now close when the user clicks anywhere outside the popup
		- window.
		- Added an Install Checker.
		- Displays the mod's version number in the Settings window.
		- Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0608: 2.3.0.2 (_ForgeUser15750166) for KSP ['0.23.5', '0.23']
	+ No changelog provided
* 2013-1223: 2.3 (taraniselsu) for KSP 0.23
	+ Changes
		- Integrated with the Toolbar mod.
* 2013-1221: 2.2 (taraniselsu) for KSP 0.23
	+ Changes
		- Updated for 0.23
* 2014-0817: 2.4.0.3 (_ForgeUser15750166) for KSP 0.24.2
	+ Updated for KSP 0.24.2.
	+ Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
	+ Bug fix: the popup window should now close when the user clicks anywhere outside the popup
	+ window.
	+ Added an Install Checker.
	+ Displays the mod's version number in the Settings window.
	+ Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0817: 2.4 (taraniselsu) for KSP 0.24.2.
	+ Changes
		- Updated for KSP 0.24.2.
		- Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
		- Bug fix: the popup window should now close when the user clicks anywhere outside the popup
		- window.
		- Added an Install Checker.
		- Displays the mod's version number in the Settings window.
		- Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0608: 2.3.0.2 (_ForgeUser15750166) for KSP ['0.23.5', '0.23']
	+ No changelog provided
* 2013-1223: 2.3 (taraniselsu) for KSP 0.23
	+ Changes
		- Integrated with the Toolbar mod.
* 2013-1221: 2.2 (taraniselsu) for KSP 0.23
	+ Changes
		- Updated for 0.23
* 2014-0817: 2.4.0.3 (_ForgeUser15750166) for KSP 0.24.2
	+ Updated for KSP 0.24.2.
	+ Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
	+ Bug fix: the popup window should now close when the user clicks anywhere outside the popup
	+ window.
	+ Added an Install Checker.
	+ Displays the mod's version number in the Settings window.
	+ Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0817: 2.4 (taraniselsu) for KSP 0.24.2.
	+ Changes
		- Updated for KSP 0.24.2.
		- Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
		- Bug fix: the popup window should now close when the user clicks anywhere outside the popup
		- window.
		- Added an Install Checker.
		- Displays the mod's version number in the Settings window.
		- Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0608: 2.3.0.2 (_ForgeUser15750166) for KSP ['0.23.5', '0.23']
	+ No changelog provided
* 2013-1223: 2.3 (taraniselsu) for KSP 0.23
	+ Changes
		- Integrated with the Toolbar mod.
* 2013-1221: 2.2 (taraniselsu) for KSP 0.23
	+ Changes
		- Updated for 0.23
* 2014-0817: 2.4.0.3 (_ForgeUser15750166) for KSP 0.24.2
	+ Updated for KSP 0.24.2.
	+ Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
	+ Bug fix: the popup window should now close when the user clicks anywhere outside the popup
	+ window.
	+ Added an Install Checker.
	+ Displays the mod's version number in the Settings window.
	+ Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0817: 2.4 (taraniselsu) for KSP 0.24.2.
	+ Changes
		- Updated for KSP 0.24.2.
		- Bug fix: was letting the user edit fuel levels on other planets/moons besides Kerbin.
		- Bug fix: the popup window should now close when the user clicks anywhere outside the popup
		- window.
		- Added an Install Checker.
		- Displays the mod's version number in the Settings window.
		- Added support for the [KSP Add-on Version Checker (KSP-AVC)](http://forum.kerbalspaceprogram.com/threads/71488) and the [KSP-AVC plug-in](http://forum.kerbalspaceprogram.com/threads/79745)
* 2014-0608: 2.3.0.2 (_ForgeUser15750166) for KSP ['0.23.5', '0.23']
	+ No changelog provided
* 2013-1223: 2.3 (taraniselsu) for KSP 0.23
	+ Changes
		- Integrated with the Toolbar mod.
* 2013-1221: 2.2 (taraniselsu) for KSP 0.23
	+ Changes
		- Updated for 0.23
