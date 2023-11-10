// Created by Kabourlix Cendrée on 10/11/2023

using System;

namespace SDKabu.KCharacter
{
    public interface IKActor
    {
        public Guid ID { get; }
        public KHealthComp HealthComp { get; }
    }
}