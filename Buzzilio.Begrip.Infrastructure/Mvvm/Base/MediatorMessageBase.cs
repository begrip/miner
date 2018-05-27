using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;

namespace Buzzilio.Begrip.Infrastructure.Mvvm.Base
{
    public class MediatorMessageBase: MessageBase
    {
       public MessagePurpose Purpose { get; set; }
       public object Payload { get; set; }
    }
}
