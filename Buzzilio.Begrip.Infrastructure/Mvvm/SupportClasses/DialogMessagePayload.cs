using System;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.SupportClasses
{
    public class DialogPromptSettings
    {
        public bool DialogCancellable { get; set; }
        public string DialogMessage { get; set; }
        public Uri DialogImage { get; set; }
    }
}
