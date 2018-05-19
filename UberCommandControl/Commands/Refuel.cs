using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;

namespace UberCommandControl.Commands
{
    public class Refuel : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "refuel"; }
        }

        public string Help
        {
            get { return "Refuel the vehicle/generator that you're looking at"; }
        }

        public string Syntax
        {
            get { return "/refuel"; }
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
                vehicle.askFillFuel(10000);
                UnturnedChat.Say(caller, Base.Instance.Translate("refuel_vehicle"));
            }
            else
            {
                if (trans.GetComponent<InteractableDoorHinge>() != null)
                    trans = trans.parent.parent;

                if (BarricadeManager.tryGetInfo(trans, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion BarricRegion))
                {
                    if (trans.name == "458" || trans.name == "1230")
                    {
                        InteractableGenerator i = trans.GetComponent<InteractableGenerator>();
                        BarricadeManager.sendFuel(trans, i.capacity);
                        UnturnedChat.Say(caller, Base.Instance.Translate("refuel_object"));
                    }
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
                  "uber.refuel"
                };
            }
        }
    }
}
