using System;
using BackEnd;
using UnityEngine;

namespace SceneHandler._0_Initialize
{
    public class InitializeSceneHandler : BaseSceneHandler
    {
        private void Awake()
        {
            var bro = Backend.Initialize();

            if(bro.IsSuccess())
            {
                Debug.Log("초기화 성공 : " + bro);
            }
            else
            {
                Debug.LogError("초기화 실패 : " + bro);
            }
        }
    }
}
