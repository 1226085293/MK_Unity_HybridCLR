using HybridCLR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HotFix
{
    public class App
    {
        public static int Main()
        {
#if !UNITY_EDITOR
            LoadMetadataForAOTAssembly();
            Debug.Log("ydd-- AOT程序集加载完毕!");
#endif
            LoadScene();
            return 0;
        }
        /// <summary>
        /// 切换场景
        /// </summary>
        static async void LoadScene()
        {
            var handler = await Addressables.LoadSceneAsync("Assets/Main/MainScene.unity").Task;
            handler.ActivateAsync();
        }

        /// <summary>
        /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
        /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
        /// </summary>
        public static unsafe void LoadMetadataForAOTAssembly()
        {
            // 可以加载任意aot assembly的对应的dll。但要求dll必须与unity build过程中生成的裁剪后的dll一致，而不能直接使用原始dll。
            // 我们在Huatuo_BuildProcessor_xxx里添加了处理代码，这些裁剪后的dll在打包时自动被复制到 {项目目录}/HuatuoData/AssembliesPostIl2CppStrip/{Target} 目录。

            /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
            /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误


            foreach (var dllBytes in LoadDll.aotDllBytes)
            {
                fixed (byte* ptr = dllBytes.bytes)
                {
                    // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
                    int err = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(ptr, dllBytes.bytes.Length, (Int32)HomologousImageMode.SuperSet);
                    Debug.Log($"LoadMetadataForAOTAssembly:{dllBytes.name}. ret:{err}");
                }
            }
        }
    }
}

