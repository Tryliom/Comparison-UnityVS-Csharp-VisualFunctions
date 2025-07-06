using UnityEngine;
using VisualFunctions;

public class TestRandomVec2 : TestVS
{
    public Vector2Variable Vec2;
    public FloatVariable RandomRange;

    protected override void OnStart()
    {
        Vec2.Value = Vector2.zero;
    }

    protected override void Run()
    {
        var vector2 = Vec2.Value;
        
        vector2.x = Random.Range(-RandomRange.Value, RandomRange.Value);
        vector2.y = Random.Range(-RandomRange.Value, RandomRange.Value);
        
        Vec2.Value = vector2;
    }
}