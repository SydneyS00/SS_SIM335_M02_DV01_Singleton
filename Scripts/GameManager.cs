using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton
{
    public class GameManager : Singleton<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        void Start()
        {
            //To do
            //   - Load player save
            //   - If no save, redirect player to registration scene
            //   - Call backend and get daily challenges and rewards

            _sessionStartTime = DateTime.Now;

            //Displays the current start time of the game session 
            Debug.Log("Game session start @: " + DateTime.Now);
        }

        //This will take and display the time that the application is closed
        void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);

            //This displays the time that the game session ended
            Debug.Log("Game session ended @: " + DateTime.Now);
            //This displays the duration of the game session 
            Debug.Log("Game session lasted: " + timeDifference);
        }

        void OnGUI()
        {
            if(GUILayout.Button("Next Scene"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    public class Singleton<T>:
        MonoBehaviour where T : Component
    {
        private static T _instance;
        
        public static T instance
        {
            //This accessor makes sure that there's no existing instances of 
            //  this object before initializing a new one
            //FindObjectOfType() searches for the first loaded object of the 
            //  specified object T. If we cannot find it we create a new 
            //  GameObject, rename it, and add a component to if of a 
            //  non-specified type.
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if(_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance; 
            }
        }

        public virtual void Awake()
        {
            //When this Awake() function is called the Singleton component
            //  will check whether there's already one instance of itself
            //  initialized in memory. If not, then it will become the current
            //  instance. But if one already exists, then it will destory 
            //  itself to prevent duplication

            if(_instance == null)
            {
                //checks to see if there is already an instance of T
                //makes it the current instance and does not destroy it
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                //if there is already an existing instance 
                //destroy to prevent duplicates
                Destroy(gameObject);
            }
        }
    }
}
