using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 position;

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        transform.position = position;
    }
}
