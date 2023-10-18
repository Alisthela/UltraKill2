using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodParticleCollision : MonoBehaviour
{
    public ParticleSystem bloodParticleSystem;
    public GameObject bloodDecal;
    public GameObject bloodEffectGroup;
    public Transform parent;
    private List<ParticleCollisionEvent> collisionEvents;

    public void Start()
    {
        bloodParticleSystem = GetComponent<ParticleSystem>();
        parent = GetComponentInParent<Transform>();
        bloodDecal = GetComponentInParent<EnemyCommon>().enemyBloodDecal;
        collisionEvents = new List<ParticleCollisionEvent>();
        bloodEffectGroup = GameObject.Find("BloodEffectGroup");
        this.transform.parent = bloodEffectGroup.transform;
        this.transform.parent.localScale = new Vector3(1f, 1f, 1f);

        Destroy(this.gameObject, 10f);
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, parent.rotation.z * -1.0f);
        this.transform.position = parent.position;
    }

    private void OnParticleCollision(GameObject other)
    {
        int collisionEventsAmount = bloodParticleSystem.GetCollisionEvents(other, collisionEvents);

        if(other.tag == "Player")
        {
            var playerHealth = other.GetComponent<TestHealth>();
            playerHealth.playerHealth += 2;

            if (playerHealth.playerHealth > 100)
            {
                playerHealth.playerHealth = 100;
            }
        }
        else if(other.tag == "Terrain")
        {
            var chance = Random.Range(0, 100);

            var randomOne = Random.Range(0.5f, 1f);
            var randomTwo = Random.Range(0f, 0.1f);
            var randomThree = Random.Range(0f, 0.1f);

            var randomColor = new Color(randomOne, randomTwo, randomThree, 0.6f);

            if (chance > 95)
            {
                foreach (ParticleCollisionEvent particle in collisionEvents)
                {
                    var decal = Instantiate(bloodDecal, particle.intersection, Quaternion.Euler(0, Random.Range(0, 360), other.transform.rotation.z));
                    decal.transform.localScale = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));
                    decal.transform.parent = this.gameObject.transform;

                    var decalRenderer = decal.GetComponent<Renderer>();
                    decalRenderer.material.SetColor("_Color", randomColor);
                    Destroy(decal, 6);
                }
            }
        }
    }
}
