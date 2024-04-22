using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togglesoupy : MonoBehaviour
{
    public GameObject goodsoup;
    private void OnEnable()
    {
        goodsoup.SetActive(true);
    }
    private void OnDisable()
    {
        goodsoup.SetActive(false);
    }
}
