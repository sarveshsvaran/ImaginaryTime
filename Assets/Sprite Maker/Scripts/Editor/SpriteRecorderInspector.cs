using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpriteRecorder))]
public class SpriteRecorderInspector : Editor {
	private SpriteRecorder recorder;
	private void OnEnable(){
		recorder = (SpriteRecorder)target;
	}

	public override void OnInspectorGUI ()
	{
		GUI.changed = false;
		recorder.folder = EditorGUILayout.TextField (new GUIContent("Folder","The sprites will be created in this folder."), recorder.folder);
		recorder.framesToCapture = EditorGUILayout.IntField (new GUIContent("Single Sprites","How many single sprites should be captured?"), recorder.framesToCapture);
		recorder.padding= EditorGUILayout.IntField (new GUIContent("Padding","Padding between single sprites in the sprite sheet or in single sprites."), recorder.padding);

		recorder.recordAnimation = EditorGUILayout.Toggle (new GUIContent ("Record Animation", "Do you want to capture sprites from an animation?"), recorder.recordAnimation);
		if (recorder.recordAnimation) {
			recorder.autoFrameRate = EditorGUILayout.Toggle (new GUIContent ("Automatic Frame Rate", "Set the frame rate automatcly based on the animations length."), recorder.autoFrameRate);
			if (!recorder.autoFrameRate) {
				recorder.frameRate = EditorGUILayout.IntField (new GUIContent ("Frame Rate", "Frame rate to capture at."), recorder.frameRate);
			}
			recorder.animationClip=(AnimationClip)EditorGUILayout.ObjectField("Animation Clip",recorder.animationClip,typeof(AnimationClip),false);
		} else {
			recorder.frameRate= EditorGUILayout.IntField (new GUIContent("Frame Rate","Frame rate to capture at."), recorder.frameRate);		
		}

		if (GUI.changed) {
			EditorUtility.SetDirty(recorder);
		}
	}
}
