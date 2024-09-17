using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ates : MonoBehaviour
{
    public float mesafe; //merminin menzili

    public float sikmaAraligi;

    bool fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, forward, out hit, mesafe))
        {
            if (hit.distance <= mesafe && hit.collider.gameObject.tag == "playerCatman") 
            {
                if (fire == true && (Input.GetMouseButton(0)))
                {
                    Debug.Log("vurdun");
                    fire = false;
                }
            }
        }
    }
    IEnumerator firetime()
    {
        yield return new WaitForSeconds(sikmaAraligi);
        fire = true;
    }
}
