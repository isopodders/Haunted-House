using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private List<string> m_OwnedKeys = new List<string>();
    Animator m_Animator;
    AudioSource m_AudioSource;

    public InputAction MoveAction;

    public GameObject attackPrefab;
    private bool usedAttack = false;
    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveAction.Enable();
        m_AudioSource = GetComponent<AudioSource> ();
    }

    void FixedUpdate()
    {
        Shooting();
        var pos = MoveAction.ReadValue<Vector2>();

        float horizontal = pos.x;
        float vertical = pos.y;


        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * walkSpeed * Time.deltaTime);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Play();
        }
    }
    public void AddKey(string keyName)
    {
        m_OwnedKeys.Add(keyName);
    }
    public bool OwnKey(string keyName)
    {
        return m_OwnedKeys.Contains(keyName);
    }
    //Used shooting code from fighter plane, will only allow it once. 
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && usedAttack == false)
        {
            Instantiate(attackPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            usedAttack = true;
        }
    }

}