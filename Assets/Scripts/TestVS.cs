using UnityEngine;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using VisualFunctions;
using CustomEvent = Unity.VisualScripting.CustomEvent;

public class TestVS : MonoBehaviour
{
    public int TestNumber = 1000000;
    public GameObject TargetObject;
    public KeyCode StartKey = KeyCode.Space;
    
    [Header("Visual Functions to Test")]
    public Functions TestedFunctions;

    private void RunTests()
    {
        if (!TargetObject)
        {
            UnityEngine.Debug.LogError("TargetObject is not set. Please assign a GameObject to test the event.");
            return;
        }

        CustomEvent.Trigger(TargetObject, "Start");
        
        var visualScriptTime = new Stopwatch();
        visualScriptTime.Start();

        for (var i = 0; i < TestNumber; i++)
        {
            CustomEvent.Trigger(TargetObject, "Run");
        }

        visualScriptTime.Stop();
        
        OnStart();
        
        var visualFunctionsTime = new Stopwatch();
        visualFunctionsTime.Start();
        
        for (var i = 0; i < TestNumber; i++)
        {
            TestedFunctions.Invoke();
        }
        
        visualFunctionsTime.Stop();
        
        var codeTime = new Stopwatch();
        codeTime.Start();
        
        for (var i = 0; i < TestNumber; i++)
        {
            Run();
        }
        
        codeTime.Stop();
        
        var vsElapsed = visualScriptTime.ElapsedTicks / 10000f;
        var vfElapsed = visualFunctionsTime.ElapsedTicks / 10000f;
        var codeElapsed = codeTime.ElapsedTicks / 10000f;

        UnityEngine.Debug.Log($"{GetType().Name}: " +
                              $"Unity VS {vsElapsed} ms " +
                              $"| Visual Functions {vfElapsed} ms " +
                              $"| Code {codeElapsed} ms\n" +
                              $"Unity VS is {vsElapsed / codeElapsed}x slower than Code\n" +
                              $"Unity VS is {vsElapsed / vfElapsed}x slower than Visual Functions\n" +
                              $"Visual Functions is {vfElapsed / codeElapsed}x slower than Code");
    }

    private void Update()
    {
        if (Input.GetKeyDown(StartKey))
        {
            RunTests();
        }
    }
    
    protected virtual void OnStart() {}
    
    [MethodImpl(MethodImplOptions.NoOptimization)]
    protected virtual void Run() {}
}