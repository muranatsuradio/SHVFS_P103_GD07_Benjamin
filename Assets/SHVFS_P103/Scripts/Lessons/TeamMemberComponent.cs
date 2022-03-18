using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMemberComponent : MonoBehaviour
{
    public int TeamID;
    public ZoneComponent ControllingZone;

    private void Awake()
    {
        TeamPointSystem.Instance.AddMember(this);
    }
    private void OnDestroy()
    {
        TeamPointSystem.Instance.RemoveMember(this);
        if (ControllingZone)
        {
            ControllingZone.RemoveMember(this);
            ControllingZone = null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.GetComponent<ZoneComponent>()) return;
        ControllingZone = other.GetComponent<ZoneComponent>();
        ControllingZone.AddMember(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (ControllingZone)
        {
            ControllingZone.RemoveMember(this);
        }
        ControllingZone = null;
    }
}
