using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPrice : MonoBehaviour
{
    public static ItemPrice instance;

    public UIHandler handler;
    public TMP_Text price;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    { 
        price = GetComponent<TMP_Text>();
        price.text = handler.priceDisplay.text;
    }
}
