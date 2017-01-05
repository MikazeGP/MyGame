using System.Collections;
using UnityEngine;

/// <summary>
/// Ubh coroutine.
/// </summary>
public class UbhCoroutine : UbhSingletonMonoBehavior<UbhCoroutine>
{
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// アクティブではないオブジェクトの継続コルーチンを開始
    /// Example : UbhCoroutine.Start (CoroutineMethod());
    /// </summary>
    public static Coroutine StartIE(IEnumerator routine)
    {
        return instance.StartCoroutine(routine);
    }
}
