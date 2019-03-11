using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage{
    
    void GiveDamage();
    IEnumerator DamageInterval(float interval);
}
