using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;

namespace UberCommandControl.Commands
{
    public class Repair : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "repair"; }
        }

        public string Help
        {
            get { return "Repair an object/structure that you're looking at"; }
        }

        public string Syntax
        {
            get { return "/repair"; }
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
                vehicle.askRepair(10000);
                UnturnedChat.Say(caller, Base.Instance.Translate("repair_vehicle"));
            }
            else
            {
                if (trans.GetComponent<InteractableDoorHinge>() != null)
                    trans = trans.parent.parent;

                if (BarricadeManager.tryGetInfo(trans, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion BarricRegion))
                {
                    BarricadeManager.repair(trans, 10000, 1);
                    UnturnedChat.Say(caller, Base.Instance.Translate("repair_object"));
                    return;
                }
                if (StructureManager.tryGetInfo(trans, out x, out y, out index, out StructureRegion StructRegion))
                {
                    StructureManager.repair(trans, 10000, 1);
                    UnturnedChat.Say(caller, Base.Instance.Translate("repair_object"));
                    return;
                }
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNoObjectFound, caller);
            }

        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.repair"
                };
            }
        }
    }
}
