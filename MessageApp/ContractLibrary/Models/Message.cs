using System;
using System.Collections.Generic;

namespace ContractLibrary.Models;

public partial class Message
{
    public int Id { get; set; }

    public int? AccountIdSend { get; set; }

    public int? AccountIdAccept { get; set; }

    public string? Content { get; set; }

    public DateTime? Time { get; set; }

    public virtual Account? AccountIdAcceptNavigation { get; set; }

    public virtual Account? AccountIdSendNavigation { get; set; }
}
