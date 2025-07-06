using System.Collections.Generic;
using VisualFunctions;

public class TestListLoop : TestVS
{
    public ListOfVariable IntegerList;

    protected override void OnStart()
    {
        IntegerList.Value.SetList(typeof(int));
    }

    protected override void Run()
    {
        var list = IntegerList.Value.ListValue.Value as List<int>;
        
        list.Add(list.Count);
    }
}