using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;

[AlwaysSynchronizeSystem]
public class PlayerSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir += Vector3.left;
        }

        Entities
            .WithName("PlayerComponent")
            .ForEach((ref Translation translation, in PlayerComponent player) =>
            {
                translation.Value += (float3)dir * 10f * deltaTime;
            }).Run();
        
        return default;
    }
}
