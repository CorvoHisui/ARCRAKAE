using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected Vector3 originalSize;
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D GetHit2D;
    public float ySpeed = 0.75f;
    public float xSpeed = 1f;

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 Input)
    {
        //reset vector
        moveDelta = new Vector3(Input.x * xSpeed, Input.y * ySpeed, 0);

        //swap sprite direction
        if(moveDelta.x>0)
            transform.localScale = originalSize;
        else if(moveDelta.x<0)
            transform.localScale = new Vector3(-originalSize.x,originalSize.y,originalSize.z);

        //add push vector
        moveDelta += pushDirection;
        //reduce force
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //movement
        transform.Translate(moveDelta * Time.deltaTime);
    }

}
