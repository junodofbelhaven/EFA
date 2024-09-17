using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("playerCatman"))
        {
            Debug.Log("hit " + collision.gameObject.name + "!");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hit a wall");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);

    }

}
