using Controller;
using System;
using Terresquall;
using UnityEngine;

public class BulletFireController
{
    [SerializeField]float speed = 5f;
    
    [SerializeField]float interval = 0.25f;
    [SerializeField]float curInterval = 0.0f;

    [SerializeField]GameObject bulletPrefab;
    [SerializeField]Transform owner;
    [SerializeField]VirtualJoystick fireJs;

    private bool enableBulletSpawn = false;

    
    public void Initialize(Transform newOwner, GameObject newBullet, float speed, VirtualJoystick newFireJs = null)
    {
        owner = newOwner;
        bulletPrefab = newBullet;

        fireJs = newFireJs;

        if (fireJs != null)
        {
            RegisterEvents();
        }
    }

    public void OnDestroy()
    {
        UnRegisterEvents();
    }

    void RegisterEvents()
    {
        fireJs.OnFireJsPointerDownEvent.AddListener(OnFireJsPointerDown);
        fireJs.OnFireJsPointerUpEvent.AddListener(OnFireJsPointerUp);
    }

    void UnRegisterEvents()
    {
        fireJs.OnFireJsPointerDownEvent.RemoveListener(OnFireJsPointerDown);
        fireJs.OnFireJsPointerUpEvent.RemoveListener(OnFireJsPointerUp);
    }

    public void Update(float deltaTime)
    {
        if (enableBulletSpawn && curInterval <= 0.0f)
        {
            FireBullet(fireJs.GetAxis());

            curInterval += interval;
        }

        if(curInterval > 0.0f)
        {
            curInterval -= deltaTime;
        }
    }

    public void FireBullet(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        GameObject newBullet = GameObject.Instantiate(bulletPrefab, owner.transform.position, Quaternion.identity);
        BulletControllerManager bulletControl = newBullet.GetComponent<BulletControllerManager>();
        bulletControl.Initialize(direction, speed);
    }

    void OnFireJsPointerDown()
    {
        enableBulletSpawn = true;
    }

    void OnFireJsPointerUp()
    {
        enableBulletSpawn = false;
    }
}
