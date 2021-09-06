using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private BoxCollider2D boxCollide;

    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.85f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        boxCollide = GetComponent<BoxCollider2D>();
    }


    protected virtual void UpdateMotor(Vector3 input, Vector2 speed)
    {
        // Reset moveDelta
        moveDelta = new Vector3(input.x * speed.x, input.y * speed.y, 0);

        // Swap sprite direction, wheater you're going right or left
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Add push vector, if any
        moveDelta += pushDirection;

        // reduce push force based on recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Proveravamo da li se mozemo kretati u tom pravcu - y pravac
        hit = Physics2D.BoxCast(transform.position, boxCollide.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // moving
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
            // deltaTime - ako neko ima vise fps-a da ne bude igrac brzi i obrnuto

        }

        // za x pravac
        hit = Physics2D.BoxCast(transform.position, boxCollide.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // moving
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
            // deltaTime - ako neko ima vise fps-a da ne bude igrac brzi i obrnuto

        }
    }
}