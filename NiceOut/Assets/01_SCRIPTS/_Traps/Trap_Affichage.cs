using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trap_Affichage : MonoBehaviour
{
    Inputs input;
    public GameObject player;

    [Header("UI Elements")]
    public GameObject gestionPanel;
    public TextMeshProUGUI _GainDemontage;
    bool gainDemontageAffiche;
    bool panelActive;
    float oldUsurePercentage;

    Trap_Manager _trapManager;
    Traps _trapStats;
    GameObject oldTrap;


    private void Awake()
    {
        input = new Inputs();
        input.Actions.Test.performed += ctx => damageThisFukinTrap();
    }
    // Start is called before the first frame update
    void Start()
    {
        gestionPanel.SetActive(false);
        panelActive = false;
        _trapManager = player.GetComponent<Trap_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<Switch_Mode>().GetPause() == false && player.GetComponent<Switch_Mode>().GetMode() == false)
        {
            if (player.GetComponent<Switch_Mode>().mort == false)
            {
                if (_trapManager.selectedTrap != null)
                {
                    if (oldTrap != _trapManager.selectedTrap)
                    {
                        gainDemontageAffiche = false;
                        oldTrap = _trapManager.selectedTrap;
                    }

                    _trapStats = _trapManager.selectedTrap.GetComponent<Traps>();

                    if (_trapStats != null)

                    {
                        if (gainDemontageAffiche == false)
                        {
                            _GainDemontage.text = "+ " + _trapStats.sellCosts[_trapStats.upgradeIndex].ToString();
                            gainDemontageAffiche = true;
                        }
                    }

                    if (panelActive == false)
                    {
                        gestionPanel.SetActive(true);
                        panelActive = true;
                    }
                }
                else
                {
                    if (panelActive == true)
                    {
                        gestionPanel.SetActive(false);
                        panelActive = false;
                    }
                }
            }
            else
            {
                if (panelActive == true)
                {
                    gestionPanel.SetActive(false);
                    panelActive = false;
                }
            }

        }
        else
        {
            if (panelActive == true)
            {
                gestionPanel.SetActive(false);
                panelActive = false;
            }
        }
    }

    public void damageThisFukinTrap()
    {

        _trapManager = player.GetComponent<Trap_Manager>();
        if (_trapManager.selectedTrap != null)
        {
            _trapStats = _trapManager.selectedTrap.GetComponent<Traps>();
            _trapStats.DamageTrap(10);
        }

    }
    private void OnEnable()
    {
        input.Actions.Enable();
    }
    private void OnDisable()
    {
        input.Actions.Disable();
    }
}
