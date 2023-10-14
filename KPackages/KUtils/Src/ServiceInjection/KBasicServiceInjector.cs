// Copyrighted by team Rézoskour
// Created by Kabourlix Cendrée on 14

#nullable enable
using UnityEngine;

namespace SDKabu.KUtils
{
    public class KBasicServiceInjector : MonoBehaviour
    {
        [SerializeField] private bool injectKCoolDownSystem = true;

        private void Awake()
        {
            //Instantiate KCoolDownSystem
            if (injectKCoolDownSystem)
            {
                InjectKCoolDownSystem();
            }
        }

        private void InjectKCoolDownSystem()
        {
            GameObject cdGameObject = new(nameof(KCoolDownSystem));
            cdGameObject.transform.SetParent(transform);
            KCoolDownSystem cdSystem = cdGameObject.AddComponent<KCoolDownSystem>();
            KServiceInjection.Add<IKCoolDown>(cdSystem);
        }
    }
}