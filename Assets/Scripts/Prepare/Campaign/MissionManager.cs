using System.Collections.Generic;
using UnityEngine;

public class MissionBase
{
    public int CampaignNumber;
    public int MissionNumber;
    public string Name;
    public string Description;
    public Sprite Sprite;

    public int Gold;

    public bool Completed;
}

public class MissionArmy
{
    public Army Army;
}

public class Mission
{
    public MissionBase MissionBase;
    public MissionArmy MissionArmy;

    public Mission(int campaignNumber, int missionNumber, string name, string spritePath, Army army, string description = "", int gold = 1000)
    {
        MissionBase = new MissionBase()
        {
            CampaignNumber = campaignNumber,
            MissionNumber = missionNumber,
            Name = name,
            Description = description,
            Sprite = Resources.Load<Sprite>(spritePath),
            Gold = gold
        };

        MissionArmy = new MissionArmy()
        {
            Army = army
        };
    }
}

public static class MissionList
{
    public static bool IsMissionsAdded = false;

    public static List<Mission> AllMission = new List<Mission>();
}


public class MissionManager : MonoBehaviour
{
    private void Awake()
    {

    }

    private void Start()
    {
        if (!MissionList.IsMissionsAdded)
        {
            AddMission();
            MissionList.IsMissionsAdded = true;
        }
    }

    private void AddMission()
    {
        MissionList.AllMission.Add(new Mission(0, 0, "����� ��������", "Sprites/Missions/Mission1", AllEnemyArmy.EnemyArmy[0], "��������� ����� �������� ������ ��������� ���������. ��������, ��� ��� ���� �������� ����. ���������� ���� ���������!"));
        MissionList.AllMission.Add(new Mission(0, 1, "����� ��������", "Sprites/Missions/Mission1", new Army(), "����� �������� ���� � ���� ������ ��������. ����� ������ ����� � ��������� �� ��� ���� ���� ������ ���������!"));
        MissionList.AllMission.Add(new Mission(0, 2, "������� ��������", "Sprites/Missions/Mission2", new Army(), "����� ����� �������� ���������� �� �������� ������ ��� ���� ����� �������� ���� ����. � �� ������� ���� �������. ����� �������� �� ��� �� ����� �������� �� ����� ��������!"));
        MissionList.AllMission.Add(new Mission(0, 3, "����� ��������", "Sprites/Missions/Mission2", new Army(), "��������, �� ������� ���������� ������ ����� ��������� ����� �������� ������������� � ���. ������� �������� ���� ���������!"));
        MissionList.AllMission.Add(new Mission(0, 4, "������ ��������", "Sprites/Missions/Mission3", new Army(), "��������, ������ �������� ������ ��� ������� ����� ����� � ������ ��� �����, �� ������ ������ �������. �������� � ��� ��� � ��������!"));
        MissionList.AllMission.Add(new Mission(0, 5, "����������� ����", "Sprites/Missions/Mission4", new Army(), "����� ��������� ������ �������� �������� � �������� ������ ����� � ����� �� ��� ������ � �����, ������ � ��� ���� ������ ����� �� ���� ���. ��� �� �� ������� �������� ������ ��� ����!"));
        MissionList.AllMission.Add(new Mission(0, 6, "������ ������������", "Sprites/Missions/Mission4", new Army(), "������������ ����� ����� ������� � ������ ������, ������ ���� ����� �������� ����� ��� �� ������ ����� �������� - � �����. �� ������ ����� �� ��� ��� �������� ������� �����, ��������!"));
        MissionList.AllMission.Add(new Mission(0, 7, "������ �����", "Sprites/Missions/Mission5", new Army(), "� ��� � �������� ���� ������ �����, ��� ����� ��������, �� �� ���������!"));
        MissionList.AllMission.Add(new Mission(0, 8, "������", "Sprites/Missions/Mission6", new Army(), ""));
        MissionList.AllMission.Add(new Mission(0, 9, "���", "Sprites/Missions/Mission6", new Army(), ""));
        MissionList.AllMission.Add(new Mission(0, 10, "������� ����", "Sprites/Missions/Mission7", new Army(), ""));
        MissionList.AllMission.Add(new Mission(0, 11, "����� ����", "Sprites/Missions/Mission17", new Army(), ""));

        MissionList.AllMission.Add(new Mission(1, 0, "������ ��������", "Sprites/Missions/Mission1", new Army(), "��������� ������ ��������-����� ��������� ���� �������� �������. �������� ������� ���� ������, ��� �������� � �����!"));
        MissionList.AllMission.Add(new Mission(1, 1, "������ ��������", "Sprites/Missions/Mission2", new Army(), "����� ����� ��������-����� ��������� � ���� ������. ����� ���������� �� ���� ��� �� ������ ���� ������ ��� ��� ������!"));
        MissionList.AllMission.Add(new Mission(1, 2, "�������������", "Sprites/Missions/Mission3", new Army(), "������� ������ ������������ ������������ ������. ��� ������������� �� ��������� �� � ������ ������� �� � ������ ���������!"));
        MissionList.AllMission.Add(new Mission(1, 3, "����������", "Sprites/Missions/Mission4", new Army(), "������ ����� �������� - ����������, � �� ��� ����� ������! ����� ���������� �������� ������ ��������!"));
        MissionList.AllMission.Add(new Mission(1, 4, "�����������", "Sprites/Missions/Mission5", new Army(), "������� ������ ������� � ��� ����� ����� ����� - ����������. ��� �� �� �������!"));
        MissionList.AllMission.Add(new Mission(1, 5, "������ �������", "Sprites/Missions/Mission6", new Army(), ""));
        MissionList.AllMission.Add(new Mission(1, 6, "������ ������ ��������", "Sprites/Missions/Mission7", new Army(), ""));
        MissionList.AllMission.Add(new Mission(1, 7, "����� �����", "Sprites/Missions/Mission7", new Army(), ""));
        MissionList.AllMission.Add(new Mission(1, 8, "����� �������", "Sprites/Missions/Mission7", new Army(), ""));
        MissionList.AllMission.Add(new Mission(1, 9, "����� ����� ��������", "Sprites/Missions/Mission7", new Army(), ""));
        MissionList.AllMission.Add(new Mission(1, 10, "��������� ��������", "Sprites/Missions/Mission7", new Army(), ""));

        MissionList.AllMission.Add(new Mission(2, 0, "����� �������", "Sprites/Missions/Mission1", new Army(), ""));
        MissionList.AllMission.Add(new Mission(2, 1, "������ �����", "Sprites/Missions/Mission2", new Army(), ""));
        MissionList.AllMission.Add(new Mission(2, 2, "������ ����", "Sprites/Missions/Mission3", new Army(), ""));
        MissionList.AllMission.Add(new Mission(2, 3, "���������", "Sprites/Missions/Mission4", new Army(), ""));
        MissionList.AllMission.Add(new Mission(2, 4, "�������� ����", "Sprites/Missions/Mission5", new Army(), ""));
        MissionList.AllMission.Add(new Mission(2, 5, "�������� �������", "Sprites/Missions/Mission6", new Army(), ""));
        MissionList.AllMission.Add(new Mission(2, 6, "���������� ���", "Sprites/Missions/Mission7", new Army(), ""));

        MissionList.AllMission.Add(new Mission(3, 0, "���������� �� �������", "Sprites/Missions/Mission1", new Army(), ""));
        MissionList.AllMission.Add(new Mission(3, 1, "���������", "Sprites/Missions/Mission2", new Army(), ""));
        MissionList.AllMission.Add(new Mission(3, 2, "���� �������", "Sprites/Missions/Mission3", new Army(), ""));
        MissionList.AllMission.Add(new Mission(3, 3, "���� ������", "Sprites/Missions/Mission4", new Army(), ""));
        MissionList.AllMission.Add(new Mission(3, 4, "�������", "Sprites/Missions/Mission5", new Army(), ""));
        MissionList.AllMission.Add(new Mission(3, 5, "������� �������", "Sprites/Missions/Mission6", new Army(), ""));
        MissionList.AllMission.Add(new Mission(3, 6, "�����������", "Sprites/Missions/Mission7", new Army(), ""));

        MissionList.AllMission.Add(new Mission(4, 0, "����� ������", "Sprites/Missions/Mission1", new Army(), ""));
        MissionList.AllMission.Add(new Mission(4, 1, "������ ���������", "Sprites/Missions/Mission2", new Army(), ""));
        MissionList.AllMission.Add(new Mission(4, 2, "������ ���������", "Sprites/Missions/Mission3", new Army(), ""));
        MissionList.AllMission.Add(new Mission(4, 3, "������ ���������", "Sprites/Missions/Mission4", new Army(), ""));
        MissionList.AllMission.Add(new Mission(4, 4, "��������� ���������", "Sprites/Missions/Mission5", new Army(), ""));
        MissionList.AllMission.Add(new Mission(4, 5, "����� ���������", "Sprites/Missions/Mission6", new Army(), ""));
        MissionList.AllMission.Add(new Mission(4, 6, "������ ���������", "Sprites/Missions/Mission7", new Army(), ""));
    }
}
