// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable
using System;
using UnityEngine;

namespace SDKabu.KCore
{
    public class KCoolDownInjector : MonoBehaviour
    {
        private void Awake()
        {
            GameObject cdGameObject = new(nameof(KCoolDownSystem));
            cdGameObject.transform.SetParent(transform);
            KCoolDownSystem cdSystem = cdGameObject.AddComponent<KCoolDownSystem>();
            KServiceInjection.Add<IKCoolDown>(cdSystem);
        }

        private void OnDestroy()
        {
            KServiceInjection.Remove<IKCoolDown>();
        }
    }
}