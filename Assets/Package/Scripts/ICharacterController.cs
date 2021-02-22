using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.CharacterControlLib
{
    /// <summary>
    ///  interface of control character
    /// </summary>
    public interface ICharacterController
    {
        public void ExecuteGravity();
        public void Move(Vector3 direction);
        public void ChangePerspective(Vector3 perspective);
        public void Jump();
        public bool IsFoot();
    }
}