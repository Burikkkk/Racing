using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private float followSpeed;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = cameraOffset.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraOffset.position, followSpeed * Time.deltaTime);
        transform.LookAt(player.position);
    }
}
