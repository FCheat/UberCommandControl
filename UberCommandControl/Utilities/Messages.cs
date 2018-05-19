using Rocket.API;
using Rocket.Unturned.Chat;

namespace UberCommandControl.Utilities
{
    public class Messages
    {
        public enum CommonMessages
        {
            CPlayerNotFound,
            CInvalidArgs,
            CTooManyArgs,
            CNotEnoughArgs,
            CNoObjectFound
        }

        public void CommonMessage(CommonMessages type, IRocketPlayer Caller)
        {
            string message = "";
            switch (type)
            {
                case CommonMessages.CPlayerNotFound:
                    message = Base.Instance.Translate("player_not_found");
                    break;
                case CommonMessages.CInvalidArgs:
                    message = Base.Instance.Translate("unidentified_argument");
                    break;
                case CommonMessages.CTooManyArgs:
                    message = Base.Instance.Translate("too_many_arguments");
                    break;
                case CommonMessages.CNotEnoughArgs:
                    message = Base.Instance.Translate("not_enough_arguments");
                    break;
                case CommonMessages.CNoObjectFound:
                    message = Base.Instance.Translate("no_object_found");
                    break;
                default:
                    message = "error";
                    break;
            }
            UnturnedChat.Say(Caller, message);
        }
    }
}
