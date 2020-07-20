using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string message;
    private float cooldown = 5f;
    private float lastShout = -4f;

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 50, Color.white, transform.position + new Vector3(0, 0.2f, 0), Vector3.zero, 3f);
        }

    }
}
