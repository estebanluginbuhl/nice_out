using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsDamages : MonoBehaviour
{
    //Legend:
    //NF = not Flying
    //F  = flying
    //every = all enm
    public bool roundTrip, zone, slowdown, slowdownDamage, tesla, proxyMine, crossbow, decoy, dps, silverWall, tWall, FlameThrower, backFire, grenadeTrap = false;

    void Update()
    {
        if (zone == true)
        {
            ZoneNF();

            zone = false;
        }
        else if(slowdown == true)
        {
            SlowDownNF();

            slowdown = false;
        }
        else if (slowdownDamage == true)
        {
            SlowDownDamageNF();

            slowdownDamage = false;
        }
        else if (tesla == true)
        {
            TelsaEvery();

            tesla = false;
        }
        else if (proxyMine == true)
        {
            ProxyMineNF();

            proxyMine = false;
        }
        else if (crossbow == true)
        {
            CrossbowEvery();

            crossbow = false;
        }
        else if (decoy == true)
        {
            DecoyEvery();

            decoy = false;
        }
        else if (dps == true)
        {
            DpsEvery();

            dps = false;
        }
        else if (silverWall == true)
        {
            SilverWallEvery();

            silverWall = false;
        }
        else if (tWall == true)
        {
            TWallEvery();

            tWall = false;
        }
        else if (FlameThrower == true)
        {
            FlameThrowerEvery();

            FlameThrower = false;
        }
        else if (backFire == true)
        {
            BackFireEvery();

            backFire = false;
        }
        else if (grenadeTrap == true)
        {
            GrenadeLauncherEvery();

            grenadeTrap = false;
        }
        else if (roundTrip == true)
        {
            RoundTripNF();

            roundTrip = false;
        }
        else
        {
            return;
        }
    }

    void ZoneNF() //Piège fait des dégâts aux ennemis(sol)
    {
        Debug.Log("zone");
    }

    void SlowDownNF() //Piège doit ralentir les ennemis qui passe dessus (sol peut-être mur)
    {
        Debug.Log("slowndamage");
    }

    void SlowDownDamageNF() //Piège doit ralentir et faire des petits dégâts en continu (sol)
    {
        Debug.Log("slowdown + damage");
    }

    void TelsaEvery() //Piège doit attaquer les ennemis qui passent dans sa zone d’action circulaire (dégât moyen, tesla) (sol)
    {
        Debug.Log("tesla");
    }

    void ProxyMineNF() //Piège qui explose dès qu’un ennemi marche dessus (utilisation unique ou une fois par vague) (sol)
    {
        Debug.Log("proxymine");
    }

    void CrossbowEvery() //Piège qui tire des flèches sur les ennemis (mur)
    {
        Debug.Log("crossbow");
    }

    void DecoyEvery() //Piège qui attire les ennemis sur lui pour prendre des dégâts et arrêter leur avancée (sol et pouvant être détruit)
    {
        Debug.Log("decoy");
    }

    void DpsEvery() //Piège qui applique des dégâts par seconde (sol et mur)
    {
        Debug.Log("dps");
    }

    void SilverWallEvery() //Piège qui fait beaucoup de dégâts et pouvant les repousser (mur)
    {
        Debug.Log("SilverWall");
    }

    void TWallEvery() //Piège bloquant le passage à l’ennemi (sol et peut être détruit)
    {
        Debug.Log("TWall");
    }

    void FlameThrowerEvery() //Piège qui attaque avec des flammes faisant des dégâts à l’ennemi en aoe(sol ou mur)
    {
        Debug.Log("FlameThrower");
    }

    void BackFireEvery() //Piège qui explose quand on l’attaque que ça soit nous ou l’ennemi (sol/ une seule utilisation)
    {
        Debug.Log("backfire");
    }

    void GrenadeLauncherEvery() //Piège qui lâche un explosif dans sa zone d’action et explose après qq secondes(mur)
    {
        Debug.Log("grenade");
    }

    void Error1() //Piège qui arrête les ennemis pendant qq secondes ou si on leur inflige des dégâts
    {
        Debug.Log("error1");
    }

    void Error2() //Piège qui repousse les ennemis dans la direction opposé au piège (mur ou sol)
    {
        Debug.Log("error2");
    }

    void RoundTripNF() //Piège qui fait des allers-retours au sol faisant des dégâts élevés aux ennemis (sol)
    {
        Debug.Log("RoundTrip");
    }
}