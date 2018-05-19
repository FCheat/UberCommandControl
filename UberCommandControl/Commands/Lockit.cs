using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using Steamworks;
using SDG.Unturned;

namespace UberCommandControl.Commands
{
    public class Lockit : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "lockit"; }
        }

        public string Help
        {
            get { return "Lock a storage unit that you're looking at"; }
        }

        public string Syntax
        {
            get { return "/lockit"; }
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
                ItemStorageAsset itemstore = (ItemStorageAsset)BarricRegion.barricades[index].barricade.asset;
                InteractableStorage IStorage = (InteractableStorage)BarricRegion.drops[index].interactable;
                IStorage.isOpen = true;
                IStorage.enabled = true;
                IStorage.opener = Player.Player;
                IStorage.use();
                UnturnedChat.Say(caller, Base.Instance.Translate("locked", (CSteamID)BarricRegion.barricades[index].owner));
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.lockit"
                };
            }
        }
    }
}
