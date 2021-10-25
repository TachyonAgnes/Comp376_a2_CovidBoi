using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerInput2D : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite rightSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // nothing to do here...
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Physics2D.gravity = new Vector2(Physics2D.gravity.y, 0);
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 auto_direction = new Vector3(1.0f, 1.0f, 0.0f);
        Vector3 direction = new Vector3(horizontal, vertical, 0.0f);
        direction = direction.normalized;

        transform.position += direction * speed * Time.deltaTime;

        // Rotate the sprite
        if (direction != Vector3.zero)
        {
            if (direction.y > 0)
            {
                spriteRenderer.sprite = upSprite;
            }
            else if (direction.y < 0)
            {
                spriteRenderer.sprite = downSprite;
            }
            else if (direction.x > 0)
            {
                spriteRenderer.sprite = rightSprite;
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.sprite = rightSprite;
                spriteRenderer.flipX = true;
            }
        }
    }
}
