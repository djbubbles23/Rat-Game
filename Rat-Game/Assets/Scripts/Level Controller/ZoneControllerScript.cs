using UnityEngine;

public class ZoneControllerScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform entranceDoor;
    [SerializeField] Transform[] combatDoors; 
    [SerializeField] Transform cafeDoor;
    [SerializeField] Transform shopDoor;
    [SerializeField] Transform bossDoor;

    private void Awake()
    {
        if (StateControllerScript.currLevel != "Entrance")
            StateControllerScript.currLevel = "Entrance";

        if (StateControllerScript.zoneOrder.Count == 0)
            StateControllerScript.GenerateZoneOrder();

        StartZone();
    }

    public void StartZone()
    {
        player.position = GetDoorPos(StateControllerScript.currLevel);
    }

    private Vector3 GetDoorPos(string levelType)
    {
        Vector3 pos;
        switch (levelType)
        {
            case "Entrance":
                pos = entranceDoor.position;
                pos.x += 5;
                break;

            case "Combat1":
                pos = combatDoors[0].position;
                pos.z -= 2;
                break;
            case "Combat2":
                pos = combatDoors[1].position;
                pos.z -= 2;
                break;
            case "Combat3":
                pos = combatDoors[2].position;
                pos.z -= 2;
                break;

            case "Cafe":
                pos = cafeDoor.position;
                pos.z -= 5;
                break;

            case "Shop":
                pos = shopDoor.position;
                pos.z -= 5;
                break;

            case "Boss":
                pos = bossDoor.position;
                pos.z -= 5;
                break;

            default:
                pos = entranceDoor.position;
                break;
        }
        return pos;
    }
}
