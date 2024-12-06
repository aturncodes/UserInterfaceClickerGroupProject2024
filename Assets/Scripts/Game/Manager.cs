using UnityEngine;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{

    //[SerializeField] private GameObject managerPrefab;
    [SerializeField] private Generator generator;
    [SerializeField] public int marketPrice { get; }

    public void SetGenerator(Generator generator)
    {
        this.generator = generator;
        generator.ManagerActivate();
    }

    public Generator GetGenerator()
    {
        return generator;
    }
}
