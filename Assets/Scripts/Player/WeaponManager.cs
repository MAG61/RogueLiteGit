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
    [SerializeField] private Transform weaponsParent;
    public GameObject[] weaponPrefabs;

    private PlayerInputs playerInputs;
    private PlayerStats stats;
    private Vector3 mousePosition;
    void Start()
    {
        playerInputs = new PlayerInputs();
        stats = GetComponent<PlayerStats>();
        //weapons = FindObjectsOfType<Weapon>();
        //mainWeapon = weapons[0];
        //foreach (Weapon w in FindObjectsOfType<Weapon>())
        //{
        //    weapons.KeysList.Add(w);
        //    weapons.ValuesList.Add(weapons.ValuesList.Count + 1);
        //}
        playerInputs.Player.Enable();

        mainWeapon = weapons.KeysList[0];
        for (int i = 1; i < weapons.KeysList.Count; i++) automaticWeapons[i - 1].currentWeapon = weapons.KeysList[i];
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        RefreshWeapons();

        GameObject.Find("FireRate").GetComponent<Slider>().value = mainWeapon.fireRate;
        GameObject.Find("Accuracy").GetComponent<Slider>().value = mainWeapon.accuracy;
        GameObject.Find("Damage").GetComponent<Slider>().value = mainWeapon.damage;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Shooting();
        Aiming();
        if (Input.GetKeyDown(KeyCode.Q)) ChangeWeapon();
    }

    private void FixedUpdate()
    {
        if (mainWeapon.transform.position != mainWeaponLoc.position)
        {
            mainWeapon.transform.position = mainWeaponLoc.position;
        }
    }

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

    public void Shooting()
    {
        if (playerInputs.Player.Fire.ReadValue<float>() > 0.1f)
        {
            if (mainWeapon.canFire)
            {
                mainWeapon.Shoot(stats.AttackSpeedPercentage, stats.DamagePercentage);
                GetComponent<Rigidbody2D>().AddForce((transform.position - mousePosition).normalized * mainWeapon.damage * knockBackMultiplyer);
            }
        }
    }

    public void AddWeapon(Item item)
    {
        Instantiate<GameObject>(item.gameObject, weaponsParent);
        RefreshWeapons();
    }

    public void RemoveWeapon(Item item)
    {

        for (int i = 0; i < weapons.KeysList.Count; i++)
        {
            if (weapons.KeysList[i] == mainWeapon) ChangeWeapon();
            if (weapons.KeysList[i] == item)
            {
                weapons.KeysList.RemoveAt(i);
                weapons.ValuesList.RemoveAt(i);
                item.gameObject.SetActive(false);
            }
        }
        RefreshWeapons();
    }

    public void RefreshWeapons()
    {
        Weapon[] ws = GetComponentsInChildren<Weapon>();

        foreach (Weapon w in ws)
        {
            if (!weapons.KeysList.Contains(w))
            {
                weapons.KeysList.Add(w);
                weapons.ValuesList.Add(weapons.ValuesList.Count + 1);
            }
        }

        foreach (Weapon w in weapons.KeysList)
        {
            if (!w.gameObject.activeSelf) w.gameObject.SetActive(true);
        }

        //foreach (Weapon w in weapons.KeysList)
        //{
        //    if (weapons.GetValue(w) == 1) mainWeapon = w;
        //    else automaticWeapons[weapons.GetValue(w) - 2].currentWeapon = w;
        //}

        if (Mathf.Max(weapons.ValuesList.ToArray()) != weapons.KeysList.Count)
        {
            int index = 2;
            foreach (Weapon w in weapons.KeysList)
            {
                if (w == mainWeapon) weapons.SetValue(w, 1);
                else
                {
                    weapons.SetValue(w, index);
                    index++;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (i >= weapons.KeysList.Count)
            {
                if (i >= 1)
                {
                    automaticWeapons[i - 1].currentWeapon = null;
                }
                else
                {
                    mainWeapon = null;
                }
            }
            else
            {
                if (weapons.GetValue(weapons.KeysList[i]) == 1) mainWeapon = weapons.KeysList[i];
                else automaticWeapons[weapons.GetValue(weapons.KeysList[i]) - 2].currentWeapon = weapons.KeysList[i];
            }
        }
    }

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
