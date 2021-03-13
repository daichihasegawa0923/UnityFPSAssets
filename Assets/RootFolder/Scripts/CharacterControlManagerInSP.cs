using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

        /// <summary>
        /// 視点変更のためのDrag用のイベントトリガー (Option)
        /// </summary>
        /// <remarks>
        /// 必要ない場合は、インスペクタ上からアタッチしないようにする
        /// </remarks>
        [SerializeField]
        protected EventTrigger _eventTriggerDragChangePerspective;

        [SerializeField]
        protected Button _buttonJump;

        protected virtual void Start()
        {
            _buttonJump.onClick.AddListener(_characterController.Jump);
            AttachDragEvent();
        }

        protected virtual void Update()
        {
            MoveByJoystick();
        }

        protected virtual void AttachDragEvent()
        {
            if (!_eventTriggerDragChangePerspective)
                return;

            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Drag;
            entry.callback.AddListener(d => DragForChangePerspective((PointerEventData)d));
            _eventTriggerDragChangePerspective.triggers.Add(entry);
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

        /// <summary>
        /// ドラッグした方向に合わせて視点を移動する
        /// </summary>
        /// <param name="pointerEventData"></param>
        public void DragForChangePerspective(PointerEventData pointerEventData)
        {
            var spin = Vector3.zero;
            spin.y = -pointerEventData.delta.x;
            spin.x = pointerEventData.delta.y;

            _characterController.ChangePerspective(spin);
        }
    }
}