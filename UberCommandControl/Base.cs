//****//
// THIS PROJECT IS PROVIDED UNDER NO COPYRIGHT OR PROTECTION BY UBER PVP @ FAGGOCHEAT.ORG
//
// SOME DATA HAS BEEN REDACTED OR CHANGED FOR PROTECTION PURPOSES BY FURTHER ADMINISTRATION
//
// "Hope you like it."
//        - Faggo Cheat Team
//****//

using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System.Collections.Generic;
using System.Linq;

namespace UberCommandControl
{
    public class Base : RocketPlugin
    {
        public static Base Instance;
        public static Utilities.TraceRay Tracer;
        public static Utilities.Math Math;
        public static Utilities.Messages Messages;
        public static List<Commands.Destruct.toDestroy> destroyList;

        //**1RN**//
        void FixedUpdate()
        {
            //**CBACK**//
            if (destroyList.Count > 0)
            {
                int destroyedThisTick = 0;
                for (int i = 0; i < destroyList.Count(); i++)
                {
                    if (destroyedThisTick++ > 2)
                        break;
                    else
                    {
                        Commands.Destruct.toDestroy Des = destroyList[i];
                        StructureManager.damage(Des.transform, Des.transform.position, ushort.MaxValue, 1, false);
                        destroyList.Remove(Des);
                        i = 0;
                    }
                }
            }
        }

        //**ONLD**//
        protected override void Load()
        {
            Logger.Log("Uber Commands Loaded\n----------------------------------");
            Logger.Log("Loading Uber Admin Controls");
            //
            Instance = this;
            Logger.Log("- Instance Set");

            Tracer = new Utilities.TraceRay();
            Logger.Log("- Tracer Set");

            Math = new Utilities.Math();
            Logger.Log("- FMath Set");

            Messages = new Utilities.Messages();
            Logger.Log("- CMessages Set");

            destroyList = new List<Commands.Destruct.toDestroy>();
            Logger.Log("- DestroyList Set");
            //
            Logger.Log("Uber Admin Control Loaded\n----------------------------------");
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList() {
                    {"not_enough_arguments","Not enough arguments"},
                    {"too_many_arguments", "Too many arguments"},
                    {"unidentified_argument", "Did not understand arguments"},
                    {"player_not_found", "Player was not found"},
                    {"descended","You have been descended {0} units"},
                    {"ascended","You have been ascended {0} units"},
                    {"rep","Gave {0} {1} reputation"},
                    {"feed", "Fed {0}" },
                    {"object_owned", "Object owned by: {0}" },
                    {"vehicle_owned", "Vehicle owned by: {0}" },
                    {"vehicle_no_owner", "This vehicle does not have an owner" },
                    {"repair_vehicle", "Repaired Vehicle" },
                    {"repair_object", "Repaired Object"},
                    {"no_owner_found", "Could not find an owner.." },
                    {"no_object_found", "No object found, try coming closer?" },
                    {"xp", "Set {0}'s experience to {1}" },
                    {"locked", "Locked storage unit owned by {0}" },
                    {"unlocked", "Unlocked storage unit" },
                    {"kill_animals", "Thou haft smitten thy animals" },
                    {"revive_animals", "The holy spirit has returned thy lives to animals." },
                    {"kill_zombies", "Zombies be gone." },
                    {"revive_zombies", "Zombies be.. Back?" },
                    {"alert_zombies", "Zombies be alerted to their nearest player" },
                    {"destruct_conf", "Set {0} object(s) to be destroyed, owned by {1}" },
                    {"refuel_vehicle", "Vehicle refueled" },
                    {"refuel_object", "Generator refueled" },
                    {"cuff", "Cuffed {0}" },
                    {"cuffall", "Cuffed errbody" },
                    {"uncuff", "Uncuffed {0}" },
                    {"uncuffall", "Uncuffed all users previously-cuffed by this command." },
                    {"toggledoor", "Toggled door/garage" },
                    {"reown", "Re-Own Confirmed" }
                };
            }
        }

        //**We too good to even need this**//
        protected override void Unload()
        {

        }
    }
}
