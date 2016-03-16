using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteRecorder : MonoBehaviour {
	#if UNITY_EDITOR
	public string folder = "Sprites";
	public int framesToCapture = 25;
	public int padding=2;


	public int frameRate = 25;
	public bool recordAnimation;
	public bool autoFrameRate;
	public AnimationClip animationClip;


	private Camera whiteCam;
	private Camera blackCam;
	private int videoframe = 0; 
	private int unityframe = 0;
	private float originaltimescaleTime;
	private string realFolder = "";
	private bool done=false;

	private void Start () {
		whiteCam = GameObject.Find ("White Camera").GetComponent<Camera>();
		blackCam = GameObject.Find ("Black Camera").GetComponent<Camera>();

		framesToCapture +=1;
		if (recordAnimation) {
			Animation anim = FindObjectOfType<Animation> ();
			if (anim != null) {
				anim.Play (animationClip.name);
			}else{
				Animator animator=FindObjectOfType<Animator>();
				if(animator != null){
					RuntimeAnimatorController tempController = animator.runtimeAnimatorController;
					AnimatorOverrideController overrideController= new AnimatorOverrideController();
					overrideController.runtimeAnimatorController=tempController;
					overrideController["Temp"]=animationClip;
					animator.runtimeAnimatorController = overrideController;
				}
			}

			if(autoFrameRate){
				frameRate = (int)(framesToCapture / animationClip.length);
			}
		}

		Time.captureFramerate = frameRate;

		realFolder = folder;
		int count = 1;
		while (System.IO.Directory.Exists(Application.dataPath+"/"+realFolder)) {
			realFolder = folder +" "+ count;
			count++;
		}
		System.IO.Directory.CreateDirectory(Application.dataPath+"/"+realFolder);  
		originaltimescaleTime=Time.timeScale;
	}

	private void Update() {
		if(!done){
			if(unityframe%2==0){
				blackCam.enabled=true;
				whiteCam.enabled=false;
				Time.timeScale=0;
				StartCoroutine(Capture()); 
			} else {
				blackCam.enabled=false;
				whiteCam.enabled=true;
				Time.timeScale=originaltimescaleTime;
			}
			unityframe++;
		}
	}

	List<Texture2D> textures = new List<Texture2D> ();
	private IEnumerator Capture () {
		if(videoframe < framesToCapture) {
			string name =System.String.Format("{0}/{1:D04} sprite.png", realFolder, videoframe );//Time.frameCount

			yield return new WaitForEndOfFrame(); 

			int width = Screen.width;
			int height = Screen.height; 

			Texture2D texb = new Texture2D(width, height, TextureFormat.RGB24, false);
			texb.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			texb.Apply();
			
			yield return 0; 
			yield return new WaitForEndOfFrame(); 

			Texture2D texw = new Texture2D(width, height, TextureFormat.RGB24, false);
			texw.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			texw.Apply();

			Texture2D outputtex = new Texture2D(width, height, TextureFormat.ARGB32, false);

			for (int y = 0; y < outputtex.height; ++y) { 
				for (int x = 0; x < outputtex.width; ++x) { 
					var alpha = texw.GetPixel(x, y).r - texb.GetPixel(x, y).r;   
					alpha = 1.0f - alpha;
					Color color = Color.clear;
					if(alpha != 0) {
						color = texb.GetPixel(x, y) / alpha;
					} 
					color.a = alpha;
					outputtex.SetPixel(x, y, color);
				}
			}
			Texture2D tex=outputtex.Trim(padding);
	
			textures.Add(tex);

			tex.SaveTexture(name);

			Destroy(texb);
			Destroy(texw);
			videoframe++;
		}
		else {
			EditorApplication.isPlaying=false;
			done=true;
		}
	}
#endif
}
