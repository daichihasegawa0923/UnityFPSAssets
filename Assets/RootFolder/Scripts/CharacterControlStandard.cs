using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.CharacterControlLib
{
    /// <summary>
    /// Character Controlled by keyboard.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControlStandard : MonoBehaviour,ICharacterController
    {
        public CharacterController CharacterController { private set; get; }

        [SerializeField]
        private float _isFootDistance = 1.0f;
        [SerializeField]
        private float _isFootSize = 0.5f;

        [SerializeField]
        private float _currentGrabity = 0.0f;

        [SerializeField]
        private float _gravityPower = 0.01f;

        [SerializeField]
        private float _moveSpeed = 1.0f;

        [SerializeField]
        private float _jumpPower = 0.1f;

        /// <summary>
        /// 視点変更時に動かす部分
        /// </summary>
        [SerializeField]
        private GameObject _neck;

        public void ExecuteGravity()
        {
            if(this.IsFoot())
            {
                _currentGrabity = 0.0f;
                return;
            }

            _currentGrabity += this._gravityPower;
            var gravityVec = Vector3.zero;
            gravityVec.y = -_currentGrabity;
            this.CharacterController.Move(gravityVec);
        }

        public void ChangePerspective(Vector3 perspective)
        {
            var spin = _neck.transform.localEulerAngles;
            var spinHontai = transform.localEulerAngles;

            spinHontai.y += perspective.y;
            spin.x += perspective.x;

            if(spin.x > 80 && spin.x < 180)
            {
                spin.x = 80;
            }
            else if(spin.x > 180 && spin.x < 280)
            {
                spin.x = 280;
            }
            _neck.transform.localEulerAngles = spin;
            transform.localEulerAngles = spinHontai;
        }

        public bool IsFoot()
        {
            return Physics.BoxCast(transform.position, Vector3.one * _isFootSize, -transform.up, Quaternion.identity, _isFootDistance);
        }

        public void Jump()
        {
            if (!this.IsFoot())
                return;

            StopAllCoroutines();
            StartCoroutine(JumpCoroutine());
        }

        public void Move(Vector3 direction)
        {
            var x = direction.x;
            var z = direction.z;

            var moveSpeedVec = transform.forward * direction.z + transform.right * direction.x;
            this.CharacterController.Move(moveSpeedVec * _moveSpeed);
        }

        // Start is called before the first frame update
        void Start()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected IEnumerator JumpCoroutine()
        {
            var i = 0;
            var jumpPowerSum = 0.0f;
            while(i < 10)
            {
                i += 1;
                jumpPowerSum += this._jumpPower;
                var jumpVec = Vector3.zero;
                jumpVec.y = jumpPowerSum;
                CharacterController.Move(jumpVec);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}