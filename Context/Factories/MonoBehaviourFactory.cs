#region

using System;
using Svelto.DataStructures;
using UnityEngine;

#endregion

namespace Svelto.Context
{
    public class MonoBehaviourFactory : Factories.IMonoBehaviourFactory
    {
        virtual public M Build<M>(Func<M> constructor) where M : MonoBehaviour
        {
            var mb = constructor();

            return mb;
        }
    }
}
