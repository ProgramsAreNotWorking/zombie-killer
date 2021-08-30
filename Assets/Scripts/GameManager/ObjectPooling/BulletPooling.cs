using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling bulletPooling;

    [SerializeField] GameObject objectToPool;
    [SerializeField] float cuantityToPool;

    [SerializeField] List<GameObject> objectPool;

    // Start is called before the first frame update
    void Awake()
    {
        bulletPooling = this;

        for (int i = 0; i < cuantityToPool; i++)
        {
            GameObject tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            tmp.transform.SetParent(transform);
            objectPool.Add(tmp);
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if(!objectPool[i].activeSelf)
            {
                objectPool[i].SetActive(true);
                objectPool[i].transform.position = position;
                objectPool[i].transform.rotation = rotation;

                return objectPool[i];
            }
        }

        GameObject tmp = Instantiate(objectToPool, position, rotation);
        objectPool.Add(tmp);
        tmp.transform.SetParent(transform);
        return tmp;
    }
}
