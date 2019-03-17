using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsEvolution : IAbility {
    //능력 오프는 스크립트 enable false로 대체한다..
    public override void AbilityOn()
    {
        GameManager.Instance.player.maxEnergyLimit = (int)(GameManager.Instance.player.maxEnergyLimit * 1.5);
        GameManager.Instance.player.energyRecoverySpeed *= 2;
    }

    public override void AbilityOff()
    {
        GameManager.Instance.player.maxEnergyLimit = (int)(GameManager.Instance.player.maxEnergyLimit / 1.5);
        GameManager.Instance.player.energyRecoverySpeed /= 2;
    }
}
