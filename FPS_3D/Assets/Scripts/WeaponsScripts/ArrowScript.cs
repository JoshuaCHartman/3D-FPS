using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody myBody;

    public float speed = 30f;
    public float deactivateTimer = 3f;
    public float damage = 15f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();


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
        // collision w/ enemy = deactivate

    }
    public void Launch()
    {
        myBody.velocity = Camera.main.transform.forward * speed;
    }

}
