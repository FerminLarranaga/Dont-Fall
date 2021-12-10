using UnityEngine;

public class ImgRepeter : MonoBehaviour
{
    public Transform[] lavaBlocks;
    public float speed;

    private Vector3 targetEnd, targetStart;

    void Start()
    {
        targetStart = lavaBlocks[0].position;
        Transform lastBlock = lavaBlocks[lavaBlocks.Length - 1];
        float widthBlock = lastBlock.localPosition.x;
        targetEnd = new Vector3(lastBlock.position.x + widthBlock, lastBlock.position.y, lastBlock.position.z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float fixedSpeed = speed * Time.deltaTime;
        foreach(Transform t in lavaBlocks)
        {
            t.position = Vector3.MoveTowards(t.position, targetEnd, fixedSpeed);

            if (t.position == targetEnd)
            {
                t.position = targetStart;
            }
        }
    }
}
