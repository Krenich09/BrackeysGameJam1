using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax_script : MonoBehaviour
{
    SpriteRenderer[] spriteren=new SpriteRenderer[3];
    public Sprite bacground_image;
    public Camera your_camera;
    [SerializeField] float parallax_value;
    [SerializeField]int layer_order;
     public bool scrolling_vertical;
     Vector2 length;
     Vector3 startposition;
     public bool randomRotation;
     public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        spriteren=GetComponentsInChildren<SpriteRenderer>();
        startposition=transform.position;
        for(int i=0;i<spriteren.Length;i++)
        {
            spriteren[i].sprite=bacground_image;
            spriteren[i].sortingOrder=layer_order;
            
            if(i==1)
            {
                length=spriteren[0].bounds.size;
                if(scrolling_vertical)
                {
                    Vector3 temp_pos=spriteren[i].gameObject.transform.position;
                    temp_pos.y+=length.y;
                    spriteren[i].gameObject.transform.position=temp_pos;
                }
                else
                {
                    Vector3 temp_pos=spriteren[i].gameObject.transform.position;
                    temp_pos.x+=length.x;
                    spriteren[i].gameObject.transform.position=temp_pos;

                }
            }
            if(i==2)
            {
                if(scrolling_vertical)
                {
                    Vector3 temp_pos=spriteren[i].gameObject.transform.position;
                    temp_pos.y-=length.y;
                    spriteren[i].gameObject.transform.position=temp_pos;
                }
                else
                {
                    Vector3 temp_pos=spriteren[i].gameObject.transform.position;
                    temp_pos.x-=length.x;
                    spriteren[i].gameObject.transform.position=temp_pos;

                }
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = your_camera.transform.position + offset;
        Vector3 relative_pos = camPos * parallax_value;
        Vector3 dist = camPos - relative_pos;

        if(scrolling_vertical)
        {
            if(dist.y > startposition.y + length.y)
            {
                startposition.y += length.y;
            }

            if(dist.y<startposition.y - length.y)
            {
                startposition.y -= length.y;
            }
        }
        else
        {
             if(dist.x>startposition.x+length.x)
            {
                startposition.x+=length.x;
            }
            if(dist.x<startposition.x-length.x)
            {
                startposition.x-=length.x;
            }
        }
        relative_pos.z = 0;
        transform.position = startposition + relative_pos;
        
        
    }
}
