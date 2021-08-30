using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public static EnemyPooling enemyPooling;

    [SerializeField] GameObject objectToPool;
    [SerializeField] float cuantityToPool;

    [SerializeField] List<GameObject> objectPool;

    // Start is called before the first frame update
    void Awake()
    {
        enemyPooling = this;

        for (int i = 0; i < cuantityToPool; i++)//instancia una cantidad de objetos marcada
        {
            GameObject tmp = Instantiate(objectToPool);//guarda el objeto en una variable temporal
            tmp.SetActive(false);//desactiva el objeto
            tmp.transform.SetParent(transform);//hace que el objeto sea su hijo
            objectPool.Add(tmp);//agrega el objeto a una lista 
        }
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)//devuelve el objeto
    {
        for (int i = 0; i < objectPool.Count; i++)// recorre la lista en busca de un objeto desactivado
        {
            if(!objectPool[i].activeSelf)//si el objeto esta desacrivado
            {
                objectPool[i].SetActive(true);//activa el objeto
                objectPool[i].transform.position = position;//lo mueve a la posicion marcada
                objectPool[i].transform.rotation = rotation;//le pone la rotacion marcada

                return objectPool[i];//devuelve el objeto y cierra el metodo
            }
        }
        //si todos estan activos
        GameObject tmp = Instantiate(objectToPool, position, rotation);//instancia un nuevo objeto y lo guarda en tmp
        objectPool.Add(tmp);//lo guarad en la lista
        tmp.transform.SetParent(transform);//lo hace su hijo
        return tmp;// devuelve el objeto
    }
}
