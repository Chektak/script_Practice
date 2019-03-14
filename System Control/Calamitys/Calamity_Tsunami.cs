using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calamity_Tsunami : ICalamity {
    public Tsunami tsunami;
    
    public override void CalamityStart()
    {
        tsunami.gameObject.SetActive(true);
    }
    public override void CalamityRelese()
    {
        tsunami.gameObject.SetActive(false);
    }
}
