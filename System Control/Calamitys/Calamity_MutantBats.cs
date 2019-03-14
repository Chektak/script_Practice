using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calamity_MutantBats : ICalamity {
    public MutantBat[] bats;
    public override void CalamityStart()
    {
        for(int i=0; i<bats.Length; i++)
        {
            bats[i].gameObject.SetActive(true);
            bats[i].transform.position = bats[i].spown_Cave.transform.position;
        }
    }

    public override void CalamityRelese()
    {
        for (int i = 0; i < bats.Length; i++)
        {
            bats[i].gameObject.SetActive(false);
        }
    }

}
