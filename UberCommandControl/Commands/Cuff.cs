using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using UberCommandControl.Utilities;

namespace UberCommandControl.Commands
{
    public class Cuff : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "cuff"; }
        }

        public string Help
        {
            get { return "Cuff player(s)"; }
        }

        public string Syntax
        {
            get { return "/cuff <player>/all"; }
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
                Base.Messages.CommonMessage(Messages.CommonMessages.CNotEnoughArgs, caller);
                return;
            }
            if (command.Count() == 1)
                if (command[0] != "all")
                {
                    UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
                    if (target == null)
                    {
                        Base.Messages.CommonMessage(Messages.CommonMessages.CPlayerNotFound, caller);
                        return;
                    }

                    if (target.Player.life.health < 1)
                        return;

                    CuffPlayer(target.Player, (Player.IsAdmin) ? Player.CSteamID : new Steamworks.CSteamID(12));
                    UnturnedChat.Say(caller, Base.Instance.Translate("cuff", target.DisplayName), UnityEngine.Color.white);
                }
                else
                {
                    foreach (SDG.Unturned.SteamPlayer play in SDG.Unturned.Provider.clients)
                        if (play.player.life.health > 0)
                            if (play.player != Player.Player)
                                CuffPlayer(play.player, (Player.IsAdmin) ? Player.CSteamID : new Steamworks.CSteamID(12));

                    UnturnedChat.Say(caller, Base.Instance.Translate("cuffall"), UnityEngine.Color.white);
                }
            else
                Base.Messages.CommonMessage(Messages.CommonMessages.CTooManyArgs, caller);
        }

        void CuffPlayer(SDG.Unturned.Player player, Steamworks.CSteamID Owner)
        {
            player.animator.captorID = Owner;
            player.animator.captorItem = 1197; //**Handcuff**//
            player.animator.captorStrength = ushort.MaxValue;
            player.animator.sendGesture(SDG.Unturned.EPlayerGesture.ARREST_START, true);
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.cuff"
                };
            }
        }
    }
}
