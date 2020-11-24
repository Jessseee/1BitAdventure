using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Dialogue;
using UnityEngine;
using Yarn.Unity;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class NPC : MonoBehaviour
{
    public string npcName;

    public YarnProgram[] yarnScripts;
    public string[] dialogueNodes;
    [SerializeField] private DialogueContainer dialogueContainer;
    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private string[] animationNames;

    private DialoguePlacer _dialoguePlacer;
    private DialogueRunner _dialogueRunner;
    private DialogueVisitedTracker _dialogueVisitedTracker;
    private CinemachineVirtualCamera _vCam;
    private CinemachineBrain _mainCam;
    private Dictionary<string, string> _animations;
    private string _currentAnimatorState;

    private void Awake()
    {
        if (yarnScripts.Length > 0 || dialogueNodes.Length > 0)
        {
            _dialogueRunner = FindObjectOfType<DialogueRunner>();
            _dialoguePlacer = FindObjectOfType<DialoguePlacer>();
            _dialogueVisitedTracker = FindObjectOfType<DialogueVisitedTracker>();

            foreach (YarnProgram yarnScript in yarnScripts)
                _dialogueRunner.Add(yarnScript);
        }

        if (GetComponentInChildren<CinemachineVirtualCamera>() != null)
        {
            _vCam = GetComponentInChildren<CinemachineVirtualCamera>();
            _vCam.gameObject.SetActive(false);

            if (Camera.main != null)
                _mainCam = Camera.main.GetComponent<CinemachineBrain>();
            else
                Debug.LogError("Could not find main camera");

            if (_dialogueRunner != null)
            {
                _dialogueRunner.AddCommandHandler("focusCamera", FocusCamera);
                _dialogueRunner.AddCommandHandler("unFocusCamera", UnFocusCamera);
            }
        }

        if (spriteAnimator != null)
        {
            AnimationClip[] animationClips = spriteAnimator.runtimeAnimatorController.animationClips;

            if (animationClips.Length > animationNames.Length)
                Debug.LogWarning($"NPC {name} has unnamed animationClips");
            else if (animationNames.Length > animationClips.Length)
                Debug.LogError($"NPC {name} has a named animation without animationClip reference");
            else
            {
                _animations = new Dictionary<string, string>();
                for (int i = 0; i < animationClips.Length; i++)
                {
                    _animations.Add(animationNames[i], animationClips[i].name);
                }
            }
        }
    }

    public void StartDialogue(int node = 0)
    {
        if (dialogueNodes.Length - 1 >= node)
        {
            if (!_dialogueRunner.IsDialogueRunning && !_dialogueVisitedTracker.Visited(dialogueNodes[node]))
            {
                _dialoguePlacer.SetCurrentDialogueContainer(dialogueContainer);
                _dialogueRunner.StartDialogue(dialogueNodes[node]);
            }
        }
        else
            Debug.LogError($"NPC {name} does not have node {node}");
    }
    
    [YarnCommand("animate")]
    public void ChangeAnimationState(string newState)
    {
        if (_currentAnimatorState == newState) return;

        spriteAnimator.Play(_animations[newState], -1);
        _currentAnimatorState = newState;
    }

    [YarnCommand("toggleDialogueContainer")]
    public void ToggleDialogueContainer(string state)
    {
        if(Boolean.TryParse(state, out bool stateBool))
            dialogueContainer.container.SetActive(stateBool);
        else
            Debug.LogWarning($"string state in NPC {name} dialogue is not a valid boolean");
    }
    
    private void FocusCamera(string[] parameters, Action onComplete)
    {
        _vCam.gameObject.SetActive(true);
        StartCoroutine(WaitForCameraMove(onComplete));
    }
    
    private void UnFocusCamera(string[] parameters, Action onComplete)
    {
        _vCam.gameObject.SetActive(false);
        StartCoroutine(WaitForCameraMove(onComplete));
    }

    private IEnumerator WaitForCameraMove(Action onComplete)
    {
        yield return null; yield return null;
        while (_mainCam.IsBlending) { yield return null; }
        onComplete();
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(NPC))]
    public class NPCEditor : Editor
    {
        private SerializedProperty _yarnScripts;
        private SerializedProperty _dialogueNodes;
        private SerializedProperty _dialogueContainer;
        private SerializedProperty _spriteAnimator;
        private SerializedProperty _animationNames;

        private void OnEnable()
        {
            _yarnScripts = serializedObject.FindProperty("yarnScripts");
            _dialogueNodes = serializedObject.FindProperty("dialogueNodes");
            _dialogueContainer = serializedObject.FindProperty("dialogueContainer");
            _spriteAnimator = serializedObject.FindProperty("spriteAnimator");
            _animationNames = serializedObject.FindProperty("animationNames");
        }
        
        public override void OnInspectorGUI()
        {
            NPC script = (NPC) target;

            EditorGUILayout.PropertyField(_yarnScripts);
            EditorGUILayout.PropertyField(_dialogueNodes);
            EditorGUILayout.PropertyField(_dialogueContainer);
            EditorGUILayout.PropertyField(_spriteAnimator);
            if (script.spriteAnimator != null)
            {
                EditorGUILayout.PropertyField(_animationNames);

                AnimationClip[] animationClips = script.spriteAnimator.runtimeAnimatorController.animationClips;

                if (animationClips.Length > script.animationNames.Length)
                    EditorGUILayout.HelpBox($"NPC {script.name} has unnamed animationClips", MessageType.Warning);
                else if (script.animationNames.Length > animationClips.Length)
                    EditorGUILayout.HelpBox($"NPC {name} has a named animation without animationClip reference", MessageType.Warning);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
