using UnityEngine;

/// <summary>
/// Ubh timer.
/// </summary>
public class UbhTimer : UbhSingletonMonoBehavior<UbhTimer>
{
    private float m_deltaTime;
    private float m_deltaFrameCount;
    private float m_frameCount;
    private bool m_pausing;

    /// <summary>
    /// UniBulletHellのデルタ時間を取得します。
    /// </summary>
    public float deltaTime { get { return m_pausing ? 0f : m_deltaTime; } }

    /// <summary>
    /// UniBulletHellのデルタフレーム数を取得します。
    /// </summary>
    public float deltaFrameCount { get { return m_pausing ? 0f : m_deltaFrameCount; } }

    /// <summary>
    /// UniBulletHellのフレーム数を取得します。
    /// </summary>
    public float frameCount { get { return m_frameCount; } }

    /// <summary>
    /// 一時停止フラグを取得する
    /// </summary>
    public bool pausing { get { return m_pausing; } }

    protected override void Awake()
    {
        base.Awake();
        UpdateTimes();
    }

    private void Update()
    {
        UpdateTimes();
        UbhBulletManager.instance.UpdateBullets();
    }

    private void UpdateTimes()
    {
        m_deltaTime = Time.deltaTime;
        m_deltaFrameCount = m_deltaTime / (1f / Application.targetFrameRate);

        if (m_pausing == false)
        {
            m_frameCount += m_deltaFrameCount;
        }
    }

    /// <summary>
    /// UniBulletHellの休止時間
    /// </summary>
    public void Pause()
    {
        m_pausing = true;
    }

    /// <summary>
    /// UniBulletHellの時間を再開する
    /// </summary>
    public void Resume()
    {
        m_pausing = false;
    }
}