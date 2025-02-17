using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public TargetingSystem targetingSystem;
    public float cooldownTime = 0.5f;

    Timer cooldown;
    Vector3? target = null;

    void Start()
    {
        cooldown = new Timer(cooldownTime);
        cooldown.Start();
    }
    void FixedUpdate()
    {
        if (GameState.IsGameplayPaused())
            return;

        if (cooldown.IsRunning())
        {
            cooldown.Update(Time.fixedDeltaTime);
            return;
        }

        if (target == null)
        {
            target = targetingSystem.GetTarget();
            return;
        }

        Shoot();
        target = null;
        cooldown.Start();
    }

    void Shoot()
    {   
        Debug.Log(targetingSystem.GetTargetObject().transform.position);
        Debug.Log(targetingSystem.GetTargetObject().tag);
        GameObject gameObject = Instantiate(bullet, transform.position, Quaternion.identity);
        MoveTowardsDirection moveTowardsDirection = gameObject.GetComponent<MoveTowardsDirection>();

        if (moveTowardsDirection != null)
            moveTowardsDirection.SetDirection(target.Value - transform.position);
    }
}
