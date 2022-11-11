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
    private ColliderController colliderController;

    private void Start()
    {
        if (GetComponent<PlayerStatus>().activeMovement == MovementType.slingshot)
        {
            joystick.gameObject.SetActive(false);
            this.enabled = false;
        }   
        colliderController = GetComponent<ColliderController>();
    }
    void FixedUpdate()
    {            
        horizontal = joystick.Horizontal * speed;
        vertical = joystick.Vertical * speed;
        Vector3 direction = new Vector3(horizontal, vertical, 0f);

        player.position = player.position + new Vector3(horizontal, vertical, 0f) * Time.deltaTime;
        if(direction != Vector3.zero)
            colliderController.SetStandingPlayer(new Vector3(horizontal, vertical, 0f));
    }
}
