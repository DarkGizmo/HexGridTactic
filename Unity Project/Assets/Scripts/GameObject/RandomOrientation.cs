using UnityEngine;
using UnityEditor;

class RandomOrientation : MonoBehaviour
{
    public bool LockXAxis = true;
    public Vector2 XAxisRangeLimit = new Vector2(-180.0f, 180.0f);
    public bool LockYAxis = true;
    public Vector2 YAxisRangeLimit = new Vector2(-180.0f, 180.0f);
    public bool LockZAxis = true;
    public Vector2 ZAxisRangeLimit = new Vector2(-180.0f, 180.0f);

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
            randomXAxis = transform.eulerAngles.x;
        }
        if (LockYAxis)
        {
            randomYAxis = transform.eulerAngles.y;
        }
        if (LockZAxis)
        {
            randomZAxis = transform.eulerAngles.z;
        }
        
        transform.eulerAngles = new Vector3(randomXAxis, randomYAxis, randomZAxis);
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
