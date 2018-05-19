using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using Steamworks;
using SDG.Unturned;

namespace UberCommandControl.Commands
{
    public class Owner : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "getowner"; }
        }

        public string Help
        {
            get { return "Find the owner of an object/vehicle"; }
        }

        public string Syntax
        {
            get { return "/getowner"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer Player = (UnturnedPlayer)caller;
            Transform trans = Base.Tracer.TraceToHit(Player).transform;
            if (trans == null)
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNoObjectFound, caller);
                return;
            }
            InteractableVehicle vehicle = trans.gameObject.GetComponent<InteractableVehicle>();
            if (vehicle != null)
            {
                if (vehicle.lockedOwner != CSteamID.Nil)
                    UnturnedChat.Say(caller, Base.Instance.Translate("vehicle_owned", vehicle.lockedOwner.ToString()));
                else
                    UnturnedChat.Say(caller, Base.Instance.Translate("vehicle_no_owner"));
            }
            else
            {
                if (trans.GetComponent<InteractableDoorHinge>() != null)
                    trans = trans.parent.parent;

                if (BarricadeManager.tryGetInfo(trans, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion BarricRegion))
                {
                    UnturnedChat.Say(caller, Base.Instance.Translate("object_owned", (CSteamID)BarricRegion.barricades[index].owner));
                    return;
                }
                if (StructureManager.tryGetInfo(trans, out x, out y, out index, out StructureRegion StructRegion))
                {
                    UnturnedChat.Say(caller, Base.Instance.Translate("object_owned", (CSteamID)StructRegion.structures[index].owner));
                    return;
                }
                UnturnedChat.Say(caller, Base.Instance.Translate("no_owner_found"));
            }

        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.getowner"
                };
            }
        }
    }
}