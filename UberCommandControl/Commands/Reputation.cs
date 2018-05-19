using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

//TODO:
// - Math for actual subtraction/addition

namespace UberCommandControl.Commands
{
    public class Reputation : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Name
        {
            get { return "rep"; }
        }

        public string Help
        {
            get { return "Add/Subtract reputation for a given user"; }
        }

        public string Syntax
        {
            get { return "/rep <playername> <amount>"; }
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
                target.Reputation = int.Parse(command[1], CultureInfo.InvariantCulture);
                UnturnedChat.Say(caller, Base.Instance.Translate("rep", command[0], command[1]));
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
                  "uber.rep"
                };
            }
        }
    }
}
