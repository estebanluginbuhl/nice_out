using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Forsee : MonoBehaviour
{
    public LayerMask floor;
    public LayerMask traps;
    public MeshFilter mshFlt;
    public MeshRenderer mshRnd;
    public Trap_Inventory trapInventory;
    public Trap_Manager trapManager;
    public bool switchMode;
    public Vector3 colliderCube;

    public Material[] mat;

    public float offset;

    public bool detectCollision;
    // Start is called before the first frame update
    void Start()
    {
        mshFlt = GetComponent<MeshFilter>();
        mshRnd = GetComponent<MeshRenderer>();
        trapInventory = GameObject.Find("UI_Manager").GetComponent<Trap_Inventory>();
        trapManager = GameObject.Find("ThirdPersonController").GetComponent<Trap_Manager>();
        switchMode = GameObject.Find("ThirdPersonController").GetComponent<Switch_Mode>().mode;
    }

    // Update is called once per frame
    void Update()
    {
    
        switchMode = GameObject.Find("ThirdPersonController").GetComponent<Switch_Mode>().mode;
        if (switchMode == true && trapInventory != null && trapInventory.trapsItem.Length != 0)
        {
            Traps trap = trapInventory.trapsItem[trapInventory.selectedSlotIndex].GetComponent<Traps>();
            mshFlt.mesh = trap.trapAndUpgrades[0].GetComponent<MeshFilter>().sharedMesh;

            colliderCube = (trap.colliderSize)/2;
            offset = trap.offsetPositions[0];

            Collider[] boxCollider = Physics.OverlapBox(transform.position + Vector3.up * offset, colliderCube, transform.rotation, floor | traps);

            if (boxCollider.Length > 0)
            {

                foreach(Collider c in boxCollider)
                {
                    Debug.Log(c.gameObject.name);
                }

            }
            if (boxCollider.Length != 0)
            {
                if (detectCollision == false)
                {
                    detectCollision = true;
                }
            }
            else
            {
                if (detectCollision == true)
                {
                    detectCollision = false;
                }
            }

            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.Rotate(trapManager.floorInclinaison);
            transform.Rotate(new Vector3(0, 1, 0), trapManager.trapOrientation, Space.Self);
            
            transform.position = trapManager.trapPosition;
        }
        else
        {
            if (mshFlt.mesh)
            {
                mshFlt.mesh = null;
            }
        }

        if(detectCollision == true)
        {
            mshRnd.material = mat[1];
        }
        else
        {
            mshRnd.material = mat[0];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.up * offset, colliderCube * 2);
        Debug.Log(transform.rotation);
    }
}
