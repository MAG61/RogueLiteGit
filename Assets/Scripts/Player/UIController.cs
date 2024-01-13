using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject shopItems;
    [SerializeField] private GameObject CraftUI;
    private bool isShopEnabled = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            shopUI.GetComponent<ShopUI>().ReloadItems();
            isShopEnabled = !isShopEnabled;
        }

        if (isShopEnabled)
        {
            shopUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            shopUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void CloseShop()
    {
        CloseCrafting();
        isShopEnabled = false;
    }
    public void OpenShop()
    {
        if (!isShopEnabled)
        {
            isShopEnabled = true;
            StartCoroutine(reloadItems());
        }
    }

    public bool ShopEnabled() { return isShopEnabled; }

    IEnumerator reloadItems()
    {
        yield return new WaitForEndOfFrame();
        shopUI.GetComponent<ShopUI>().ReloadShopItems();
        shopUI.GetComponent<ShopUI>().ReloadWeaponSlots();
    }

    public void OpenCrafting()
    {
        shopItems.SetActive(false);
        CraftUI.SetActive(true);
    }

    public void CloseCrafting()
    {
        shopItems.SetActive(true);
        CraftUI.SetActive(false);
    }
}
