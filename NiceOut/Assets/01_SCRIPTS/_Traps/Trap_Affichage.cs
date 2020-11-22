using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trap_Affichage : MonoBehaviour
{
    Inputs input;
    public GameObject player;
    public GameObject gestionPanel;
    public TextMeshProUGUI _PrixReparation;
    public TextMeshProUGUI _PrixRechargement;
    public TextMeshProUGUI _GainDemontage;
    public Image _Image_Reparation;
    public Image _Image_Rechargement;

    bool prixReparationAffiche;
    bool prixRechargeAffiche;
    bool gainDemontageAffiche;
    bool panelActive;
    float oldLifePercentage;
    float oldAmmoPercentage;


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
                        prixReparationAffiche = false;
                        prixRechargeAffiche = false;
                        gainDemontageAffiche = false;
                        oldTrap = _trapManager.selectedTrap;
                    }

                    _trapStats = _trapManager.selectedTrap.GetComponent<Traps>();

                    if (_trapStats != null)

                    {

                        if (oldAmmoPercentage != Mathf.RoundToInt(_trapStats.ammoPercentage * _trapStats.costs[_trapStats.upgradeIndex]) && prixRechargeAffiche == true)
                        {
                            prixRechargeAffiche = false;
                        }
                        if (oldLifePercentage != Mathf.RoundToInt(_trapStats.lifePercentage * _trapStats.costs[_trapStats.upgradeIndex]) && prixReparationAffiche == true)
                        {
                            prixReparationAffiche = false;
                        }

                        //Reparation
                        if (_trapStats.lifePercentage != 1 && prixReparationAffiche == false)
                        {
                            _Image_Reparation.color = Color.white;
                            _PrixReparation.text = "- " + Mathf.RoundToInt((1 - _trapStats.lifePercentage) * _trapStats.costs[_trapStats.upgradeIndex]).ToString();
                            oldLifePercentage = Mathf.RoundToInt(_trapStats.lifePercentage * _trapStats.costs[_trapStats.upgradeIndex]);
                            prixReparationAffiche = true;
                        }
                        else
                        {
                            if (prixReparationAffiche == false)
                            {
                                _Image_Reparation.color = Color.gray;
                                _PrixReparation.text = "";
                                prixReparationAffiche = true;
                            }
                        }
                        //Rechargement
                        if (_trapStats.ammoPercentage != 1 && prixRechargeAffiche == false)
                        {
                            _Image_Rechargement.color = Color.white;
                            _PrixRechargement.text = "- " + Mathf.RoundToInt((1 - _trapStats.ammoPercentage) * _trapStats.costs[_trapStats.upgradeIndex]).ToString();
                            oldAmmoPercentage = Mathf.RoundToInt(_trapStats.ammoPercentage * _trapStats.costs[_trapStats.upgradeIndex]);
                            prixRechargeAffiche = true;
                        }
                        else
                        {
                            if (prixRechargeAffiche == false)
                            {
                                _Image_Rechargement.color = Color.gray;
                                _PrixRechargement.text = "";
                                prixRechargeAffiche = true;
                            }
                        }

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
