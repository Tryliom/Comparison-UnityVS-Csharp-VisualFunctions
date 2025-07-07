using System;
using UnityEngine;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Profiling;
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
        
        var visualScript = new Counter(() => CustomEvent.Trigger(TargetObject, "Run"));

        for (var i = 0; i < TestNumber; i++)
        {
            visualScript.Test();
        }

        var visualScriptTime = visualScript.GetTime();
        var visualScriptMemory = visualScript.GetAllocatedMemoryMb();
        
        OnStart();
        
        var visualFunctions = new Counter(() => TestedFunctions.Invoke());
        
        for (var i = 0; i < TestNumber; i++)
        {
            visualFunctions.Test();
        }
        
        var visualFunctionsTime = visualFunctions.GetTime();
        var visualFunctionsMemory = visualFunctions.GetAllocatedMemoryMb();
        
        var code = new Counter(Run);
        
        for (var i = 0; i < TestNumber; i++)
        {
            code.Test();
        }
        
        var codeTime = code.GetTime();
        var codeMemory = code.GetAllocatedMemoryMb();

        UnityEngine.Debug.Log($"{GetType().Name}: " +
                              $"Unity VS {visualScriptTime} ms ({visualScriptMemory} MB) " +
                              $"| Visual Functions {visualFunctionsTime} ms ({visualFunctionsMemory} MB) " +
                              $"| Code {codeTime} ms ({codeMemory} MB)");
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
    
    private class Counter
    {
        private readonly Stopwatch _stopwatch;
        private readonly Action _testedAction;
        private Recorder _rec;

        public Counter(Action testedAction)
        {
            _stopwatch = Stopwatch.StartNew();
            _testedAction = testedAction;
            _rec = Recorder.Get("GC.Alloc");
            _rec.enabled = false;
#if !UNITY_WEBGL
            _rec.FilterToCurrentThread();
#endif
            _rec.enabled = true;
        }

        public void Test()
        {
            _testedAction?.Invoke();
        }

        public float GetTime()
        {
            _stopwatch.Stop();
            return _stopwatch.ElapsedTicks / 10000f; // Convert ticks to milliseconds
        }
            
        public float GetAllocatedMemoryMb()
        {
            if (_rec == null) return 0;

            _rec.enabled = false;
#if !UNITY_WEBGL
            _rec.CollectFromAllThreads();
#endif

            var res = _rec.sampleBlockCount;
                
            _rec = null;
                
            return res / (1024f * 1024f);
        }
    }
}