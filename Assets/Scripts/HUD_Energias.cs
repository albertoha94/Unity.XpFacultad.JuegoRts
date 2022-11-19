using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Energias : MonoBehaviour
{

    public static HUD_Energias self;
    public static Text R => self.r;
    public static Text G => self.g;
    public static Text B => self.b;

    [SerializeField] private Text r;
    [SerializeField] private Text g;
    [SerializeField] private Text b;

    private void Awake()
    {
        self = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
