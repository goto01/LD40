using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
    class AudioItemPlayer : MonoBehaviour
    {
        [SerializeField] private string _audioItem;

        protected virtual void Start()
        {
            AudioController.Play(_audioItem);
        }
    }
}
