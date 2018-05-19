using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;

namespace UberCommandControl.Commands
{
    public class Unlockit : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "unlockit"; }
        }

        public string Help
        {
            get { return "Unock a storage unit that you're looking at"; }
        }

        public string Syntax
        {
            get { return "/unlockit"; }
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
                return;
            if (trans.GetComponent<InteractableDoorHinge>() != null)
                return;
            if (BarricadeManager.tryGetInfo(trans, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion BarricRegion))
            {
                UnturnedChat.Say(caller, Base.Instance.Translate("unlocked"));
                ItemStorageAsset itemstore = (ItemStorageAsset)BarricRegion.barricades[index].barricade.asset;
                InteractableStorage IStorage = (InteractableStorage)BarricRegion.drops[index].interactable;
                IStorage.isOpen = false;
                IStorage.enabled = true;
                IStorage.opener = null;
                IStorage.use();
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.unlockit"
                };
            }
        }
    }
}
