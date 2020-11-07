using UnityEngine;

public class ThirdPersonControler : MonoBehaviour
{
    float h, v;
    public float speed, turnSmooth, acceleration;
    Transform cam;
    CharacterController charaCtrl;
    // Update is called once per frame
    private void Start()
    {
        cam = Camera.main.transform;
        charaCtrl = GetComponent<CharacterController>();
    }
    void Update()
    {
        //Mouvement
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 desiredVelocity = new Vector3(h, 0, v); //Vecteur de mouvement
        Vector3 velocityNormalized = desiredVelocity.normalized; //Normale du Vecteur de mouvement

        float test = h + v;

        if (velocityNormalized != Vector3.zero)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self); //Mvt du perso
            //transform.Translate(velocityNormalized * speed * Time.deltaTime, Space.Self); //Mvt du perso

            //Rotation

            float targetRotationY = Mathf.Atan2(h, v) * Mathf.Rad2Deg + cam.eulerAngles.y; //Angle de rotation du perso

            Vector3 rotation = new Vector3(transform.rotation.x, targetRotationY, transform.rotation.z); //Vecteur de rotation

            transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(rotation), turnSmooth); //Rotation
        }
    }
}
