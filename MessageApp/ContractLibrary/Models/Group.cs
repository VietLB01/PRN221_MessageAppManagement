using System;
using System.Collections.Generic;

namespace ContractLibrary.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string? GroupName { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<MessageGroup> MessageGroups { get; } = new List<MessageGroup>();

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
