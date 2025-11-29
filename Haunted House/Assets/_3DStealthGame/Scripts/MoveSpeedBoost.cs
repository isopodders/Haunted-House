using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBoost : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    private bool boostOn = false;
    public float baseSprint = 5f;
    public float speedDuration = 3f;
    public float cooldownDuration = 3f;
    public bool onCooldown = false;

    void Start()
    {
        //gets the players rigid body
        playerRigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        playerRigidBody.freezeRotation = true;
        //Use the speed on shift during the boost. 
        if (Input.GetKey(KeyCode.LeftShift)){
            if (boostOn)
            {
                playerRigidBody.linearVelocity = transform.forward * baseSprint;

            }
        }

        //enables the boost for a set duration of(speedDuration)
        if (Input.GetKey(KeyCode.B) && onCooldown == false)
            {
                if (!boostOn)
                {
                    Debug.Log("boost start");
                    FindObjectOfType<UiManager>().UpdateBoostUi(1);
                    boostOn = true;
                    Invoke("EndBoost", speedDuration);
                }
            }
    }
    //ends the speedboost and updates ui
    private void EndBoost()
    {
        boostOn = false;
        onCooldown = true;
        FindObjectOfType<UiManager>().UpdateBoostUi(-2);
        Debug.Log("boost end");
        Invoke("EndCooldown", cooldownDuration);
    }
    //resets the cooldown and updates ui
    private void EndCooldown()
    {
        FindObjectOfType<UiManager>().UpdateBoostUi(1);
        onCooldown = false;
    }
}
