using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [TrackColor(0.8f, 0.3f, 0.4f)]
    [TrackClipType(typeof(AudioPreview))]
    public class AudioPreviewTrack : TrackAsset
    {
        #region TrackAsset overrides

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<AudioPreviewMixer>.Create(graph, inputCount);
        }

        #endregion
    }
}
