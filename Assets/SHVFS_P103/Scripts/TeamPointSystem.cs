using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamPointSystem : Singleton<TeamPointSystem>
{
    public List<Team> Teams = new List<Team>();
    public List<ZoneComponent> Zones = new List<ZoneComponent>();

    public int MaxTeamPoints = 10;
    public int TickPoint = 1;
    public float TickTime = 1.0f;
    private float m_curTime = 0.0f;

    public override void Awake()
    {
        base.Awake();

        foreach (var member in FindObjectsOfType<TeamMemberComponent>())
        {
            AddMember(member);
        }
        foreach (var zone in FindObjectsOfType<ZoneComponent>())
        {
            Zones.Add(zone);
        }
    }
    private void Update()
    {
        if (m_curTime >= TickTime)
        {
            AssignPoints();
            m_curTime = .0f;
        }
        m_curTime += Time.deltaTime;
    }
    public void RemoveMember(TeamMemberComponent member)
    {
        var team = Teams.Find(team => team.ID == member.TeamID);
        if (team == null) return;
        if (team.Members.Contains(member))
        {
            team.Members.Remove(member);
        }
    }
    public void AddMember(TeamMemberComponent member)
    {
        if (!Teams.Any(team => team.ID.Equals(member.TeamID)))
        {
            var team = new Team(member.TeamID, new List<TeamMemberComponent> { member });
            Teams.Add(team);
        }
        else
        {
            var team = Teams.FirstOrDefault(team => team.ID.Equals(member.TeamID));
            if (team.Members.Contains(member)) return;
            team.Members.Add(member);
        }
    }
    public void AcquireTeamPoints(int points, int teamNum)
    {
        var team = Teams.Find(team => team.ID == teamNum);

        if (team.Point < MaxTeamPoints)
        {
            team.Point += points;
        }
        //Debug.Log($"Team : {team.ID}  || Points : {team.Point}");
    }
    private void AssignPoints()
    {
        foreach (var zone in Zones)
        {
            if (zone.BelongToTeam != null)
            {
                AcquireTeamPoints(TickPoint, zone.BelongToTeam.ID);
            }
        }
    }
}
[System.Serializable]
public class Team
{
    public int ID;
    public int Point = 0;
    public List<TeamMemberComponent> Members;
    public Team(int id, List<TeamMemberComponent> members)
    {
        ID = id;
        Members = members;
    }
    public Team(int id, TeamMemberComponent member)
    {
        ID = id;
        Members.Add(member);
    }
}


