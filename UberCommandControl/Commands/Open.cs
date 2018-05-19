using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;
using SDG.Unturned;

namespace UberCommandControl.Commands
{
    public class Open : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "open"; }
        }

        public string Help
        {
            get { return "Toggle a door/storage unit that you're looking at"; }
        }

        public string Syntax
        {
            get { return "/open"; }
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
            if (trans.GetComponent<InteractableDoorHinge>() != null)
            {
                InteractableDoorHinge Hinge = trans.GetComponent<InteractableDoorHinge>();
                Hinge.door.updateToggle(Hinge.door.isOpen ? false : true); //**This sets the state to open/closed, but DOES NOT animate it.**//
                Hinge.enabled = true;
                UnturnedChat.Say(caller, Base.Instance.Translate("toggledoor"));
                return;
            }
            if (BarricadeManager.tryGetInfo(trans, out byte x, out byte y, out ushort plant, out ushort index, out BarricadeRegion BarricRegion))
            {
                ItemStorageAsset itemstore = (ItemStorageAsset)BarricRegion.barricades[index].barricade.asset;
                InteractableStorage IStorage = (InteractableStorage)BarricRegion.drops[index].interactable;
                IStorage.isOpen = true;
                IStorage.opener = Player.Player;
                Player.Inventory.isStoring = true;
                Player.Inventory.storage = IStorage;
                Player.Inventory.updateItems(PlayerInventory.STORAGE, IStorage.items);
                Player.Inventory.sendStorage();
                return;
            }
            Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNoObjectFound, caller);
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.fopen"
                };
            }
        }
    }
}
