using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.CharacterControlLib
{
    /// <summary>
    ///  interface of control character
    /// </summary>
    public abstract class ICharacterController:MonoBehaviour
    {
        public abstract void ExecuteGravity();
        public abstract void Move(Vector3 direction);
        public abstract void ChangePerspective(Vector3 perspective);
        public abstract void Jump();
        public abstract bool IsFoot();
    }
}