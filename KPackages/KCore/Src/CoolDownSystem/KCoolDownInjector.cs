// Created by Kabourlix Cendrée on 14/10/2023

#nullable enable
using UnityEngine;

namespace SDKabu.KUtils
{
    public class KBasicServiceInjector : MonoBehaviour
    {
        private void Awake()
        {
            GameObject cdGameObject = new(nameof(KCoolDownSystem));
            cdGameObject.transform.SetParent(transform);
            KCoolDownSystem cdSystem = cdGameObject.AddComponent<KCoolDownSystem>();
            KServiceInjection.Add<IKCoolDown>(cdSystem);
        }
    }
}