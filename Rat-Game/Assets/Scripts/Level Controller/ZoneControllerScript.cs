using UnityEngine;

public class ZoneControllerScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform entranceDoor;
    [SerializeField] Transform[] combatDoors; 
    [SerializeField] Transform[] shopDoor;
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
            // Combat 
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
            case "Combat4":
                pos = combatDoors[3].position;
                pos.z -= 2;
                break;
            case "Combat5":
                pos = combatDoors[4].position;
                pos.z -= 2;
                break;
            
            // Shop 
            case "Shop1":
                pos = shopDoor[0].position;
                pos.z -= 5;
                break;
            case "Shop2":
                pos = shopDoor[1].position;
                pos.z -= 5;
                break;
            case "Shop3":
                pos = shopDoor[2].position;
                pos.z -= 5;
                break;
            
            // Boss
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
