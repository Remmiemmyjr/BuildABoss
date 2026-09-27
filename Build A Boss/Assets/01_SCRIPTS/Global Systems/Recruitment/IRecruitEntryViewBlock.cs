using System;
using UnityEngine;

public interface IRecruitEntryViewBlock
{
    // When given a minion, show up on the list.   Bind an event when this entry block is selected, if desired
    void Bind(MinionProfileInstance minion, Action<MinionProfileInstance> onSelected);
    // If the recruit needs to display as unavailable/unselectable
    void SetAvailable(bool available);
}
