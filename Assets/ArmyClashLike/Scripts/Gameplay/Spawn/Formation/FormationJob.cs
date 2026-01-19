using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ArmyClashLike.Gameplay
{
    [BurstCompile]
    public struct FormationJob : IJob
    {
        [ReadOnly] public float3 center;
        [ReadOnly] public int count;
        [ReadOnly] public FormationType type;
        [ReadOnly] public float spacing;
        [ReadOnly] public quaternion rotation;

        [WriteOnly] public NativeArray<float3> positions;

        public void Execute()
        {
            if (count <= 0)
            {
                return;
            }
            
            var rotationMatrix = new float4x4(rotation, float3.zero);

            switch (type)
            {
                case FormationType.Line:
                    GenerateLine(rotationMatrix);
                    break;
                case FormationType.Rectangle:
                    GenerateRectangle(rotationMatrix);
                    break;
                case FormationType.Circle:
                    GenerateCircle(rotationMatrix);
                    break;
            }
        }

        private void GenerateLine(float4x4 rotMatrix)
        {
            float halfLength = (count - 1f) * spacing * 0.5f;

            // Направление по умолчанию — вперёд по Z
            float3 forward = math.mul(rotMatrix, new float4(0, 0, 1, 0)).xyz;
            forward = math.normalize(forward);

            for (int i = 0; i < count; i++)
            {
                float offset = i * spacing - halfLength;
                float3 localPos = offset * forward;
                positions[i] = center + localPos;
            }
        }

        private void GenerateRectangle(float4x4 rotMatrix)
        {
            var sqrtCount = math.sqrt(count);
            var side = (int)math.ceil(sqrtCount);
            var rows = side;
            var cols = (count + side - 1) / side;

            var halfWidth = (cols - 1f) * spacing * 0.5f;
            var halfDepth = (rows - 1f) * spacing * 0.5f;

            var index = 0;
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    if (index >= count)
                    {
                        break;
                    }

                    var x = col * spacing - halfWidth;
                    var z = row * spacing - halfDepth;
                    
                    var localPos = new float3(x, 0, z);
                    var worldPos = math.mul(rotMatrix, new float4(localPos, 0)).xyz;

                    positions[index] = center + worldPos;
                    index++;
                }
            }
        }

        private void GenerateCircle(float4x4 rotMatrix)
        {
            if (count == 1)
            {
                positions[0] = center;
                return;
            }

            var angleStep = 2f * math.PI / count;
            var radius = spacing / (2f * math.sin(math.PI / count));

            for (var i = 0; i < count; i++)
            {
                var angle = i * angleStep;
                var localPos = new float3(
                    radius * math.cos(angle),
                    0f,
                    radius * math.sin(angle)
                );

                var worldPos = math.mul(rotMatrix, new float4(localPos, 0)).xyz;
                positions[i] = center + worldPos;
            }
        }
    }
}