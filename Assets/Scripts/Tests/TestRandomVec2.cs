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
        Vec2.Value = new Vector2(Random.Range(-RandomRange.Value, RandomRange.Value), Random.Range(-RandomRange.Value, RandomRange.Value));
    }
}