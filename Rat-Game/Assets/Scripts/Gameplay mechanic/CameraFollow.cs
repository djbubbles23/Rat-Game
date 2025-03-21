using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x,
                                         transform.position.y,
                                         transform.position.z);
    }
}
