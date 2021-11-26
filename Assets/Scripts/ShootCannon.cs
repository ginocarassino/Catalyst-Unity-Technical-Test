using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootCannon : MonoBehaviour
{
    [Header("Controllers")]
    private MainController main_ctr;

    [Header("Transforms")]
    [SerializeField] private Transform cannonPitch;
    [SerializeField] private float rotationSpeed;

    [Header("Other Variables")]
    [SerializeField] ParticleSystem explosionCannon;
    public Transform spawnPoint;
    public GameObject bullet;
    public float forceSpeed = 5f;

    [Header("UI")]
    [SerializeField] private Slider forceSlider;
    [SerializeField] private Text forceText;

    private void Start()
    {
        main_ctr = (MainController)FindObjectOfType(typeof(MainController));
    }

    void Update()
    {
        forceSpeed = forceSlider.value;
        forceText.text = forceSlider.value.ToString("000");

        if (Input.GetKey(KeyCode.A))
        {
            RotateCannon(transform, Vector3.down);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateCannon(transform, Vector3.up);
        }
        if (Input.GetKey(KeyCode.W) && cannonPitch.localEulerAngles.x >= 30f)
        {
            RotateCannon(cannonPitch, Vector3.left);
        }
        else if (Input.GetKey(KeyCode.S) && cannonPitch.localEulerAngles.x < 80f)
        {
            RotateCannon(cannonPitch, Vector3.right);
        }
        else
        {
            RotateCannon(transform, Vector3.zero);
        }
    }

    //Shoots the bullet
    public void ShootBullet()
    {
        if (forceSpeed > 0)
        {
            main_ctr.CannonSound.Play(0);
            explosionCannon.Play();

            GameObject bull = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
            Rigidbody rig = bull.GetComponent<Rigidbody>();

            rig.AddForce(spawnPoint.right * forceSpeed, ForceMode.Impulse);
        }
    }

    private void RotateCannon(Transform _transform, Vector3 _rotation)
    {
        _transform.Rotate(_rotation * rotationSpeed * Time.deltaTime); //Rotate cannon in the vector direction pressed
    }
}
