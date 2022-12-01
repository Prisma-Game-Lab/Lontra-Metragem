using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> standingSprites;
    [SerializeField]
    private List<Sprite> slidingSprites;

    private CapsuleCollider2D playerCollider;
    private SpriteRenderer sr;
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetSlidingPlayer(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))//movimento horizontal
        {
            playerCollider.direction = CapsuleDirection2D.Horizontal;
            playerCollider.size = new Vector2(sr.bounds.size.x * 0.6f, sr.bounds.size.y * 0.5f);
        }
    }
    public void SetStandingPlayer(Vector3 direction)
    {
        playerCollider.direction = CapsuleDirection2D.Vertical;
        playerCollider.size = new Vector3(sr.bounds.size.x * 0.6f, sr.bounds.size.y * 0.7f, sr.bounds.size.z);
    }
}
