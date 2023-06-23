using System.Collections;
using UnityEngine;

public class MyParticleSystem : MonoBehaviour
{
    public GameObject particleObject;
    public float flowRate=1;
    float initFlowRate=1;
    public int particleNumber=5;
    public bool burst=false;
    public bool burstTrigger=false;
    public bool growOverTime = false;
    public bool colorOverTime = false;
    public float growthSpeedOnFrame = 1;
    public Vector2 size= Vector2.one;
    public Color color=Color.gray;
    public float lifeSpan=1;
    public float initalForces=1;
    void Start()
    {
        initFlowRate = flowRate;
        ObjectPool.SharedInstance.SetObjectToPool(particleObject);
        ObjectPool.SharedInstance.SetAmountToPool(particleNumber*6);
    }

    void Update()
    {
        if (burst)
        {
            if (burstTrigger)
            {
                burstTrigger = false;
                FireProjectiles();
            }
        }
        else
        {
            flowRate-=Time.deltaTime;
            if (flowRate<=0)
            {
                FireProjectiles();
                flowRate = initFlowRate;

            }
        }
    }

    private void FireProjectiles()
    {
        for (int i = 0; i < particleNumber; i++)
        {
            GameObject gm = ObjectPool.SharedInstance.GetPooledObject();
            if (gm != null)
            {
                SetUpObject(gm);
            }
        }
    }

    private void SetUpObject(GameObject gm)
    {
        gm.transform.position = gameObject.transform.position;
        gm.GetComponent<MyParticleLifeComponent>().lifeSpan = lifeSpan;
        Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
        gm.SetActive(true);
        rb.velocity = new Vector2(Random.Range(-7f, 7f), Random.Range(0f, 1f)) * initalForces;
        if (growOverTime&&colorOverTime) 
        {
            StartCoroutine(GrowObjectOverTime(gm));
            StartCoroutine(ColorObjectOverTime(gm));
        }
        else if (growOverTime)
        {
            gm.GetComponent<Renderer>().material.color = color;
            StartCoroutine(GrowObjectOverTime(gm));
        }
        else if (colorOverTime)
        {
            gm.transform.localScale = size;
            StartCoroutine(ColorObjectOverTime(gm));
        }
        else
        {
            gm.GetComponent<Renderer>().material.color = color;
            gm.transform.localScale = size;
        }
        
        
    }

    private IEnumerator ColorObjectOverTime(GameObject gm)
    {
        float temp = 0;
        gm.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, temp);
        while(gm.GetComponent<Renderer>().material.color.a<1)
        {
            gm.GetComponent<Renderer>().material.color=new Color(color.r, color.g, color.b, temp);
            temp += 0.1f;
            yield return null;
        }
    }

    private IEnumerator GrowObjectOverTime(GameObject gm)
    {
        gm.transform.localScale = Vector2.zero;
        while (size.x > gm.transform.localScale.x) 
        {
            gm.transform.localScale += new Vector3(growthSpeedOnFrame,growthSpeedOnFrame,growthSpeedOnFrame);
            yield return null;
        }
    }
}
