using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpearScript : MonoBehaviour
{
    private Rigidbody _myBody;

    public float speed = 30f;
    public float deactivateTimer = 3f;
    public float damage = 50f;

    private void Awake()
    {
        _myBody = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {
        // invoke activates method by name after given time in seconds
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        // collision w/ enemy = deactivate the arrow/spear
        if (target.tag == Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);
        }

    }
    public void Launch(Camera mainCamera)
    {
        //myBody.velocity = Camera.main.transform.forward * speed;
        _myBody.velocity = mainCamera.transform.forward * speed; // move foward from main camera
        // velocity adds forwards momentum faster than using AddForce physics method
        transform.LookAt(transform.position + _myBody.velocity); // rotates gameObject to align correctly forward
                                                                // & then move it with the velocity on rigidbody

    }

}
