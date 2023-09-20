using UnityEditor;
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{

    #region Properties
    SerializedProperty objective;

    SerializedProperty _cleanlinessForStar1;
    SerializedProperty _cleanlinessForStar2;
    SerializedProperty _cleanlinessForStar3;
    SerializedProperty _gameEndTimer;

    SerializedProperty _surviveTimeForStar1;
    SerializedProperty _surviveTimeForStar2;
    SerializedProperty _surviveTimeForStar3;
    SerializedProperty _minCleanlinessValue;

    SerializedProperty _trashCountForStar1;
    SerializedProperty _trashCountForStar2;
    SerializedProperty _trashCountForStar3;
    SerializedProperty BCS;

    SerializedProperty _bossDead;

    SerializedProperty _levelComplete;
    SerializedProperty _restartPanel;
    SerializedProperty _pauseMenu;
    SerializedProperty _HUD;
    SerializedProperty _nextLevelPanel;
    SerializedProperty _loadingScreen;
    SerializedProperty _progressBar;
    SerializedProperty _stars;
    SerializedProperty _gameTimer;

    private bool panelsGroup = false;

    #endregion

    private void OnEnable()
    {
        objective = serializedObject.FindProperty("objective");

        _cleanlinessForStar1 = serializedObject.FindProperty("_cleanlinessForStar1");
        _cleanlinessForStar2 = serializedObject.FindProperty("_cleanlinessForStar2");
        _cleanlinessForStar3 = serializedObject.FindProperty("_cleanlinessForStar3");
        _gameEndTimer = serializedObject.FindProperty("_gameEndTimer");

        _surviveTimeForStar1 = serializedObject.FindProperty("_surviveTimeForStar1");
        _surviveTimeForStar2 = serializedObject.FindProperty("_surviveTimeForStar2");
        _surviveTimeForStar3 = serializedObject.FindProperty("_surviveTimeForStar3");
        _minCleanlinessValue = serializedObject.FindProperty("_minCleanlinessValue");

        _trashCountForStar1 = serializedObject.FindProperty("_trashCountForStar1");
        _trashCountForStar2 = serializedObject.FindProperty("_trashCountForStar2");
        _trashCountForStar3 = serializedObject.FindProperty("_trashCountForStar3");
        BCS = serializedObject.FindProperty("BCS");
        
        _bossDead = serializedObject.FindProperty("_bossDead");

        _levelComplete = serializedObject.FindProperty("_levelComplete");
        _nextLevelPanel = serializedObject.FindProperty("_nextLevelPanel");
        _loadingScreen = serializedObject.FindProperty("_loadingScreen");
        _restartPanel = serializedObject.FindProperty("_restartPanel");
        _progressBar = serializedObject.FindProperty("_progressBar");
        _stars = serializedObject.FindProperty("_stars");
        _gameTimer = serializedObject.FindProperty("_gameTimer");
        _pauseMenu = serializedObject.FindProperty("_pauseMenu");
        _HUD = serializedObject.FindProperty("_HUD");


    }

    public override void OnInspectorGUI()
    {
        GameManager _gameManager = (GameManager)target;
        
        
        serializedObject.Update();

        EditorGUILayout.PropertyField(objective);
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(5);

        if (_gameManager.objective == GameManager.Objective.Cleanliness)
        {
            EditorGUILayout.PropertyField(_cleanlinessForStar1);
            EditorGUILayout.PropertyField(_cleanlinessForStar2);
            EditorGUILayout.PropertyField(_cleanlinessForStar3);
            EditorGUILayout.PropertyField(_gameEndTimer);
        }

        if(_gameManager.objective == GameManager.Objective.SurviveTime)
        {
            EditorGUILayout.PropertyField(_surviveTimeForStar1);
            EditorGUILayout.PropertyField(_surviveTimeForStar2);
            EditorGUILayout.PropertyField(_surviveTimeForStar3);
            EditorGUILayout.PropertyField(_minCleanlinessValue);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(5);

        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(5);
        
        if (_gameManager.objective == GameManager.Objective.TrashCount)
        {
            EditorGUILayout.PropertyField(_trashCountForStar1);
            EditorGUILayout.PropertyField(_trashCountForStar2);
            EditorGUILayout.PropertyField(_trashCountForStar3);
            EditorGUILayout.PropertyField(BCS);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(5);

        if (_gameManager.objective == GameManager.Objective.Boss)
        {
            EditorGUILayout.PropertyField(_bossDead);
            EditorGUILayout.PropertyField(_gameEndTimer);
            EditorGUILayout.PropertyField(_minCleanlinessValue);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(5);

        panelsGroup = EditorGUILayout.BeginFoldoutHeaderGroup(panelsGroup, "Panels");
        if (panelsGroup)
        {
            EditorGUILayout.PropertyField(_levelComplete);
            EditorGUILayout.PropertyField(_nextLevelPanel);
            EditorGUILayout.PropertyField(_loadingScreen);
            EditorGUILayout.PropertyField(_restartPanel);
            EditorGUILayout.PropertyField(_pauseMenu);
            EditorGUILayout.PropertyField(_HUD);
            EditorGUILayout.PropertyField(_progressBar);
            EditorGUILayout.PropertyField(_stars);
            EditorGUILayout.PropertyField(_gameTimer);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(5);




        serializedObject.ApplyModifiedProperties();
    }
}