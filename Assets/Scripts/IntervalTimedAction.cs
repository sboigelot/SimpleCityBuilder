using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class IntervalTimedAction
    {
        public Action Action;

        public TimeSpan Duration
        {
            get { return TimeSpan.FromSeconds(duration); }
            set { duration = (float)value.TotalSeconds; }
        }

        private float duration;

        private float elapsed;

        public void Update()
        {
            elapsed += Time.deltaTime;
            if (elapsed >= duration)
            {
                elapsed -= duration;
                if (Action != null)
                {
                    Action();
                }
            }
        }
    }
}