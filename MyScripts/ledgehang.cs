using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgehang : MonoBehaviour
{
    [SerializeField]private Transform _grabPosition;
    private void OnTriggerEnter(Collider other){
        if(other.tag == "ledge"){
            CharacterContorl player = other.transform.GetComponentInParent<CharacterContorl>();
            if(player != null){
                if(_grabPosition != null){
                    player.GrabLedge(_grabPosition.position);
                }
            }
        }
    } 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
