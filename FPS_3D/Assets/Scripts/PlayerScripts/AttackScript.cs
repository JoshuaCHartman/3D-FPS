using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 2;
    public float radius = 1f;
    public LayerMask layerMask;

    void Update()
    {
        // test for hit / contact
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask); 
        // layer mask set to "enemy" so collisions only detected between attack point and objects on enemy layer
        // all overlap of sphere at attack point with enemy results in array of overlap points
        if(hits.Length > 0)
        {
            print("Hit : " + hits[0].gameObject.tag);
            gameObject.SetActive(false);
        }
    }
}
