using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsEvolution : IAbility {
    //�ɷ� ������ ��ũ��Ʈ enable false�� ��ü�Ѵ�.
    public override void AbilityOn()
    {
        GameManager.Instance.player.maxDashCoolTimeLimit /= 2;
    }

    public override void AbilityOff()
    {
        GameManager.Instance.player.maxDashCoolTimeLimit /= 2;
    }
}
