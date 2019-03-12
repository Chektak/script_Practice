using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBuff:Monobehaviour {//슬로우, 출혈 등이 버프에 해당된다.
    abstract void BuffEffectStart();
    abstract void BuffEffectRelease();
}
