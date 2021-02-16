using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform player;
    public Transform reciever;
     private bool playerisoverlapping=false;

    // Update is called once per frame
    void Update()
    {if(playerisoverlapping)
      {Vector3 portaltoplayer=player.position-transform.position;
       float dotProduct=Vector3.Dot(transform.up,portaltoplayer);

       //if this is true teleportation happens
       if(dotProduct<0f)
       {//teleport it
       float roatationDiff=-Quaternion.Angle(transform.rotation,reciever.rotation);
        roatationDiff+=180;
        player.Rotate(Vector3.up,roatationDiff);

        Vector3 positionOffset=Quaternion.Euler(0f,roatationDiff,0f)*portaltoplayer;
        player.position=reciever.position+positionOffset;

         playerisoverlapping=false;
       }
      }
    }
      void OnTriggerEnter(Collider surface)
     { if (surface.tag=="Player")
       {playerisoverlapping=true;
       }
      }
      void OnTriggerExit(Collider surface)
      {if(surface.tag=="Player")
        {playerisoverlapping=false;
        }
      }


}
