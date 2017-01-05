﻿using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Ubh random lock on shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/Random Shot (Lock On)")]
public class UbhRandomLockOnShot : UbhRandomShot
{
    [Header("===== RandomLockOnShot Settings =====")]
    // タグ名でターゲットを設定
    [FormerlySerializedAs("_SetTargetFromTag")]
    public bool m_setTargetFromTag = true;
    // SetTargetFromTagを使用してターゲットの一意のタグ名を設定します。
    [FormerlySerializedAs("_TargetTagName")]
    public string m_targetTagName = "Player";
    // ターゲットのロックの変換
    // タグでターゲットを指定する必要はありません。
    // RandomCenterAngleをTransform.positionのターゲットの方向に上書きします
    [FormerlySerializedAs("_TargetTransform")]
    public Transform m_targetTransform;
    // "Always aim to target."
    [FormerlySerializedAs("_Aiming")]
    public bool m_aiming;

    public override void Shot()
    {
        if (m_shooting)
        {
            return;
        }

        AimTarget();

        if (m_targetTransform == null)
        {
            Debug.LogWarning("Cannot shot because TargetTransform is not set.");
            return;
        }

        base.Shot();

        if (m_aiming)
        {
            StartCoroutine(AimingCoroutine());
        }
    }

    private void AimTarget()
    {
        if (m_targetTransform == null && m_setTargetFromTag)
        {
            m_targetTransform = UbhUtil.GetTransformFromTagName(m_targetTagName);
        }
        if (m_targetTransform != null)
        {
            m_randomCenterAngle = UbhUtil.GetAngleFromTwoPosition(transform, m_targetTransform, shotCtrl.m_axisMove);
        }
    }

    private IEnumerator AimingCoroutine()
    {
        while (m_aiming)
        {
            if (m_shooting == false)
            {
                yield break;
            }

            AimTarget();

            yield return 0;
        }
    }
}