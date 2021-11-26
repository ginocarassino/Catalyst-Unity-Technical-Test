using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCollision : MonoBehaviour
{
    private MainController main_ctr;
    private Rigidbody m_Rigidbody;
    [SerializeField] ParticleSystem Explosion;
    [SerializeField] GameObject Sphere;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        main_ctr = (MainController)FindObjectOfType(typeof(MainController));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "YellowTarget")
        {
            main_ctr.points += 50;

            StartCoroutine(DestroyRoutine(other));
        }
        else if (other.gameObject.tag == "RedTarget")
        {
            main_ctr.points += 30;

            StartCoroutine(DestroyRoutine(other));
        }
        else if (other.gameObject.tag == "WhiteTarget")
        {
            main_ctr.points += 10;

            StartCoroutine(DestroyRoutine(other));
        }
        else if (other.gameObject.tag == "OutCollision")
        {
            StartCoroutine(DestroyOutRoutine());
        }

        main_ctr.pointsText.text = main_ctr.points.ToString();
    }

    IEnumerator DestroyRoutine(Collider other)
    {
        main_ctr.CollectSound.Play(0);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        Explosion.Play();
        main_ctr.spawned[int.Parse(other.transform.parent.gameObject.name)] = 0;
        Destroy(other.transform.parent.gameObject);
        Destroy(Sphere);

        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    IEnumerator DestroyOutRoutine()
    {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        Destroy(Sphere);

        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
