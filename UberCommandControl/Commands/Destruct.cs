using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.Unturned.Chat;
using System.Globalization;
using UnityEngine;
using SDG.Unturned;

namespace UberCommandControl.Commands
{
    public class Destruct : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Name
        {
            get { return "destruct"; }
        }

        public string Help
        {
            get { return "Destroy structures based on SteamID"; }
        }

        public string Syntax
        {
            get { return "/destruct <SteamID>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public class toDestroy
        {
            public Transform transform;
            public ulong Owner;
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

            //**NUM_SE_REP**//
            if (!command[0].Contains("0") && !command[0].Contains("1") && !command[0].Contains("2") && !command[0].Contains("3") && !command[0].Contains("4") && !command[0].Contains("5") && !command[0].Contains("6") && !command[0].Contains("7") && !command[0].Contains("8") && !command[0].Contains("9"))
            {
                Base.Messages.CommonMessage(Utilities.Messages.CommonMessages.CInvalidArgs, caller);
                return;
            }

            ulong owner = ulong.Parse(command[0], CultureInfo.InvariantCulture);
            Base.destroyList.Clear();

            foreach (StructureRegion region in StructureManager.regions)
                for (int i = 0; i < region.drops.Count; i++)
                    if (region.structures[i].owner == owner)
                        Base.destroyList.Add(new toDestroy() { transform = region.drops[i].model, Owner = owner });

            UnturnedChat.Say(caller, Base.Instance.Translate("destruct_conf", Base.destroyList.Count.ToString(), owner.ToString()));
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.destruct"
                };
            }
        }
    }
}
