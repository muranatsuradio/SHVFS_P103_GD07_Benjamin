using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerComponent : MonoBehaviour
{
    //We want this to be self-contained
    //GLobally unique identifier;
    public AttackGUIDComponent AttackGUIDComponent;
    public Guid GUID;
    public GameObject Attacker;

    public float AttackActiveTime;
    private float m_attackActiveTimer;

    public float AttackPower;

    private void Start()
    {
        GUID = AttackGUIDComponent.GUID;
    }
    private void OnEnable()
    {
        Attacker.SetActive(false);
    }

    private void Update()
    {
        if (m_attackActiveTimer < .0f)
        {
            m_attackActiveTimer = .0f;
        }

        m_attackActiveTimer -= Time.deltaTime;
        Attacker.transform.localScale = Vector3.one * m_attackActiveTimer / AttackActiveTime;

        if (m_attackActiveTimer > .0f)
        {
            Attacker.SetActive(true);
            return;
        }

        Attacker.SetActive(false);

        if (!Input.GetMouseButtonDown(0)) return;

        m_attackActiveTimer = AttackActiveTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<AttackableComponent>()) return;
        if (other.GetComponent<AttackableComponent>().GUID == GUID) return;
        Debug.Log(other.name);

        other.GetComponent<Rigidbody>().AddForce((-transform.forward + transform.up) * AttackPower, ForceMode.Impulse);
        Debug.Log(other.GetComponent<Rigidbody>().velocity);
    }
}

