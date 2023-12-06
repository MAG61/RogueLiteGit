using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Weapon mainWeapon;

    public Transform mainWeaponLoc;
    private Vector3 mousePosition;


    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        mainWeapon.transform.eulerAngles = new Vector3(0, 0, angle);

        if (Input.GetButton("Fire1"))
        {
            if (mainWeapon.canFire) mainWeapon.Shoot();
        }
    }

    private void FixedUpdate()
    {
        mainWeapon.transform.position = mainWeaponLoc.position;



        //mainWeapon.transform.LookAt(mousePosition);
    }
}
