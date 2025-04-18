using UnityEngine;

public class ZoneControllerScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] doors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.position = getDoorPos(StateControllerScript.currLevel);
    }

    private Vector3 getDoorPos(string levelType) {
        Vector3 startPos;
        switch (levelType) {
            case "Entrance":
                startPos = doors[0].position;
                startPos.x += 5;
            break;

            case "Combat":
                startPos = doors[Random.Range(1,doors.Length - 2)].position;
                startPos.z -= 5;
            break;

            case "Cafe":
                startPos = doors[doors.Length-2].position;
                startPos.z -= 5;
            break;

            case "Shop":
                startPos = doors[doors.Length-1].position;
                startPos.z -= 5;
            break;

            case "Mini":
                startPos = doors[doors.Length-2].position;
                startPos.z -= 5;
            break;

            case "Boss":
                startPos = doors[doors.Length-2].position;
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
