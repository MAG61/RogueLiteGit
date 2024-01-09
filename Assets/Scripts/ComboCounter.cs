using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    private int combo = 0;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private TextMeshProUGUI comboText;
    private void Start()
    {
        Enemy.EnemyDead += AddCombo;
        comboText.text = combo.ToString();
    }

    public void AddCombo()
    {
        combo++;
        comboText.text = combo.ToString();
        GetComponent<Animation>().Play("Activated");
        StopAllCoroutines();
        StartCoroutine(resetCombo());
    }

    IEnumerator resetCombo()
    {
        yield return new WaitForSeconds(resetTime);
        combo = 0;
        comboText.text = combo.ToString();
    }
}
