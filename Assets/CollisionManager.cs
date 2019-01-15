using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionManager : MonoBehaviour
{
    public Text counterText, block1Text;
    public RectTransform block1TextRect, block2TextRect;
    public int collisions;
    public int blockMass;
    public BlockRigidbody blockRbody1, blockRbody2;
    public AudioSource bonkSource;
    public AudioClip bonkClip;
    public Transform block1Transform, block2Transform, wallTransform;
    // Start is called before the first frame update
    void Start()
    {
        blockRbody1.velocityX = -1;
        blockRbody1.mass = blockMass;
        block1Text.text = blockMass + " kg";
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 1000; i++)
        {
            blockRbody1.UpdateMotion();
            blockRbody2.UpdateMotion();
            block1TextRect.anchoredPosition = new Vector2(block1Transform.transform.position.x * 108f, -56);
            block2TextRect.anchoredPosition = new Vector2(block2Transform.transform.position.x * 108f, -108);
            UpdateCollision();
        }
        counterText.text = "# Collisions: " + collisions;
        
    }
    void UpdateCollision()
    {
        Vector2 block1Pos = block1Transform.position;
        Vector2 block2Pos = block2Transform.position;
        Vector2 wallPos = wallTransform.position;
        if ((wallPos.x + 0.5f > block2Pos.x - 0.5f && wallPos.x + 0.5f < block2Pos.x + 0.5f) || (wallPos.x - 0.5f < block2Pos.x + 0.5f && wallPos.x + 0.5f > block2Pos.x - 0.5f)) OnHitWall();
        if ((block1Pos.x + 0.5f > block2Pos.x - 0.5f && block1Pos.x + 0.5f < block2Pos.x + 0.5f) || (block1Pos.x - 0.5f < block2Pos.x + 0.5f && block1Pos.x + 0.5f > block2Pos.x - 0.5f)) OnHitBlock();

    }
    void OnHitBlock()
    {
        collisions++;
        PlayBonkSFX();
        Debug.Log("a");
        float m1 = blockRbody1.mass;
        float m2 = blockRbody2.mass;
        float v1 = blockRbody1.velocityX;
        float v2 = blockRbody2.velocityX;

        blockRbody1.velocityX = v1 * (m1 - m2) / (m1 + m2) + v2 * 2f * m2 / (m1 + m2);
        blockRbody2.velocityX = v2 * (m2 - m1) / (m1 + m2) + v1 * 2f * m1 / (m1 + m2);
        blockRbody1.UpdateMotion();
        blockRbody2.UpdateMotion();
    }
    void OnHitWall()
    {
        collisions++;
        PlayBonkSFX();

        blockRbody2.velocityX = -blockRbody2.velocityX;
        blockRbody2.UpdateMotion();
    }
    public void PlayBonkSFX()
    {
        bonkSource.PlayOneShot(bonkClip);
    }
}
