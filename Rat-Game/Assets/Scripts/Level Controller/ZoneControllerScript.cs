using UnityEngine;

public class ZoneControllerScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] doors;

    private void Start()
    {
        StartZone();
    }

    public void StartZone()
    {
        player.position = GetDoorPos(StateControllerScript.currLevel);
    }

    private Vector3 GetDoorPos(string levelType)
    {
        Vector3 startPos;
        switch (levelType)
        {
            case "Entrance":
                startPos = doors[0].position;
                startPos.x += 5;
                break;

            case "Combat":
                startPos = doors[Random.Range(1, doors.Length - 2)].position;
                startPos.z -= 2;
                break;

            case "Cafe":
            case "Mini":
            case "Boss":
                startPos = doors[doors.Length - 2].position;
                startPos.z -= 5;
                break;

            case "Shop":
                startPos = doors[doors.Length - 1].position;
                startPos.z -= 5;
                break;

            default:
                startPos = doors[0].position;
                startPos.z -= 5;
                break;
        }
        return startPos;
    }
}
