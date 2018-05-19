using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

namespace UberCommandControl.Commands
{
    public class XP : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Name
        {
            get { return "xp"; }
        }

        public string Help
        {
            get { return "Set a players XP"; }
        }

        public string Syntax
        {
            get { return "/xp <player> <xp>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Count() < 2 || command[0] == "")
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNotEnoughArgs, caller);
                return;
            }
            if (command.Count() < 3)
            {
                UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
                if (target == null)
                {
                    Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CPlayerNotFound, caller);
                    return;
                }
                target.Experience = uint.Parse(command[1], CultureInfo.InvariantCulture);
                UnturnedChat.Say(caller, Base.Instance.Translate("xp", command[0], command[1]));
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
                  "uber.xp"
                };
            }
        }
    }
}
