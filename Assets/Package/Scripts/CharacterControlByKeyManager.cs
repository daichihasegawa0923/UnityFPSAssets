using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.CharacterControlLib
{
    public class CharacterControlByKeyManager : MonoBehaviour
    {
        [SerializeField]
        private ICharacterController CharacterController;
        [SerializeField]
        private GameObject CharacetrControllerGameObject;

        [SerializeField]
        private float _mouseSensitivity = 10.0f;

        private void Start()
        {
            CharacterController = CharacetrControllerGameObject.GetComponent<ICharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            ControlByKeyboard();
        }

        public void ControlByKeyboard()
        {
            CharacterController.ExecuteGravity();

            if (Input.GetKeyDown(KeyCode.Space))
                CharacterController.Jump();

            var moveSpeed = Vector3.zero;
            moveSpeed.z = Input.GetAxis("Vertical");
            moveSpeed.x = Input.GetAxis("Horizontal");
            CharacterController.Move(moveSpeed);

            var neckSpin = Vector3.zero;
            neckSpin.x = - Input.GetAxis("Mouse Y");
            neckSpin.y = Input.GetAxis("Mouse X");
            neckSpin *= _mouseSensitivity;
            CharacterController.ChangePerspective(neckSpin);
        }
    }
}