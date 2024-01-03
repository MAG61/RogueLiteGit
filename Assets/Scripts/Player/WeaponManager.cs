using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [Space]
   
    [Header("Weapons")]
    public MAP<Weapon, int> weapons = new MAP<Weapon, int>();
    public Weapon mainWeapon;
    public Transform mainWeaponLoc;
    [SerializeField] private AutomaticWeapons[] automaticWeapons;

    [Space]
    [Space]

    public float knockBackMultiplyer;


    private PlayerStats stats;
    private Vector3 mousePosition;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        //weapons = FindObjectsOfType<Weapon>();
        //mainWeapon = weapons[0];
        foreach (Weapon w in FindObjectsOfType<Weapon>())
        {
            weapons.KeysList.Add(w);
            weapons.ValuesList.Add(weapons.ValuesList.Count + 1);
        }

        mainWeapon = weapons.KeysList[0];
        for (int i = 1; i < weapons.KeysList.Count; i++) automaticWeapons[i - 1].currentWeapon = weapons.KeysList[i];
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();


        GameObject.Find("FireRate").GetComponent<Slider>().value = mainWeapon.fireRate;
        GameObject.Find("Accuracy").GetComponent<Slider>().value = mainWeapon.accuracy;
        GameObject.Find("Damage").GetComponent<Slider>().value = mainWeapon.damage;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Aiming();
        Shooting();
        if (Input.GetKeyDown(KeyCode.Q)) ChangeWeapon();
    }

    private void FixedUpdate()
    {
        if (mainWeapon.transform.position != mainWeaponLoc.position)
        {
            mainWeapon.transform.position = mainWeaponLoc.position;

        }

    }

    #region Aiming
    private void Aiming()
    {
        Vector3 lookDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        mainWeapon.transform.eulerAngles = new Vector3(0, 0, angle);

        if (angle > 90 || angle < -90)
        {
            mainWeapon.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            mainWeapon.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    #endregion


    #region Shooting
    private void Shooting()
    {
        if (Input.GetButton("Fire1"))
        {
            if (mainWeapon.canFire)
            {
                mainWeapon.Shoot(stats.AttackSpeedPercentage, stats.DamagePercentage);
                GetComponent<Rigidbody2D>().AddForce((transform.position - mousePosition).normalized * mainWeapon.damage * knockBackMultiplyer);
            }
        }
    }
    #endregion

    #region ChangeWeapon
    private void ChangeWeapon()
    {
        foreach (Weapon w in weapons.KeysList)
        {
            if (weapons.GetValue(w) - 1 == 0) { weapons.SetValue(w, weapons.KeysList.Count); }
            else { weapons.SetValue(w, weapons.GetValue(w) - 1); }
        }

        foreach (Weapon w in weapons.KeysList)
        {
            if (weapons.GetValue(w) == 1) mainWeapon = w;
            else automaticWeapons[weapons.GetValue(w) - 2].currentWeapon = w;
        }

        GameObject.Find("FireRate").GetComponent<Slider>().value = mainWeapon.fireRate;
        GameObject.Find("Accuracy").GetComponent<Slider>().value = mainWeapon.accuracy;
        GameObject.Find("Damage").GetComponent<Slider>().value = mainWeapon.damage;
    }
    #endregion

    #region FastUI
    public void FireRateChanged(float newValue)
    {
        if (mainWeapon.fireRate != newValue) mainWeapon.fireRate = newValue;
        GameObject.Find("FireRateText").GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }

    public void AccuracyChanged(float newValue)
    {
        if (mainWeapon.accuracy != newValue) mainWeapon.accuracy = newValue;
        GameObject.Find("AccuracyText").GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }

    public void DamageChanged(float newValue)
    {
        if (mainWeapon.damage != newValue) mainWeapon.damage = newValue;
        GameObject.Find("DamageText").GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }
    #endregion
}
