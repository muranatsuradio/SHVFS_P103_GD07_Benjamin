using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneComponent : MonoBehaviour
{
    public int MinMembers = 2;

    public Team BelongToTeam;
    public List<Team> InZoneTeams;
    private void Update()
    {
        if (CanAcquirePoints())
        {
            BelongToTeam = InZoneTeams[0];
        }
        else BelongToTeam = null;
    }
    public bool CanAcquirePoints()
    {
        if (InZoneTeams == null) return false;
        if (InZoneTeams.Count > 1 || InZoneTeams.Any(team => team.Members.Count < MinMembers) || InZoneTeams.Count == 0) return false;
        return true;
    }
    public void AddMember(TeamMemberComponent member)
    {
        if (!InZoneTeams.Any(team => team.ID.Equals(member.TeamID)))
        {
            InZoneTeams.Add(new Team(member.TeamID, new List<TeamMemberComponent> { member }));
        }
        else
        {
            var team = InZoneTeams.FirstOrDefault(team => team.ID.Equals(member.TeamID));
            if (team.Members.Contains(member)) return;
            team.Members.Add(member);
        }
    }
    public void RemoveMember(TeamMemberComponent member)
    {
        var team = InZoneTeams.Find(team => team.ID == member.TeamID);
        if (team == null) return;
        if (team.Members.Contains(member))
        {
            team.Members.Remove(member);
        }
        var emptyTeam = InZoneTeams.Find(team => team.Members.Count == 0);
        if (emptyTeam != null)
        {
            InZoneTeams.Remove(emptyTeam);
        }
    }
}
