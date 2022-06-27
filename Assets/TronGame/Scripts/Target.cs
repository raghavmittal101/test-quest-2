using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Material OrangeGreyLight, BlueGreyLight, DiscOrange, DiscBlue;

    private void Update() {
        
        
    }


   private void OnTriggerEnter(Collider other) 
   {
        if(other.tag == "Turret")
        {
            // if((other.gameObject.name == "Turret_OrangeGrey(Clone)" && gameObject.name == "Disc_Blue(Clone)") || (other.gameObject.name == "Turret_BlueGrey(Clone)" && gameObject.name == "Disc_Orange(Clone)")){

               if((other.gameObject.GetComponentInParent<Turret>().transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterials[1] == OrangeGreyLight && gameObject.GetComponent<MeshRenderer>().sharedMaterials[1] == DiscBlue)
                 || (other.gameObject.GetComponentInParent<Turret>().transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterials[1] == BlueGreyLight && gameObject.GetComponent<MeshRenderer>().sharedMaterials[1] == DiscOrange)){ 
                Debug.Log("In target");
                if(other.gameObject.transform.parent.gameObject != null){
                    Destroy(other.gameObject.GetComponentInParent<Turret>().gameObject);
                    Destroy(gameObject,1);
                }
            }
        }

   }
}






        