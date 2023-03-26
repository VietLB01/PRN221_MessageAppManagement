using System;
using System.Collections.Generic;

namespace ContractLibrary.Models;

public partial class MessageGroup
{
    public int AccountIdsend { get; set; }

    public int GroupIdaccept { get; set; }

    public string? Content { get; set; }

    public DateTime? Time { get; set; }

    public virtual Account AccountIdsendNavigation { get; set; } = null!;

    public virtual Group GroupIdacceptNavigation { get; set; } = null!;
}
