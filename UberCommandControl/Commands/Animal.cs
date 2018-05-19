using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Chat;
using UnityEngine;

namespace UberCommandControl.Commands
{
    public class Animal : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Name
        {
            get { return "animals"; }
        }

        public string Help
        {
            get { return "Kill/Revive animals across the map"; }
        }

        public string Syntax
        {
            get { return "/animals respawn/kill"; }
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

            bool kill = false;
            bool revive = false;
            if (command[0] == "kill")
            {
                kill = true;
                UnturnedChat.Say(caller, Base.Instance.Translate("kill_animals"));
            }
            else if (command[0] == "respawn")
            {
                revive = true;
                UnturnedChat.Say(caller, Base.Instance.Translate("revive_animals"));
            }
            else
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CInvalidArgs, caller);
                return;
            }

            foreach (SDG.Unturned.Animal i in SDG.Unturned.AnimalManager.animals)
            {
                if (kill)
                    SDG.Unturned.AnimalManager.sendAnimalDead(i, Vector3.zero);
                if (revive)
                    i.sendRevive(i.transform.position, 0.25f);
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.animals"
                };
            }
        }
    }
}
