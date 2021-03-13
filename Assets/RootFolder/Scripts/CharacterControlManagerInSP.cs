using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diamond.CharacterControlLib
{
    /// <summary>
    ///  スマートフォン上でCharacterControllerを動かすためのマネージャー
    /// </summary>
    public class CharacterControlManagerInSP : MonoBehaviour
    {
        /// <summary>
        ///  操作対象のキャラクター
        /// </summary>
        [SerializeField]
        protected ICharacterController _characterController;

        /// <summary>
        /// ジョイスティック
        /// </summary>
        [SerializeField]
        protected Joystick _joystick;

        [SerializeField]
        protected Button _buttonJump;

        protected virtual void Start()
        {
            _buttonJump.onClick.AddListener(_characterController.Jump);
        }

        protected virtual void Update()
        {
            MoveByJoystick();
        }

        /// <summary>
        /// ジョイスティックの方向に合わせてキャラクターを動かす
        /// </summary>
        protected virtual void MoveByJoystick()
        {
            var direction = Vector3.zero;
            direction.x = _joystick.Horizontal;
            direction.z = _joystick.Vertical;
            _characterController.Move(direction);
        }
    }
}