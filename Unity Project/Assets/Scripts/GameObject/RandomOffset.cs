using UnityEngine;

class RandomOffset : MonoBehaviour
{
    public bool LockXAxis = true;
    public Vector2 XAxisRangeLimit = new Vector2(-1.0f, 1.0f);
    public bool LockYAxis = true;
    public Vector2 YAxisRangeLimit = new Vector2(-1.0f, 1.0f);
    public bool LockZAxis = true;
    public Vector2 ZAxisRangeLimit = new Vector2(-1.0f, 1.0f);
    
    public bool UseSeed = false;
    public int Seed = 0;
    
    public bool TestRandom = false;
    public float TestEach = 0.5f;
    public static float TestTimer = 0.0f;

    private Vector3 basePosition;

    public void Awake()
    {
        basePosition = transform.position;
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
        
        float randomXAxis = basePosition.x + Random.Range(XAxisRangeLimit.x, XAxisRangeLimit.y);
        float randomYAxis = basePosition.y + Random.Range(YAxisRangeLimit.x, YAxisRangeLimit.y);
        float randomZAxis = basePosition.z + Random.Range(ZAxisRangeLimit.x, ZAxisRangeLimit.y);
        
        if (LockXAxis)
        {
            randomXAxis = basePosition.x;
        }
        if (LockYAxis)
        {
            randomYAxis = basePosition.y;
        }
        if (LockZAxis)
        {
            randomZAxis = basePosition.z;
        }
        
        transform.position = new Vector3(randomXAxis, randomYAxis, randomZAxis);
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
