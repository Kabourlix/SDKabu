// Copyrighted by team Rézoskour
// Created by Kabourlix Cendrée on 12

#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDKabu.KUtils
{
    /// <summary>
    /// Any of your service shall implement this interface.
    /// </summary>
    public interface IKService : IDisposable
    {
    }

    /// <summary>
    /// Use this class to declare a service.
    /// </summary>
    public static class KServiceInjection
    {
        private static Dictionary<Type, IKService> TypeToImplementationMapServices { get; set; } = new();

        public static void Add<TServiceType>(IKService _serviceImplementation) where TServiceType : IKService
        {
            Type serviceType = typeof(TServiceType);

            if (TypeToImplementationMapServices.ContainsKey(serviceType))
            {
                Debug.LogError($"Service {serviceType} has already been registered");
                return;
            }

            TypeToImplementationMapServices.Add(serviceType, _serviceImplementation);
            Debug.Log($"Service {serviceType} has been registered");
        }

        public static void Remove<TServiceType>() where TServiceType : IKService
        {
            Type serviceType = typeof(TServiceType);

            if (!TypeToImplementationMapServices.TryGetValue(serviceType, out IKService service))
            {
                Debug.LogWarning($"Service type {serviceType} is not registered");
                return;
            }

            TypeToImplementationMapServices.Remove(serviceType);
            service.Dispose();
            Debug.Log($"Service {serviceType} has been unregistered");
        }

        public static TServiceType? Get<TServiceType>() where TServiceType : IKService
        {
            if (TryGet(out TServiceType? res))
            {
                return res;
            }

            Debug.LogError($"Service {typeof(TServiceType)} is not registered");
            return default;
        }

        public static bool TryGet<TServiceType>(out TServiceType? _serviceImplementation) where TServiceType : IKService
        {
            Type serviceType = typeof(TServiceType);

            if (!TypeToImplementationMapServices.ContainsKey(serviceType))
            {
                _serviceImplementation = default;
                return false;
            }

            _serviceImplementation = (TServiceType)TypeToImplementationMapServices[serviceType];
            return true;
        }

        public static bool Has<TServiceType>() where TServiceType : IKService => TryGet(out TServiceType _);
    }
}