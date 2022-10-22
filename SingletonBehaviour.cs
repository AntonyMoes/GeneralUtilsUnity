using System;
using UnityEngine;

namespace GeneralUtils {
    public abstract class SingletonBehaviour<TSelf> : MonoBehaviour where TSelf : SingletonBehaviour<TSelf> {
        public static TSelf Instance { get; private set; }

        protected SingletonBehaviour() {
            if (Instance != null) {
                throw new Exception($"{typeof(SingletonBehaviour<TSelf>)} already has an instance!");
            }

            Instance = (TSelf) this;
        }

        public void Dispose() {
            Instance = default;
        }
    }
}