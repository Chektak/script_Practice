using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBuff:MonoBehaviour {//슬로우, 출혈 등이 버프에 해당된다.
    public abstract void BuffOn(float duration);
    public abstract void BuffOff();
}
