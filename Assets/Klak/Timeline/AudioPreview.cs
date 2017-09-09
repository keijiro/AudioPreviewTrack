using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    public class AudioPreview : PlayableAsset, ITimelineClipAsset
    {
        #region Serialized variables

        public AudioPreviewPlayable template = new AudioPreviewPlayable();

        #endregion

        #region ITimelineClipAsset implementation

        public ClipCaps clipCaps { get { return ClipCaps.ClipIn; } }

        #endregion

        #region PlayableAsset overrides

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<AudioPreviewPlayable>.Create(graph, template);
        }

        #endregion
    }
}
