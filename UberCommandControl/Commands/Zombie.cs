using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Chat;
using UnityEngine;
using Rocket.Unturned.Player;
using Object = UnityEngine.Object;

namespace UberCommandControl.Commands
{
    public class Zombie : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Name
        {
            get { return "zombies"; }
        }

        public string Help
        {
            get { return "Kill/Revive zombies across the map"; }
        }

        public string Syntax
        {
            get { return "/zombies respawn/kill/alert"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Count() < 1)
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CNotEnoughArgs, caller);
                return;
            }
            else if (command.Count() > 1)
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CTooManyArgs, caller);
                return;
            }

            bool kill = false, revive = false, agro = false;
            if (command[0] == "kill")
            {
                kill = true;
                UnturnedChat.Say(caller, Base.Instance.Translate("kill_zombies"));
            }
            else if (command[0] == "respawn")
            {
                revive = true;
                UnturnedChat.Say(caller, Base.Instance.Translate("revive_zombies"));
            }
            else if (command[0] == "alert")
            {
                agro = true;
                UnturnedChat.Say(caller, Base.Instance.Translate("alert_zombies"));
            }
            else
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CInvalidArgs, caller);
                return;
            }

            foreach (SDG.Unturned.Zombie i in Zombies)
            {
                if (kill)
                    SDG.Unturned.ZombieManager.sendZombieDead(i, Vector3.zero);
                if (revive)
                    SDG.Unturned.ZombieManager.sendZombieAlive(i, i.type, (byte)i.speciality, i.shirt, i.pants, i.hat, i.gear, i.transform.position, 0);
                if (agro)
                {
                    UnturnedPlayer bestPlayer = null;
                    float bestDistance = 9999f;
                    foreach (SDG.Unturned.SteamPlayer p in SDG.Unturned.Provider.clients)
                    {
                        UnturnedPlayer Player = UnturnedPlayer.FromSteamPlayer(p);
                        float dist = Base.Math.VectorDistance(Player.Position, i.transform.position);
                        if (dist < 30f)
                        {
                            bestPlayer = Player;
                            break;
                        }
                        else if (dist < bestDistance)
                        {
                            bestDistance = dist;
                            bestPlayer = Player;
                        }
                    }
                    if (bestPlayer != null)
                        i.alert(bestPlayer.Player);
                }
            }
        }

        public static List<SDG.Unturned.Zombie> Zombies =>
        Object.FindObjectsOfType<SDG.Unturned.Zombie>()?.ToList() ?? new List<SDG.Unturned.Zombie>(0);

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.zombies"
                };
            }
        }
    }
}
