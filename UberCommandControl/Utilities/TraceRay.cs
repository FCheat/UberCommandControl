using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;

/*Raycast-Traceray - United*/

namespace UberCommandControl.Utilities
{
    public class TraceRay
    {
        public enum SearchType
        {
            Barricade,
            Structure,
            Vehicle
        };

        public RaycastHit TraceToHit(UnturnedPlayer Player)
        {
            if (Physics.Raycast(Player.Player.look.aim.position, Player.Player.look.aim.forward, out RaycastHit RayT, 10, RayMasks.BARRICADE_INTERACT))
                return RayT;
            else
                return new RaycastHit();
        }
    }
}
