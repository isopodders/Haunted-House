using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //destroys the particle spawner soon after.
        Invoke("Destroy", 3f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something: " + other.gameObject.name);
        // Check if the object we hit has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an Enemy! Destroying...");
            // Destroy the enemy object
            Destroy(other.gameObject);


        }
    }
}