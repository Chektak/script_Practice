using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calamity_MeteorStrike : ICalamity {
    public Meteor[] meteors;

	public override void CalamityStart()
    {
        foreach(Meteor meteor in meteors)
        {
            meteor.gameObject.SetActive(true);
        }
    }
    public override void CalamityRelese()
    {
        foreach (Meteor meteor in meteors)
        {
            meteor.gameObject.SetActive(false);
        }
    }

}
