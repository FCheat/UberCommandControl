using System.Collections.Generic;
using System.Globalization;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UnityEngine;

namespace UberCommandControl.Commands
{
    public class Ascend : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "ascend"; }
        }

        public string Help
        {
            get { return "Teleport up X amount of units"; }
        }

        public string Syntax
        {
            get { return "/ascend <units>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer Player = (UnturnedPlayer)caller;
            string filter = string.Join(".", command);
            if (filter == "")
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNotEnoughArgs, caller);
                return;
            }
            if (!filter.Contains("."))
            {
                Player.Teleport(new Vector3(Player.Position.x, Player.Position.y + float.Parse(filter, CultureInfo.InvariantCulture), Player.Position.z), Player.Rotation);
                UnturnedChat.Say(caller, Base.Instance.Translate("ascended", filter));
            }
            else
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CTooManyArgs, caller);
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.ascend"
                };
            }
        }
    }
}
