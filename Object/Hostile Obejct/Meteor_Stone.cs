using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Stone : MonoBehaviour, IDamage{
   
    private bool canAttack = true;

    public void GiveDamage()
    {
        if(canAttack==true)
            GameManager.Instance.player.Hp -= 50;
    }

    public IEnumerator DamageInterval(float interval)
    {
        while (interval<0)
        {
            interval -= Time.deltaTime;
            yield return null;
        }
        canAttack = true;
        yield break;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Damage!");
            GiveDamage();
            canAttack = false;
            StartCoroutine(DamageInterval(1f));
        }
    }

}
