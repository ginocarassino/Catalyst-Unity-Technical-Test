using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootCannon : MonoBehaviour
{
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

    [Header("Sounds")]
    [SerializeField] private AudioSource CannonSound;

    void Update()
    {
        forceSpeed = forceSlider.value;

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

    public void ShootBullet()
    {
        CannonSound.Play(0);
        explosionCannon.Play();

        GameObject bull = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
        Rigidbody rig = bull.GetComponent<Rigidbody>();

        rig.AddForce(spawnPoint.right * forceSpeed, ForceMode.Impulse);
    }

    private void RotateCannon(Transform _transform, Vector3 _rotation)
    {
        _transform.Rotate(_rotation * rotationSpeed * Time.deltaTime);
    }
}
