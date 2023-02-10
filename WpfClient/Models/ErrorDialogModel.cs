using System;
using System.IO;
using System.Media;
using System.Reflection;

namespace WpfClient.Models
{
    public class ErrorDialogModel : BaseDialogModel
    {

        public string ApplyButtonText { get; init; }

        public Action ApplyCallback { get; init; }

        public static ErrorDialogModel Error(string message, Action applyCallback = null)
        {
            return new() { Message = message, ApplyCallback = applyCallback, CloseButtonText = "OK" };
        }
    }
}
