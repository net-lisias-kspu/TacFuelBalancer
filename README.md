# TAC Fuel Balancer /L Unofficial

A Fuel Balancer from Thunder Aerospace Corporation (TAC), designed by Taranis Elsu. Unofficial fork by Lisias.


## In a Hurry

* [Latest Release](https://github.com/net-lisias-kspu/TacFuelBalancer/releases)
	+ [Binaries](https://github.com/net-lisias-kspu/TacFuelBalancer/tree/Archive)
* [Source](https://github.com/net-lisias-kspu/TacFuelBalancer)
* Documentation
	+ [Project's README](https://github.com/net-lisias-kspu/TacFuelBalancer/blob/master/README.md)
	+ [Install Instructions](https://github.com/net-lisias-kspu/TacFuelBalancer/blob/master/INSTALL.md)
	+ [Change Log](./CHANGE_LOG.md)
	+ [TODO](./TODO.md) list


## Description

A Fuel Balancer from Thunder Aerospace Corporation (TAC)

### Features
* Transfer a resource into a part, drawing an equal amount from each other part.
* Transfer a resource out of a part, transferring an equal amount into each other part.
* Dump a resource out of a part. Note that the resource is lost, never to be found.
* When still Prelaunch (or Landed): edit the amount of a resource loaded in a part. Works on all resources, even solid rocket fuel.
* Enable balance mode to transfer a resource such that all parts are the same percentage full.
* Lock a part, so that none of the resource will be transferred into or out of the part.

Note that it can transfer any resource that uses the "pump" resource transfer mode, including liquid fuel, oxidizer, electric charge, Kethane, and RCS fuel; but not resources such as solid rocket fuel.

This system does not consume power itself, but the vessel is required to have power and required to be controllable (have a probe core or at least one Kerbal on-board). Otherwise, everything is disabled.

### How to use

Open the GUI using the button along the screen edge. It defaults to the top right-hand corner, but can be dragged to anywhere along any edge.

Click the button at the end of a row, and:

* Highlight - highlights the part so you can find it.
* Edit - edit the amount in the part (only available Prelaunch or when Landed).
* Lock - prevents it from transferring the resource into or out of the part.
* In - transfers the resource into the part, taking an equal amount from each other part.
* Out - transfers the resource out of the part, putting an equal amount in each other part.
* Balance - transfers the resource around such that all parts being balanced are the same percentage full.
* Balance All - balances all parts that are not in one of the other modes (In, Out, Lock, etc).

Click the "S" button to bring up the settings menu.
Click the "?" button to bring up the help/about menu.
Click the "X" button to close the window.

### Settings

* Maximum Fuel Flow Rate - controls how quickly fuel is transferred around. This limits each action to only transfer up to the selected amount.
* Fuel Warning Level - warns (yellow) when a resource drops below this percentage of capacity.
* Fuel Warning Level - warns (red) when a resource drops below this percentage of capacity.
* Show <whatever> - toggles the display of the columns on the main window.
* Balance In's - when this is enabled, it will balance the resource level between parts that are set to In. Note that this can cause the resource level in a part to drop until it evens out with the other parts, then it will start increasing again.
* Balance Out's - when this is enabled, it will balance the resource level between parts that are set to Out. Note that this can cause the resource level in a part to rise until it evens out with the other parts, then it will start decreasing again.


## Installation

Detailed installation instructions are now on its own file (see the [In a Hurry](#in-a-hurry) section) and on the distribution file.

### License:

Released under [CC BY-NC-SA 3.0](https://creativecommons.org/licenses/by-nc-sa/3.0/). See [here](./LICENSE)

Please note the copyrights and trademarks in [NOTICE](./NOTICE)


## UPSTREAM

* [linuxgurugamer](https://forum.kerbalspaceprogram.com/index.php?/profile/129964-linuxgurugamer/) ROOT / Current Maintainer
	+ [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/186526-*)
	+ [SpaceDock](https://spacedock.info/mod/640/TacFuelBalancer)
	+ [GitHub](https://github.com/linuxgurugamer/TacFuelBalancer)
* [Z-Key Aerospace](https://forum.kerbalspaceprogram.com/index.php?/profile/138926-z-key-aerospace/)
	+ [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/139223-*)
	+ [Homepage](https://themoose.co.uk/ksp/downloads.html)
	+ [GitHub](https://github.com/thewebbooth/TacFuelBalancer)
* [Taranis Elsu](https://forum.kerbalspaceprogram.com/index.php?/profile/57742-taraniselsu/)
	+ [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/23808-*)
	+ [CurseForge](https://www.curse.com/ksp-mods/kerbal/221160-tac-fuel-balancer)
	+ [GitHub](https://github.com/thewebbooth/TacFuelBalancer)
