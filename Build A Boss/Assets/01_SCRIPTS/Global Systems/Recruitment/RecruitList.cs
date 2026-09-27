using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RecruitList 
{
    // do i want a list or a dictionary?
    public List<MinionProfileInstance> Recruits;

    public RecruitList()
    {
        Recruits = new List<MinionProfileInstance>();
    }
}
