// Created by Kabourlix Cendrée on 22/10/2023

using UnityEngine;

namespace SDKabu.KCharacter
{
    public interface IKNavigation<in TVector> where TVector : struct
    {
        public bool TrySetDestination(TVector _location, bool _override = true);

        public void MoveTo(TVector _target, float _speed, float _deltaTime);
        public void MoveToInstantly(TVector _target);
    }

    public interface IKNavigation2D : IKNavigation<Vector2>
    {
    }

    public interface IKNavigation : IKNavigation<Vector3>
    {
    }
}