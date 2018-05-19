using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

namespace UberCommandControl.Commands
{
    public class Uncuff : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "uncuff"; }
        }

        public string Help
        {
            get { return "unCuff player(s) that have been previously cuffed by the command."; }
        }

        public string Syntax
        {
            get { return "/uncuff <player>/all"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer Player = (UnturnedPlayer)caller;
            if (command.Count() < 1 || command[0] == "")
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNotEnoughArgs, caller);
                return;
            }
            if (command.Count() == 1)
                if (command[0] != "all")
                {
                    UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
                    if (target == null)
                    {
                        Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CPlayerNotFound, caller);
                        return;
                    }
                    if (target.Player.life.health < 1)
                        return;
                    UncuffPlayer(target.Player, false);
                    UnturnedChat.Say(caller, Base.Instance.Translate("uncuff", target.DisplayName), UnityEngine.Color.white);
                }
                else
                {
                    foreach (SDG.Unturned.SteamPlayer play in SDG.Unturned.Provider.clients)
                        if (play.player.life.health > 0)
                            UncuffPlayer(play.player, true);
                    UnturnedChat.Say(caller, Base.Instance.Translate("uncuffall"), UnityEngine.Color.white);
                }
            else
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CTooManyArgs, caller);
        }

        void UncuffPlayer(SDG.Unturned.Player player, bool CheckStrength)
        {
            if (CheckStrength)
                if (player.animator.captorStrength != ushort.MaxValue)
                    return;

            player.animator.captorID = Steamworks.CSteamID.Nil;
            player.animator.captorStrength = 0;
            player.animator.sendGesture(SDG.Unturned.EPlayerGesture.ARREST_STOP, true);
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.uncuff"
                };
            }
        }
    }
}
