using System;
using System.Collections.Generic;

namespace ContractLibrary.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateTime? Dob { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Avatar { get; set; }

    public bool? IsOnline { get; set; }
    public string FullName
    {

        get
        {
            return Firstname + " " + Lastname;
        }

    }

    public virtual ICollection<Message> MessageAccountIdAcceptNavigations { get; } = new List<Message>();

    public virtual ICollection<Message> MessageAccountIdSendNavigations { get; } = new List<Message>();

    public virtual ICollection<MessageGroup> MessageGroups { get; } = new List<MessageGroup>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
