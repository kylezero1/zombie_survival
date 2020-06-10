using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TreeDropController : MonoBehaviour
{
    int health = 4;

    private Transform treeTransform;

    // Start is called before the first frame update
    void Start()
    {
        treeTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            if (health <= 0)
            {
                Vector3 randomDir = UtilsClass.GetRandomDir();
                Instantiate(ItemAssets.Instance.pfItemWorldWoodSpawner, treeTransform.position + randomDir * .7f, Quaternion.identity);                
                Destroy(gameObject);
            }
        }
    }
}
