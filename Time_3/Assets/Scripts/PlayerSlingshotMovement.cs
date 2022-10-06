using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlingshotMovement : MonoBehaviour
{
    [SerializeField]
    private float impulseForce = 1.0f;
    private bool onSlingshot = false;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (CheckTouch(Camera.main.ScreenToWorldPoint(touch.position)))
                        onSlingshot = true;
                    break;
                case TouchPhase.Moved:
                    if (onSlingshot)
                        Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(touch.position));
                    break;
                case TouchPhase.Ended:
                    onSlingshot = false;
                    Vector3 direction = transform.position - Camera.main.ScreenToWorldPoint(touch.position);
                    rb.AddForce(new Vector2(direction.x, direction.y) * impulseForce);
                    break;
            }
        }
    }


    private bool CheckTouch(Vector3 initialPosition)
    {
        if (Mathf.Abs(initialPosition.x - transform.position.x) > 0.3f)
            return false;
        if (Mathf.Abs(initialPosition.y - transform.position.y) > 0.3f)
            return false;
        return true;
    }
}
