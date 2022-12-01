using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;
    public GameObject prefab;

    RaycastHit hit;
    float groundDisntant;

    private void OnCollisionEnter(Collision other)
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
        {
            if(hit.collider.tag == "Ground")
            {
                groundDisntant = hit.distance;
            }
        }


        if (other.gameObject.TryGetComponent(out PlayerBehavior player))
        {
            player.currentHP -= damage;
            Destroy(this.gameObject);
            Instantiate(prefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (3/2 + groundDisntant), this.gameObject.transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(prefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (1/3 + groundDisntant), this.gameObject.transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
