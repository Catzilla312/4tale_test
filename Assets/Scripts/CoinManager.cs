using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject coin;
    public GameObject boom;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1,1,1);   
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player"){
            EventManager.EmitCoinPickUp();
            Destroy(this.gameObject);

            boom = Instantiate(boom,transform.position,Quaternion.identity);
            // Destroy(boom);
        }
    }
}
