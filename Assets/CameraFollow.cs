using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTracking : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform character;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(character.position.x, character.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}
