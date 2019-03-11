using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff{//슬로우, 출혈 등이 버프에 해당된다.
    void BuffEffect();
    IEnumerator BuffDuration();
}
