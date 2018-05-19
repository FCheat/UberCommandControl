using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

namespace UberCommandControl.Commands
{
    public class Feed : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Name
        {
            get { return "feed"; }
        }

        public string Help
        {
            get { return "Refill food & water for a player"; }
        }

        public string Syntax
        {
            get { return "/feed <playername>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Count() == 0 || command[0] == "")
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNotEnoughArgs, caller);
                return;
            }
            if (command.Count() < 2)
            {
                UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
                if (target == null)
                {
                    Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CPlayerNotFound, caller);
                    return;
                }
                target.Hunger = 0;
                target.Thirst = 0;
                UnturnedChat.Say(caller, Base.Instance.Translate("feed", command[0]));
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
                  "uber.feed"
                };
            }
        }
    }
}
