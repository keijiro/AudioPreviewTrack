using UnityEngine;
using UnityEngine.Playables;
using System.Reflection;
using System;

namespace Klak.Timeline
{
    [Serializable]
    public class AudioPreviewPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public AudioClip clip;

        #endregion

        #if UNITY_EDITOR

        #region PlayableBehaviour overrides

        public override void OnGraphStop(Playable playable)
        {
            if (clip != null) StopClip();
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (clip != null) PlayClip((float)playable.GetTime());
        }

        #endregion

        #region Audio clip utilities

        const float kMaxGap = 1.0f / 15;

        float _lastUpdateTime;
        bool _played;

        float GetClipPosition()
        {
            var type = Type.GetType("UnityEditor.AudioUtil,UnityEditor");
            var func = type.GetMethod("GetClipPosition", new [] { typeof(AudioClip) });
            return (float)func.Invoke(null, new System.Object [] { clip });
        }

        void CheckPosition()
        {
            // Stop if the gap is larger than kMaxGap.
            if (Mathf.Abs(GetClipPosition() - _lastUpdateTime) > kMaxGap) StopClip();
        }

        void PlayClip(float time)
        {
            // Stop and replay if the gap is larger than kMaxGap.
            if (Mathf.Abs(GetClipPosition() - time) > kMaxGap)
            {
                StopClip();

                var type = Type.GetType("UnityEditor.AudioUtil,UnityEditor");
                var func = type.GetMethod("PlayClip", new [] { typeof(AudioClip), typeof(int), typeof(bool) });
                func.Invoke(null, new System.Object [] { clip, 0, false });

                func = type.GetMethod("SetClipSamplePosition", new[] { typeof(AudioClip), typeof(int) });
                func.Invoke(null, new System.Object [] { clip, (int)(time * clip.frequency + 1) });

                UnityEditor.EditorApplication.update += CheckPosition;
                _played = true;
            }

            _lastUpdateTime = time;
        }

        void StopClip()
        {
            var type = Type.GetType("UnityEditor.AudioUtil,UnityEditor");
            var func = type.GetMethod("StopAllClips", BindingFlags.Public | BindingFlags.Static);
            func.Invoke(null, null);

            if (!_played) return;

            UnityEditor.EditorApplication.update -= CheckPosition;
            _played = false;
        }

        #endregion

        #endif
    }
}
