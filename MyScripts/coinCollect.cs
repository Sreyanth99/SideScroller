using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollect : MonoBehaviour
{
    public AudioSource coinsound;
    public AudioClip coinsfx,win;
    // Start is called before the first frame update
    private int coins = 0;
    void Start()
    {
        coinsound = GetComponent<AudioSource>();
        
    }
            private void OnTriggerEnter(Collider Col){
            if(Col.gameObject.tag == "coin"){
                Debug.Log("Coin Collected!");
                coins += 1;
                Col.gameObject.SetActive(false);
                coinsound.clip = coinsfx;
                coinsound.Play();
                if(coins == 6 ){       
                    coinsound.clip = win;
                    coinsound.Play();
                    }
                }
            }



    // Update is called once per frame
    void Update()
    {


        
    }
}
