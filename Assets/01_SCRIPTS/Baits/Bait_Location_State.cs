using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait_Location_State : MonoBehaviour
{
    bool occupied;
    public Animator locationAnm;
    public MeshRenderer mshRndr;
    public Material[] locationMats;

    public void SetLocationState(bool _state)
    {
        if (_state)
        {
            if (occupied)
            {
                mshRndr.material = locationMats[2];
                locationAnm.SetBool("Selected", false);
            }
            else
            {
                mshRndr.material = locationMats[1];
                locationAnm.SetBool("Selected", true);
            }
        }
        else
        {
            mshRndr.material = locationMats[0];
            locationAnm.SetBool("Selected", false);
        }
    }

    public void SetOccupation(bool _occupied)
    {
        if (_occupied)
        {
            SetLocationState(true);
        }
        else
        {
            SetLocationState(false);
        }
        occupied = _occupied;
    }
}
