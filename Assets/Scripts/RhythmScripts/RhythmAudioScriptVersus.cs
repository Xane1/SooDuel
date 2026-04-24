using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmAudioScriptVersus : MonoBehaviour
{
    public float[] beatTimestamps;
    public float[] attackTimestamps;
    
    
    public float songPosition;
    public float firstBeatOffset;
   // public float endBeatOffset;
    private int nextBeat;
    private int nextAttack;

    public AudioClip[] easyClips;
    public AudioClip[] normalClips;
    public AudioClip[] hardClips;
    
    private readonly float[] easyBeatTimestamps  = { 4.717855f,5.656353f,6.721675f,7.736267f,8.294293f,8.812813f,9.458020f,10.428498f,11.425637f,12.076177f,13.750517f,14.086452f, 15.724937f,16.748738f,17.415274f,18.412413f,19.398888f,20.705300f,21.713104f,22.059703f,23.670056f,24.677860f,25.360393f,26.021598f,26.997408f,27.370668f,28.693078f,29.679552f,30.676691f,31.716489f,32.756286f,33.412158f,34.366639f,35.406436f,36.056976f,37.715320f,38.675133f,39.357667f,40.658747f,41.303954f,42.269100f,42.914307f,44.508663f,45.473809f,46.572261f,47.430761f,48.433232f,49.750309f,50.064914f,51.062054f,51.696597f,52.683071f,53.674878f,54.997287f,55.983762f,56.980901f,57.967375f,58.985843f,59.636383f,60.622858f,61.614665f,62.265205f,63.294338f,63.982204f,64.318138f,64.984675f,65.981814f,66.675012f,67.021611f,67.672151f,68.679955f,69.698423f,71.063491f,71.362099f,72.055298f,73.068434f,73.745635f,74.076237f,74.774768f,75.777239f,76.785043f,77.803511f,78.454051f,79.445858f,80.485655f,81.840058f,82.170661f,82.847862f,83.850333f,84.490209f,85.482016f,86.511148f,87.486958f,88.169492f,88.510759f,89.161299f,90.179767f, 91.192903f,91.843443f,92.840582f,};
    private readonly float[] easyAttackTimestamps = { 13.042404f, 26.015876f, 41.367552f, 54.341025f, 68.370293f,83.823282f,};
    
    private readonly float[] normalBeatTimestamps = { 6.099400f,7.370288f,8.550399f,9.505726f,10.374599f,11.273731f,12.164217f,13.301101f,14.433661f,15.012909f,15.920687f,16.512903f,16.811173f,17.390422f,18.298199f,18.855834f,19.184363f,20.347182f,20.671389f,21.224701f,21.505679f,22.106541f,22.651208f,22.988382f,23.541694f,23.844287f,24.388953f,24.985493f,25.309699f,25.880302f,26.165603f,26.740529f,27.306809f,27.635338f,28.214587f,28.517179f,29.100750f,29.675676f,29.986914f,30.548871f,31.460972f,32.027252f,32.338490f,32.878833f,33.220331f,33.782288f,34.378828f,34.694388f,35.260668f,35.554615f,36.151155f,36.756340f,37.063255f,37.668440f,37.949419f,38.528667f,39.401863f,39.976788f,40.305317f,40.888888f,41.472460f,41.775052f,42.345655f,42.648248f,43.236142f,43.819713f,44.113660f,44.688585f,44.978210f,46.162643f,46.465235f,47.044484f,47.338431f,47.922002f,48.553124f,48.816811f,49.421996f,49.737557f,50.321128f,50.917668f,51.220260f,51.799508f,52.115069f,52.685672f,53.290857f,53.593450f,54.164053f,54.453677f,55.067507f,55.616496f,55.927734f,56.502660f,56.787962f,57.375855f,57.976718f,58.249051f,58.862881f,59.135215f,59.723108f,60.609272f,61.175552f,61.495436f,62.087653f,62.666901f,62.973816f,63.557387f,63.885916f,64.469488f,65.044413f,65.338360f,65.939222f,66.241815f,67.395989f,68.299444f,68.597713f,69.176962f,69.769179f,70.071771f,70.642374f,70.931998f,71.528538f,72.133723f,73.002595f,73.309510f,73.901727f,74.524203f,74.779245f,75.380108f,75.682700f,76.240335f,76.849842f,77.143789f,77.723038f,78.034276f,78.622170f,79.214386f,79.473751f,80.057323f,80.372883f,80.973746f,81.552994f,81.859909f,82.443480f,82.741750f,83.320999f,83.891602f,84.176903f,84.773442f,85.689865f,86.269114f,87.125018f,87.436256f,	};
    private readonly float[] normalAttackTimestamps = { 7.505726f, 13.626740f, 28.877400f, 43.561781f, 58.319648f, 73.380108f, };
    
    private readonly float[] hardBeatTimestamps =
    {
        10.077803f, 10.342965f, 10.891539f, 11.145663f, 11.629679f, 12.185224f, 12.425118f, 12.922127f, 13.200958f,
        13.709964f, 14.177779f, 14.461729f, 14.948611f, 15.179300f, 15.685515f, 16.118226f, 16.382941f, 16.900664f,
        17.146162f, 17.616031f, 18.345330f, 18.843672f, 19.071393f, 20.106935f, 20.307645f, 20.818275f, 21.093859f,
        21.563995f, 22.056931f, 22.291327f, 22.565516f, 22.817686f, 23.066277f, 23.543564f, 23.911310f, 24.279056f,
        24.771992f, 25.028396f, 25.507484f, 25.992595f, 26.720263f, 27.001941f, 27.502701f, 27.972164f, 28.934563f,
        29.443148f, 29.936699f, 30.162991f, 30.648102f, 30.882834f, 31.383594f, 31.805276f, 32.079964f, 32.555338f,
        32.776334f, 33.277094f, 33.723084f, 33.981289f, 34.458576f, 34.685483f, 35.186244f, 35.647882f, 35.874789f,
        36.336428f, 36.602457f, 37.082030f, 37.431841f, 37.807411f, 38.269050f, 38.503201f, 39.000946f, 39.349659f,
        39.704952f, 40.187551f, 40.448434f, 41.369564f, 41.654198f, 43.154903f, 43.529935f, 44.003658f, 44.220781f,
        44.714244f, 45.043218f, 45.405090f, 45.865655f, 46.109096f, 46.582820f, 47.063123f, 47.293405f, 47.780288f,
        47.990832f, 48.490873f, 48.819848f, 48.925120f, 49.148823f, 49.826511f, 50.306814f, 50.754219f, 50.991081f,
        51.695087f, 52.122754f, 52.800442f, 53.241268f, 53.491289f, 53.958433f, 54.293987f, 54.622962f, 55.313809f,
        55.741476f, 55.978338f, 56.438902f, 57.123170f, 57.570575f, 58.004822f, 58.228525f, 58.925951f, 59.827342f,
        60.070783f, 60.741892f, 61.636703f, 61.886724f, 62.564412f, 63.432905f, 63.676346f, 64.354034f, 65.248845f,
        65.479128f, 66.163395f, 66.610801f, 67.077945f, 67.295068f, 67.999074f, 68.439900f, 68.907044f, 69.321552f,
        69.676845f, 69.966343f, 70.071615f, 70.440066f, 70.683508f, 70.940108f, 71.170390f, 71.295401f, 71.426991f,
        71.650693f, 72.427074f, 72.650777f, 73.058705f, 73.453475f, 74.674593f, 75.295733f, 75.691763f, 76.058955f,
        76.708933f, 77.348535f, 77.710453f, 78.098598f, 78.361778f, 78.735447f, 79.348702f, 79.598723f, 80.004301f,
        80.511460f, 81.005822f, 81.536930f, 82.034871f, 82.543877f, 83.052883f, 83.561768f, 84.461789f, 85.225298f,
        85.833892f, 86.454556f, 86.851904f, 87.593282f, 88.124419f, 88.655555f, 89.131365f, 89.684633f, 90.149377f,
        90.702645f, 91.200585f, 91.709591f, 92.240728f, 92.605884f, 93.349866f, 93.705158f, 94.221425f, 94.586811f,
        94.968421f, 95.222839f, 95.615658f, 96.007981f, 96.611540f, 97.008065f, 97.244926f, 97.629551f, 97.784445f,
        98.271341f, 98.647563f, 99.034850f, 99.278288f, 99.653021f, 100.047791f, 100.296300f, 100.797853f, 101.297895f,
        101.679468f, 102.055690f, 102.284819f, 102.678870f, 103.048040f, 103.339270f, 103.829666f, 104.324462f,
        104.877353f, 105.502545f, 105.916700f, 106.382240f, 106.913377f, 107.153645f, 107.422383f, 107.942454f,
        108.193205f, 108.429329f, 108.930108f, 109.978478f, 110.469710f, 110.877639f, 111.516561f, 112.667357f,
        113.547052f, };

    private readonly float[] hardAttackTimestamps = {13.179300f, 28.882834f, 43.865655f, 58.741892f, 73.691763f, 88.702645f};
    
    private BeatSpawnerScript beatSpawnerScript;
    private AttackTelegraphManager attackTelegraphManager;
   // private int nextBeatIndex = 0;
    private Coroutine muteRoutine;

    public AudioSource musicSource;

    private float dspSongTime;
    private float originalFirstBeatOffset;

    IEnumerator TemporaryMute(float duration)
    {
        musicSource.mute = true;
        yield return new WaitForSeconds(duration);
        musicSource.mute = false;
        muteRoutine = null;
    }

    void Awake()
    {
        originalFirstBeatOffset = firstBeatOffset;

    }

   void Start()
    {
        
        firstBeatOffset = originalFirstBeatOffset - 0.36f;

        nextBeat = 0;
        nextAttack = 0;
        musicSource = GetComponent<AudioSource>();

        AudioClip clipToPlay = normalClips[0];
        beatTimestamps = normalBeatTimestamps;
        attackTimestamps = normalAttackTimestamps;

        if (DifficultyManager.Instance != null)
        {
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy)
            {
                clipToPlay = easyClips[0];
                beatTimestamps = easyBeatTimestamps;
                attackTimestamps = easyAttackTimestamps;
            }

            else if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard)
            {
                clipToPlay = hardClips[0];
                beatTimestamps = hardBeatTimestamps;
                attackTimestamps = hardAttackTimestamps;
                //      endBeatOffset = 12f;
            }
        }

        musicSource.clip = clipToPlay;
        
        dspSongTime = (float)AudioSettings.dspTime;   
        
        musicSource.Play();

        beatSpawnerScript = GameObject.Find("BeatSpawner").GetComponent<BeatSpawnerScript>();
        attackTelegraphManager = GameObject.Find("Main Camera").GetComponent<AttackTelegraphManager>();
    }

    void OnEnable()
    {
        BeatTargetScript.OnBeatFail += HandleBeatFail;
        BeatTargetScript.OnBeatSuccess += HandleBeatSuccess;
    }

    void OnDisable()
    {
        BeatTargetScript.OnBeatFail -= HandleBeatFail;
        BeatTargetScript.OnBeatSuccess -= HandleBeatSuccess;
    }

    public void HandleBeatSuccess()
    {
        if (muteRoutine != null) 
        {
            StopCoroutine(muteRoutine); 
            muteRoutine = null;
        }
        musicSource.mute = false;
    }

    private void HandleBeatFail()
    {
        if (muteRoutine != null)
        {
            StopCoroutine(muteRoutine);
            muteRoutine = null;
        }

        muteRoutine = StartCoroutine(TemporaryMute(10f));
    }

    void Update()
    {
        if (musicSource == null || musicSource.clip == null || beatTimestamps == null || attackTimestamps == null)
            return;

        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        if (songPosition >= musicSource.clip.length)
            return;

        if (nextBeat < beatTimestamps.Length && songPosition >= beatTimestamps[nextBeat])
        {
            nextBeat += 1;
            beatSpawnerScript.SpawnObjectAtRandom();
            if (SceneManager.GetActiveScene().name == "CoOpScene")
            {
                beatSpawnerScript.SpawnObjectAtRandom();
            }
        }
        if (nextAttack < attackTimestamps.Length && songPosition >= attackTimestamps[nextAttack])
        {
            nextAttack += 1;
            attackTelegraphManager.TelegraphSequence();
        }
    }
}