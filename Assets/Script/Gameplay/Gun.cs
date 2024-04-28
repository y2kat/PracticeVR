using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawner;
    [SerializeField] float bulletSpeed;



    void Start() {
        XRGrabInteractable _interactor = GetComponent<XRGrabInteractable>();
        _interactor.activated.AddListener(FireGun);

    }

    private void FireGun(ActivateEventArgs arg) {
        GameObject tempBullet = Instantiate(bullet, spawner.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody>().velocity = spawner.forward * bulletSpeed;
        Destroy(tempBullet, 6);
    }
}
