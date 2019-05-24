using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMainMenu : MonoBehaviour
{
    public GameObject botoesPrincipais;
    public GameObject painelUpgrades;
    public GameObject mensagemDeImpdimento;

    // Start is called before the first frame update
    void Start()
    {
        MainData.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtivaPainelUpgrades()
    {
        botoesPrincipais.SetActive(false);
        if (MainData.arrozTotal != 0 || MainData.arrozInLevel[0] != 0)
        {
            painelUpgrades.SetActive(true);
        }
        else
        {
            mensagemDeImpdimento.SetActive(true);
        }
    }
}
