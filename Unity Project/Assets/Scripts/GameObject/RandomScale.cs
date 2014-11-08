using UnityEngine;
using UnityEditor;

class RandomScale : MonoBehaviour
{
    public bool LockXAxis = true;
    public Vector2 XAxisRangeLimit = new Vector2(0.5f, 2.0f);
    public bool LockYAxis = true;
    public Vector2 YAxisRangeLimit = new Vector2(0.5f, 2.0f);
    public bool LockZAxis = true;
    public Vector2 ZAxisRangeLimit = new Vector2(0.5f, 2.0f);
    
    public bool UseSeed = false;
    public int Seed = 0;
    
    public bool TestRandom = false;
    public float TestEach = 0.5f;
    public static float TestTimer = 0.0f;

    public void Awake()
    {
        Randomize();
    }
    
    private void Randomize()
    {
        if (UseSeed)
        {
            Random.seed = Seed;
        }
        else
        {
            Random.seed = Time.frameCount;
        }
        
        float randomXAxis = Random.Range(XAxisRangeLimit.x, XAxisRangeLimit.y);
        float randomYAxis = Random.Range(YAxisRangeLimit.x, YAxisRangeLimit.y);
        float randomZAxis = Random.Range(ZAxisRangeLimit.x, ZAxisRangeLimit.y);
        
        if (LockXAxis)
        {
            randomXAxis = transform.localScale.x;
        }
        if (LockYAxis)
        {
            randomYAxis = transform.localScale.y;
        }
        if (LockZAxis)
        {
            randomZAxis = transform.localScale.z;
        }
        
        transform.localScale = new Vector3(randomXAxis, randomYAxis, randomZAxis);
    }
    
    public void OnDrawGizmosSelected()
    {
        if (TestRandom)
        {
            TestTimer += Time.deltaTime;
            if(TestTimer > TestEach)
            {
                TestTimer = 0.0f;
                Randomize();
            }
        }
    }
}
