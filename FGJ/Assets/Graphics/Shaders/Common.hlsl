float random(float3 input)
{
    return frac(sin(dot(input.xyz, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
}