using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Collidable
{
    // Damage structure
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7};
    public float[] pushForce = {2f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f};

    //Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //swing
    private float cooldown = 0.7f;
    private float lastSwing;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if(coll.name == "Player")
                return;

            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("recieveDamage", dmg);


            Debug.Log(coll.name);
        }
    }
    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel ++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        //Change stats
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
