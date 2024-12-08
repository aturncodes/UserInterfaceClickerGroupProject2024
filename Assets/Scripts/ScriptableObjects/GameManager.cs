    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [System.Serializable]
    public class Slot
    {
        public int slotId { get; set; }
        public bool open { get; set; }
        public GameObject generatorPrefab { get; set; }
        public GameObject managerPrefab { get; set; }
    }

    [System.Serializable]
    public struct GameManagerData
    {
        public Slot[] slots;
    }

    public class GameManager : MonoBehaviour
    {
        //Game Layout
        const int GENS_PER_COL = 4;
        const int NUM_COL = 2;
        
        [SerializeField] private GridLayoutGroup slotGrid;
        
        //Initial generator prefabs
        [SerializeField] private GameObject lockedGenerator;
        [SerializeField] private GameObject starterGenerator;
        
        //Dictionary mappings for load and save
        private Dictionary<GameObject, GameObject> prefabMapping;
        private Dictionary<GameObject, GameObject> managerMapping;
        private List<GameObject> spawnedGenerators;
        private List<Slot> slotList;
        
        public static GameManager Singleton;
        private bool newGame;
        
        public void OnEnable() {
            
            if (Singleton != null) {
                Destroy(this);
            }

            Singleton = this;
            DontDestroyOnLoad(gameObject);
            
            prefabMapping = new Dictionary<GameObject, GameObject>();
            managerMapping = new Dictionary<GameObject, GameObject>();
            spawnedGenerators = new List<GameObject>();
            slotList = new List<Slot>();
            
            //if save file does not exist
            newGame = true;
            //if savesytem has an existing file      
            //LoadSaveData();
            //newGame = false;

        }

        public void OnDestroy()
        {
            slotGrid = null;
        }

        public void NewGame()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Game");
            //SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"Scene Loaded: {scene.name}. Current Singleton: {Singleton}");
            if (scene.name == "Game")
            {
                Debug.Log($"Before loading generators - SpawnedGenerators count: {spawnedGenerators.Count}");

                SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid multiple triggers

                GameObject slotGridObject = GameObject.FindGameObjectWithTag("SlotGrid");
                if (slotGridObject != null)
                {
                    slotGrid = slotGridObject.GetComponent<GridLayoutGroup>();
                    if (slotGrid == null)
                    {
                        Debug.LogError("GridLayoutGroup component not found on SlotGrid GameObject.");
                        return;
                    }
                }
                else
                {
                    Debug.LogError("No GameObject with the tag 'SlotGrid' found.");
                    return;
                }

                // Proceed with generator setup
                Debug.Log("TUTORIAL HAS STARTED");
                for (var i = 0; i < GENS_PER_COL * NUM_COL; i++)
                {
                    Slot newSlot = new Slot();
                    newSlot.slotId = i;

                    GameObject genObject;
                    if (i == 0)
                    {
                        genObject = Instantiate(starterGenerator, slotGrid.transform);
                        Generator gen = genObject.GetComponentInChildren<Generator>();
                        if (gen == null)
                        {
                            Debug.LogError("GENERATOR IS NULL");
                            return;
                        }
                        gen.SetSlotId(i);
                        newSlot.generatorPrefab = starterGenerator;
                        prefabMapping[genObject] = starterGenerator;
                        newSlot.open = false;
                    }
                    else
                    {
                        genObject = Instantiate(lockedGenerator, slotGrid.transform);
                        Generator gen = genObject.GetComponentInChildren<Generator>();
                        if (gen == null)
                        {
                            Debug.LogError($"Generator component is NULL for lockedGenerator in slot {i}.");
                            continue;
                        }
                        gen.SetSlotId(i);
                        newSlot.generatorPrefab = lockedGenerator;
                        prefabMapping[genObject] = lockedGenerator;
                        newSlot.open = true;
                    }

                    managerMapping[genObject] = null;
                    newSlot.managerPrefab = null;
                    slotList.Add(newSlot);
                    spawnedGenerators.Add(genObject);
                    Debug.Log($"Generator added. Current count: {spawnedGenerators.Count}");

                }
            }
        }

        
        public GameObject GetLockedGenerator()
        {
            /*Debug.Log(spawnedGenerators.Count + " in spawnGen list!");
            foreach (GameObject genObject in spawnedGenerators)
            {
                if (genObject == null)
                {
                    Debug.LogError("Null genObject!");
                }

                if (!prefabMapping.ContainsKey(genObject))
                {
                    Debug.LogError("GEN NOT IN LIST!");
                }
                
                if (prefabMapping[genObject] == lockedGenerator)
                {
                    Debug.Log("Found lockedGen");
                    return genObject;
                }
            }

            return null;*/
            Debug.Log($"Total spawnedGenerators: {spawnedGenerators.Count}");
            Debug.Log($"Total prefabMapping entries: {prefabMapping.Count}");

            foreach (Slot slot in slotList)
            {
                GameObject genObject = FindGenerator(slot.slotId);
                if (genObject == null)
                {
                    Debug.LogError("Null genObject found!");
                    continue;
                }

                if (!prefabMapping.ContainsKey(genObject))
                {
                    Debug.LogError($"GenObject not in prefabMapping: {genObject.name}");
                    continue;
                }

                Debug.Log($"Checking genObject: {genObject.name}, Prefab: {prefabMapping[genObject]?.name}");
        
                if (prefabMapping[genObject] == lockedGenerator)
                {
                    Debug.Log($"Found locked generator: {genObject.name}");
                    return genObject;
                }
            }

            Debug.LogError("No locked generator found!");
            return null;
        }

        public GameObject GetOpenManagerGenerator()
        {
            //TODO: test if updates spawnGenList proeprly

            foreach (Slot slot in slotList)
            {
                GameObject genObject = FindGenerator(slot.slotId);
                if (genObject == null)
                {
                    Debug.LogError("Null genObject found!");
                    continue;
                }

                if (!managerMapping.ContainsKey(genObject))
                {
                    Debug.LogError($"GenObject not in managerMapping: {genObject.name}");
                    continue;
                }

                if (!prefabMapping.ContainsKey(genObject))
                {
                    Debug.LogError($"GenObject not in managerMapping: {genObject.name}");
                    continue;
                }

                if (prefabMapping[genObject] == lockedGenerator)
                {
                    continue;
                }

                Debug.Log($"Checking genObject: {genObject.name}, Prefab: {managerMapping[genObject]?.name}");
        
                if (managerMapping[genObject] == null)
                {
                    Debug.Log($"Found open gen: {genObject.name}");
                    return genObject;
                }
            }

            Debug.LogError("No open pos found");
            return null;
        }

        public void SellGenerator(int slotId)
        {
            Slot slot = slotList[slotId];
            GameObject genObject = FindGenerator(slotId);
            
            if (slot == null || genObject == null)
            {
                Debug.LogError("ERROR: COULDNT FIND SPECIFIED SLOT OR UNIT: " + slotId);
                return;
            }
            
            spawnedGenerators.Remove(genObject);
            prefabMapping.Remove(genObject);
            managerMapping.Remove(genObject);
            
            ResetSlot(slot);

            GameObject lockedGen = Instantiate(lockedGenerator, slotGrid.transform);
            lockedGen.transform.SetSiblingIndex(slotId+1);
            lockedGen.GetComponentInChildren<Generator>().slotId = slotId;
            
            spawnedGenerators.Add(lockedGen);
            prefabMapping[lockedGen] = lockedGenerator;
            managerMapping[lockedGen] = null;
            

        }

        public void AddGenerator(GameObject genPrefab, int slotId)
        {
            Slot slot = slotList[slotId];

            if (slot == null || !slot.open)
            {
                Debug.LogError("Slot " + slotId + " is not empty or null!");
                return;
            }
            
            GameObject currentGen = FindGenerator(slotId);

            if (currentGen == null)
            {
                Debug.LogError("Generator (locked) is empty or null!");
                return;
            }
            
            //Destroy lockedGenerator prefab
            prefabMapping.Remove(currentGen);
            managerMapping.Remove(currentGen);
            spawnedGenerators.Remove(currentGen);
            Destroy(currentGen);
            
            //Instantiate new gen to take its place
            GameObject genObject = Instantiate(genPrefab, slotGrid.transform);
            genObject.transform.SetSiblingIndex(slotId);
            genObject.GetComponentInChildren<Generator>().slotId = slotId;
            
            prefabMapping[genObject] = genPrefab;
            managerMapping[genObject] = null;
            spawnedGenerators.Add(genObject);

            //Set slot data
            slot.generatorPrefab = genPrefab;
            slot.managerPrefab = null;
            slot.open = false;

        }

        public void AddManager(GameObject managerPrefab, int slotId)
        {
            Slot slot = slotList[slotId];

            if (slot == null || slot.open)
            {
                Debug.LogError("Slot " + slotId + " is not empty or null!");
                return;
            }
            
            GameObject genObject = FindGenerator(slotId);
            Generator gen = genObject.GetComponentInChildren<Generator>();

            if (gen == null || genObject == null)
            {
                Debug.LogError("Generator is empty or null!");
                return;
            }
            
            //Destroy button and get position
            Vector3 buttonPosition = gen.GetButtonPosition();

            //TODO: add parent in every instantiate
            GameObject managerObject = Instantiate(managerPrefab, buttonPosition, Quaternion.identity, genObject.transform);
            Manager manager = managerObject.GetComponent<Manager>();
            
            slot.managerPrefab = managerPrefab;
            gen.manager = managerPrefab;
            manager.SetGenerator(gen);
            managerMapping[genObject] = managerPrefab;
            
        }
        
        public void Save(ref GameManagerData data)
        {
            
            //data.playerId = PlayerSystem.Singleton.GetCurrentPlayer();
            
            data.slots = slotList.ToArray();
        }
        
        public void Load(GameManagerData data)
        {
            foreach (GameObject gen in spawnedGenerators)
            {
                Destroy(gen);
            }
            
            spawnedGenerators.Clear();
            prefabMapping.Clear();
            managerMapping.Clear();
            slotList.Clear();

            for (int i = 0; i < GENS_PER_COL * NUM_COL; i++)
            {
                Slot savedSlot = data.slots[i];
                
                GameObject genObject = Instantiate(savedSlot.generatorPrefab, slotGrid.transform);
                prefabMapping[genObject] = savedSlot.generatorPrefab;
                Generator gen = genObject.GetComponentInChildren<Generator>();
                gen.slotId = savedSlot.slotId;
                
                if (savedSlot.managerPrefab != null)
                {
                    Vector3 managerPos = gen.GetButtonPosition();
                    GameObject managerObject = Instantiate(savedSlot.managerPrefab, managerPos, Quaternion.identity, genObject.transform);
                    gen.manager = managerObject;
                    managerObject.GetComponent<Manager>().SetGenerator(gen);
                    managerMapping[genObject] = savedSlot.managerPrefab;
                }
                else
                {
                    managerMapping[genObject] = null;
                }

                Slot newSlot = new Slot()
                {
                    slotId = savedSlot.slotId,
                    open = savedSlot.open,
                    generatorPrefab = savedSlot.generatorPrefab,
                    managerPrefab = savedSlot.managerPrefab
                };
                
                spawnedGenerators.Add(genObject);
                slotList.Add(newSlot);

            }

            
            foreach (Slot savedSlot in data.slots)
            {
                Slot slot = new Slot()
                {
                    slotId = savedSlot.slotId,
                    open = savedSlot.open,
                    generatorPrefab = savedSlot.generatorPrefab,
                    managerPrefab = savedSlot.managerPrefab
                };
            }
            slotList = data.slots.ToList();

            //TODO: TEST CODE
            /*foreach (Slot slot in data.slots)
            {
                slotList.Add(slot);
                Slot newSlot = new Slot()
                {
                    data
                };
            }*/
            
        }

        public void ResetSlot(Slot slot)
        {
            slot.generatorPrefab = lockedGenerator;
            slot.managerPrefab = null;
            slot.open = true;
        }

        public GameObject FindGenerator(int slotId)
        {
            foreach (GameObject go in spawnedGenerators)
            {
                Generator gen = go.GetComponentInChildren<Generator>();
                if (slotId == gen.slotId)
                {
                    return go;
                }
            }

            return null;
        }
       
    }
