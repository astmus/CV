

namespace WpfClient.Models
{
    public class InfoDialogModel : BaseDialogModel
    {
        public static InfoDialogModel Info(string message) => new() { Message = message, CloseButtonText = "OK" };
    }
}
