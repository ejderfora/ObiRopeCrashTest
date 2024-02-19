using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanFire : MonoBehaviour
{
    
    public ParticleSystem fire;
    public AudioSource PlayerSFXSource;
    public AudioClip FireWhooshSFX;

    private bool isSoundPlayed = false;

   

    public void OnParticleCollision(GameObject other)
    {
        Debug.Log("buraya kadar");
        if (other.tag == "Particle")
        {
            
            
            
            if(fire.transform.localScale.y<1.5f)
            {
                fire.transform.localScale *= 1.03f;
                //fire.transform.localScale += new Vector3(count/10000,count/10000 , count/10000);

            }
            else
            {
                fire.transform.localScale = new Vector3 (2,2,2);
            }


        }
    }
    
    void PlayOilFireExplosion(){

        

        if(isSoundPlayed == false){
            PlayerSFXSource.PlayOneShot(FireWhooshSFX);
            isSoundPlayed = true;
        }else{
            Debug.Log("Already Played");
        }

    }
}
