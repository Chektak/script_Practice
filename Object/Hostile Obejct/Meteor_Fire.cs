using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Fire : MonoBehaviour, IDamage {
    private bool canAttack = true;

    public void GiveDamage()
    {
        GameManager.Instance.player.Hp -=1;
    }

    public IEnumerator DamageInterval(float interval)
    {
        float time = 0;
        while (time < interval)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
        yield break;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && canAttack == true)
        {
            GiveDamage();
            canAttack = false;
            StartCoroutine(DamageInterval(0.2f));
        }
    }
}
