using VisualFunctions;

public class TestFloatIncrement : TestVS
{
    public FloatVariable Increment;

    protected override void OnStart()
    {
        Increment.Value = 0f;
    }

    protected override void Run()
    {
        Increment.Value++;
    }
}