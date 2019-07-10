
using ToolbarControl_NS;
using UnityEngine;
namespace Tac
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {

        void Start()
        {
            ToolbarControl.RegisterMod(FuelBalanceController.MODID, FuelBalanceController.MODNAME);
        }
    }
}
