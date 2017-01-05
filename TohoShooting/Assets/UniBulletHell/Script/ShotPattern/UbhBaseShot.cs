using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// Ubh base shot.
/// 各ショットパターンクラスはこのクラスを継承します。
/// </summary>
public abstract class UbhBaseShot : UbhMonoBehaviour
{
    [Header("===== Common Settings =====")]
    // ショットの弾丸プレハブを設定（スプライトまたはモデルなど）
    [FormerlySerializedAs("_BulletPrefab")]
    public GameObject m_bulletPrefab;
    // ショットの弾数を設定。
    [FormerlySerializedAs("_BulletNum")]
    public int m_bulletNum = 10;
    // ショットの弾丸の基本速度を設定
    [FormerlySerializedAs("_BulletSpeed")]
    public float m_bulletSpeed = 2f;
    // 弾丸速度の加速を設定
    [FormerlySerializedAs("_AccelerationSpeed")]
    public float m_accelerationSpeed = 0f;
    // 弾丸の回転を加速させる
    [FormerlySerializedAs("_AccelerationTurn")]
    public float m_accelerationTurn = 0f;
    // このフラグは、指定した時刻に弾を一時停止して再開します
    [FormerlySerializedAs("_UsePauseAndResume")]
    public bool m_usePauseAndResume = false;
    // 弾丸を一時停止する時間を設定
    [FormerlySerializedAs("_PauseTime")]
    public float m_pauseTime = 0f;
    // 弾丸を再開する時間を設定します
    [FormerlySerializedAs("_ResumeTime")]
    public float m_resumeTime = 0f;
    // このフラグは、指定した時刻に弾丸GameObjectを自動的に解放します
    [FormerlySerializedAs("_UseAutoRelease")]
    public bool m_useAutoRelease = false;
    // UseAutoReleaseを使用してショットの後に自動的に解放する時間を設定します（秒）
    [FormerlySerializedAs("_AutoReleaseTime")]
    public float m_autoReleaseTime = 10f;

    [Space(10)]

    // ショット後にコールバックメソッドを設定
    [SerializeField]
    private UnityEvent m_shotFinishedCallbackEvents = new UnityEvent();

    protected bool m_shooting;

    private UbhShotCtrl m_shotCtrl;

    protected UbhShotCtrl shotCtrl
    {
        get
        {
            if (m_shotCtrl == null)
            {
                m_shotCtrl = transform.GetComponentInParent<UbhShotCtrl>();
            }
            return m_shotCtrl;
        }
    }

    /// <summary>
    /// 発射フラグ
    /// </summary>
    public bool shooting { get { return m_shooting; } }

    /// <summary>
    /// 継承クラスでのOnDisableメソッドのオーバーライドからの呼び出し。
    /// Example : protected override void OnDisable () { base.OnDisable (); }
    /// </summary>
    protected virtual void OnDisable()
    {
        m_shooting = false;
    }

    /// <summary>
    /// Abstract shot method.
    /// </summary>
    public abstract void Shot();

    /// <summary>
    /// UbhShotCtrl setter.
    /// </summary>
    public void SetShotCtrl(UbhShotCtrl shotCtrl)
    {
        m_shotCtrl = shotCtrl;
    }

    /// <summary>
    /// Finished shot.
    /// </summary>
    protected void FinishedShot()
    {
        m_shotFinishedCallbackEvents.Invoke();
        m_shooting = false;
    }

    /// <summary>
    /// オブジェクトプールからUbhBulletオブジェクトを取得
    /// </summary>
    protected UbhBullet GetBullet(Vector3 position, Quaternion rotation, bool forceInstantiate = false)
    {
        if (m_bulletPrefab == null)
        {
            Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
            return null;
        }

        // ObjectPoolからUbhBulletを取得する
        UbhBullet bullet = UbhObjectPool.instance.GetBullet(m_bulletPrefab, position, rotation, forceInstantiate);
        if (bullet == null)
        {
            return null;
        }

        return bullet;
    }

    /// <summary>
    /// Shot UbhBullet object.
    /// </summary>
    protected void ShotBullet(UbhBullet bullet, float speed, float angle,
                               bool homing = false, Transform homingTarget = null, float homingAngleSpeed = 0f,
                               bool wave = false, float waveSpeed = 0f, float waveRangeSize = 0f)
    {
        if (bullet == null)
        {
            return;
        }
        bullet.Shot(speed, angle, m_accelerationSpeed, m_accelerationTurn,
                    homing, homingTarget, homingAngleSpeed,
                    wave, waveSpeed, waveRangeSize,
                    m_usePauseAndResume, m_pauseTime, m_resumeTime,
                    shotCtrl != null ? shotCtrl.m_axisMove : UbhUtil.AXIS.X_AND_Y);
    }

    /// <summary>
    /// Auto release bullet GameObject.
    /// </summary>
    protected void AutoReleaseBullet(UbhBullet bullet)
    {
        if (m_useAutoRelease == false || m_autoReleaseTime < 0f)
        {
            return;
        }
        UbhCoroutine.StartIE(AutoReleaseBulletCoroutine(bullet));
    }

    private IEnumerator AutoReleaseBulletCoroutine(UbhBullet bullet)
    {
        float countUpTime = 0f;

        while (true)
        {
            if (bullet == null || bullet.gameObject == null || bullet.gameObject.activeSelf == false)
            {
                yield break;
            }

            if (m_autoReleaseTime <= countUpTime)
            {
                break;
            }

            yield return 0;

            countUpTime += UbhTimer.instance.deltaTime;
        }

        UbhObjectPool.instance.ReleaseBullet(bullet);
    }
}