using System.Collections.Generic;
using VisualFunctions;

public class TestListMultiply : TestVS
{
    public ListOfVariable PairSourceList;
    public ListOfVariable ResultList;
    public IntVariable PairCount;

    protected override void OnStart()
    {
        PairSourceList.Value.SetList(typeof(float));
        ResultList.Value.SetList(typeof(float));
        
        var sourceList = PairSourceList.Value.ListValue.Value as List<float>;
        
        for (var i = 0; i < PairCount.Value; i++)
        {
            sourceList.Add(i);
            sourceList.Add(i + 1);
        }
    }

    protected override void Run()
    {
        var sourceList = PairSourceList.Value.ListValue.Value as List<float>;
        var resultList = ResultList.Value.ListValue.Value as List<float>;
        
        resultList.Clear();

        for (var i = 0; i < sourceList.Count; i += 2)
        {
            resultList.Add(sourceList[i] * sourceList[i + 1]);
        }
    }
}