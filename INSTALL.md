# TAC Fuel Balancer /L Unofficial

A Fuel Balancer from Thunder Aerospace Corporation (TAC), designed by Taranis Elsu. Unofficial fork by Lisias.


## Installation Instructions

To install, place the GameData folder inside your Kerbal Space Program folder. Optionally, you can also do the same for the PluginData (be careful to do not overwrite your custom settings):

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
	+ Delete `<KSP_ROOT>/GameData/TacFuelBalancer`
* Extract the package's `GameData` folder into your KSP's root:
	+ `<PACKAGE>/GameData` --> `<KSP_ROOT>/GameData`
* Extract the package's `PluginData` folder (if available) into your KSP's root, taking precautions to do not overwrite your custom settings if this is not what you want to.
	+ `<PACKAGE>/PluginData` --> `<KSP_ROOT>/PluginData`
	+ You can safely ignore this step if you already had installed it previously and didn't deleted your custom configurable files.

The following file layout must be present after installation:

```
<KSP_ROOT>
	[GameData]
		[net.lisias.ksp]
			[TacFuelBalancer]
				[PluginData]
					[Icons]
						close.png
						...
				CHANGE_LOG.md
				LICENSE
				NOTICE
				TacFuelBalancer.dll
				...
		000_KSPe.dll
		...
	[PluginData]
		[net.lisias.ksp]
			[TacFuelBalancer] <not present until you run it for the fist time>
				FuelBalancer.cfg 
	KSP.log
	PartDatabase.cfg
	...
```


### Dependencies

* [KSP API Extensions/L](https://github.com/net-lisias-ksp/KSPAPIExtensions)
	+ Hard Dependency - Plugin will not work without it.
	+ Not Included
* [Toolbar Controller](https://github.com/net-lisias-kspu/ToolbarControl)
	+ Hard Dependency - Plugin will not work without it.
	+ Not Included


