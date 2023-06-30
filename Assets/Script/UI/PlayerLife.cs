using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public TextMeshProUGUI PlayerLifeText;
    public LevelManager Levelmanager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerLifeText != null)
        {
            PlayerLifeText.text = "X " + Levelmanager.PlayerLife;
        }
    }
}
