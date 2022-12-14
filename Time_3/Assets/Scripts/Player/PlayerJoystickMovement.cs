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
    private Vector3 lastDirection;
    private Animator playerAnim;

    private void Start()
    {
        if (GetComponent<PlayerStatus>().activeMovement == MovementType.slingshot)
        {
            this.enabled = false;
        }   
        colliderController = GetComponent<ColliderController>();
        playerAnim = GetComponent<Animator>();
        //joystick.transform.position = new Vector3(PlayerPrefs.GetFloat("JoystickX"), PlayerPrefs.GetFloat("JoystickY"), PlayerPrefs.GetFloat("JoystickZ"));
    }

    private void OnEnable()
    {
        if (GetComponent<PlayerStatus>().activeMovement == MovementType.joystick)
        {
            joystick.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (GetComponent<PlayerStatus>().activeMovement == MovementType.slingshot)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {            
        horizontal = joystick.Horizontal * speed;
        vertical = joystick.Vertical * speed;
        Vector3 direction = new Vector3(horizontal, vertical, 0f);

        player.position = player.position + direction * Time.deltaTime;
        if(direction != Vector3.zero)
        {
            playerAnim.SetBool("moving", true);
            playerAnim.SetFloat("X", direction.x);
            playerAnim.SetFloat("Y", direction.y);
            lastDirection = direction;
            colliderController.SetSlidingPlayer(direction);
        }
        else
        {
            Vector3 dir = lastDirection.normalized;
            playerAnim.SetFloat("X", dir.x);
            playerAnim.SetFloat("Y", dir.y);
            colliderController.SetStandingPlayer(dir);
            playerAnim.SetBool("moving", false);
        }      
    }
}
