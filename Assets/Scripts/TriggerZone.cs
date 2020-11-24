using Dialogue;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class TriggerZone : MonoBehaviour
{
    private enum ZoneType
    {
        Default,
        PlayerOnly,
        NextLevel,
        DialogueRunOnce,
        DialogueRunXTimes,
        DialogueRunRandom,
        DialogueRunAlways,
    }
    
    [SerializeField] private ZoneType zoneType = ZoneType.Default;
    [SerializeField] private YarnProgram[] yarnScripts;
    [SerializeField] private string dialogue = "";
    [SerializeField] private string[] randDialogues;
    [SerializeField] private int maxNumberOfVisits;
    [SerializeField] private string nextLevel;
    private DialogueRunner _dialogueRunner;
    private DialogueVisitedTracker _dialogueVisitedTracker;

    public UnityEvent<Collider2D> triggerEnterEvent;
    public UnityEvent<Collider2D> triggerExitEvent;
    public UnityEvent<Collider2D> triggerStayEvent;

    private void Awake()
    {
        if (zoneType == ZoneType.DialogueRunAlways || zoneType == ZoneType.DialogueRunOnce || zoneType == ZoneType.DialogueRunXTimes)
        {
            _dialogueRunner = FindObjectOfType<DialogueRunner>();
            _dialogueVisitedTracker = _dialogueRunner.GetComponent<DialogueVisitedTracker>();
            
            foreach (YarnProgram yarnScript in yarnScripts)
                _dialogueRunner.Add(yarnScript);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (zoneType)
        {
            case ZoneType.Default:
                triggerEnterEvent.Invoke(other);
                break;
            case ZoneType.PlayerOnly:
                if(other.gameObject.CompareTag("Player"))
                    triggerEnterEvent.Invoke(other);
                break;
            case ZoneType.NextLevel:
                if (other.gameObject.CompareTag("Player"))
                    SceneManager.LoadScene(nextLevel);
                break;
            case ZoneType.DialogueRunOnce:
                if (other.gameObject.CompareTag("Player") && !_dialogueVisitedTracker.Visited(dialogue))
                    _dialogueRunner.StartDialogue(dialogue);
                break;
            case ZoneType.DialogueRunXTimes:
                if (other.gameObject.CompareTag("Player") && _dialogueVisitedTracker.NodeNumberOfVisits(dialogue) < maxNumberOfVisits)
                    _dialogueRunner.StartDialogue(dialogue);
                break;
            case ZoneType.DialogueRunRandom:
                if (other.gameObject.CompareTag("Player"))
                    _dialogueRunner.StartDialogue(randDialogues[Random.Range(0, randDialogues.Length)]);
                break;
            case ZoneType.DialogueRunAlways:
                if (other.gameObject.CompareTag("Player"))
                    _dialogueRunner.StartDialogue(dialogue);
                break;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        triggerEnterEvent.Invoke(other);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        triggerEnterEvent.Invoke(other);
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(TriggerZone))]
    public class TriggerZoneEditor : Editor
    {
        private SerializedProperty _zoneType;
        private SerializedProperty _yarnScripts;
        private SerializedProperty _randDialogues;
        private SerializedProperty _triggerEnterEvent;
        private SerializedProperty _triggerExitEvent;
        private SerializedProperty _triggerStayEvent;

        private void OnEnable()
        {
            _zoneType = serializedObject.FindProperty("zoneType");
            _yarnScripts = serializedObject.FindProperty("yarnScripts");
            _randDialogues = serializedObject.FindProperty("randDialogues");
            _triggerEnterEvent = serializedObject.FindProperty("triggerEnterEvent");
            _triggerExitEvent = serializedObject.FindProperty("triggerExitEvent");
            _triggerStayEvent = serializedObject.FindProperty("triggerStayEvent");
        }
        
        public override void OnInspectorGUI()
        {
            TriggerZone script = (TriggerZone) target;

            Collider2D collider = script.GetComponent<Collider2D>();
            if (collider == null)
            {
                BoxCollider2D newCollider = script.gameObject.AddComponent<BoxCollider2D>();
                newCollider.isTrigger = true;
                newCollider.size = new Vector2(5.0f, 5.0f);
            }
            else if (!collider.isTrigger)
                EditorGUILayout.HelpBox("Collider is not a trigger", MessageType.Warning);

            EditorGUILayout.PropertyField(_zoneType);

            if (script.zoneType == ZoneType.DialogueRunOnce || script.zoneType == ZoneType.DialogueRunXTimes || script.zoneType == ZoneType.DialogueRunRandom || script.zoneType == ZoneType.DialogueRunAlways)
            {
                EditorGUILayout.PropertyField(_yarnScripts);
                
                if (script.zoneType == ZoneType.DialogueRunRandom)
                    EditorGUILayout.PropertyField(_randDialogues);
                else
                    script.dialogue = EditorGUILayout.TextField("Dialogue:", script.dialogue);
            
                if (script.zoneType == ZoneType.DialogueRunXTimes)
                    script.maxNumberOfVisits = EditorGUILayout.IntField("Max Number Of Visits", script.maxNumberOfVisits);
            }
            
            if (script.zoneType == ZoneType.NextLevel)
                script.nextLevel = EditorGUILayout.TextField("Scene Name:", script.nextLevel);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_triggerEnterEvent);
            EditorGUILayout.PropertyField(_triggerExitEvent);
            EditorGUILayout.PropertyField(_triggerStayEvent);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}