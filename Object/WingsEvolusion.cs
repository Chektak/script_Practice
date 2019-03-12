using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsEvolution : IBuff {
    override void BuffEffectStart(){
          GameManager.Instance.player.                          DashCoolTimeLimit/=2;
          //대쉬쿨타임 1/2특성
    }
    override void BuffEffectRelease(){
          DashCoolTimeLimit/=2;
          //대쉬쿨타임 정상화
    }
}
