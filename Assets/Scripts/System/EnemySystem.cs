using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


[AlwaysSynchronizeSystem]
public class EnemySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {  
        float deltaTime = Time.DeltaTime;
        Translation playerTranslation = new Translation();
        
        Entities
            .WithName("PlayerComponent")
            .ForEach((ref Translation translation, in PlayerComponent player) =>
            {
                playerTranslation = translation;
            }).Run();
        
        Entities
            .WithName("EnemyComponent")
            .ForEach((ref Translation translation, in EnemyComponent enemy) =>
            {
                var dir = playerTranslation.Value - translation.Value;
                dir = math.normalize(dir);
                
                translation.Value += (float3)dir * 8f * deltaTime;
            }).Run();
        
        return default;
    }
}
