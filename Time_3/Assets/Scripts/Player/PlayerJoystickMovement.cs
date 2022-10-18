using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickMovement : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick joystick;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float speed;
    private float horizontal;
    private float vertical;

    private void Start()
    {
        if (GetComponent<PlayerStatus>().activeMovement == MovementType.slingshot)
            joystick.gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        horizontal = joystick.Horizontal * speed;
        vertical = joystick.Vertical * speed;

        player.position = player.position + new Vector3(horizontal, vertical, 0f) * Time.deltaTime;
    }
}
