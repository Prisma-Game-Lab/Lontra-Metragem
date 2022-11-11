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

    private Sprite SelectSprite(Vector3 direction, List<Sprite> spriteList)
    {
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))//movimento horizontal
        {
            if (direction.x > 0)
            {
                return spriteList[0];
            }
            else
            {
                return spriteList[1];
            }
        }
        else//movimento vertical
        {
            if (direction.y > 0)
            {
                return spriteList[2];
            }
            else
            {
                return spriteList[3];
            }
        }
    }

    public void SetSlidingPlayer(Vector3 direction)
    {
        sr.sprite = SelectSprite(direction, slidingSprites);
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))//movimento horizontal
        {
            //playerCollider.direction = CapsuleDirection2D.Horizontal;
            //playerCollider.size = new Vector2(sr.bounds.size.x * 0.7f, sr.bounds.size.y * 0.3f);
            //playerCollider.offset = new Vector2(-0f, -0.3f);
        }
    }

    public void SetStandingPlayer(Vector3 direction)
    {
        sr.sprite = SelectSprite(direction, standingSprites);
        //playerCollider.direction = CapsuleDirection2D.Vertical;
        //playerCollider.size = new Vector3(sr.bounds.size.x * 0.6f, sr.bounds.size.y * 0.7f, sr.bounds.size.z);
        //playerCollider.offset = new Vector2(-0f, -0f);
    }
}
