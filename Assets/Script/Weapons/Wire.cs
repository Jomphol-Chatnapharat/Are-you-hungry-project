using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wire : MonoBehaviour
{
    [SerializeField] ParticleSystem stunvfx;
    GameObject enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Stun the enemy
            foreach (MeshRenderer wire in transform.parent.GetComponentsInChildren<MeshRenderer>())
            {
                wire.enabled = false;
            }
            enemy = other.gameObject;
            enemy.GetComponentInChildren<Animator>().enabled = false;
            enemy.GetComponent<SimpleEnemy>().enabled = false;
            enemy.GetComponent<EnemyAI>().enabled = false;
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            ParticleSystem stun = Instantiate(stunvfx, (other.gameObject.transform.position + other.gameObject.transform.up), Quaternion.identity);
            stun.transform.parent = gameObject.transform;
            stun.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            Invoke(nameof(EndStun), 2f);
            
        }
    }
    void EndStun()
    {
        enemy.GetComponentInChildren<Animator>().enabled = true;
        enemy.GetComponent<SimpleEnemy>().enabled = true;
        enemy.GetComponent<EnemyAI>().enabled = true;
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        Destroy(transform.parent.gameObject);
    }
}
