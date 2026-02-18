using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BeatSpawnerScript : MonoBehaviour
{
  public GameObject BeatTarget;
  public float Radius = 1;
  
  public LayerMask noSpawnLayer;
  public LayerMask spawnLayer;
  
  public float minDistance = 3f;  
  public int maxAttempts = 1000000;

  private RhythmAudioScript conductor;

  void Update()
  {
    
  }

  public void SpawnObjectAtRandom()
  {
    
    for(int i = 0; i < maxAttempts; i++)
    {
      Vector3 randomPos = transform.position + (Vector3)Random.insideUnitCircle * Radius;
      
      //checks minimum distance for spawn
      Collider2D tooClose = Physics2D.OverlapCircle(randomPos, minDistance, spawnLayer);
      
      //prevents beats from spawning on nospawn layer
      Collider2D blockedArea = Physics2D.OverlapCircle(randomPos, 0.1f, noSpawnLayer);

      if (tooClose == null && blockedArea == null)
      {
        Instantiate(BeatTarget, randomPos, Quaternion.identity);
        return;
      }
    }
  }
  

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.green;
    
    Gizmos.DrawWireSphere(this.transform.position, Radius);
  }
}
