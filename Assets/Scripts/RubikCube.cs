using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikCube : MonoBehaviour {

    GameObject cubePrefab;
    public int size = 3;
    public float offset = 1f;
    GameObject selectedCubes;
    public enum Axis { X, Y, Z};
    GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        selectedCubes = new GameObject("SelectedCubes");
        selectedCubes.transform.parent = transform;

        cubePrefab = Resources.Load("Cube") as GameObject;
        Vector3 position = Vector3.zero;
        for (int x = 0; x < size; x++) {
            position.x = x * offset;
            for (int y = 0; y < size; y++) {
                position.y = y * offset;
                for (int z = 0; z < size; z++) {
                    position.z = z * offset;

                    GameObject cube = Instantiate(cubePrefab, transform);
                    cube.transform.position = position;
                }
            }
        }
    }

	public void Rotate() {
        while (selectedCubes.transform.childCount != 0)
            selectedCubes.transform.GetChild(0).transform.parent = transform;

        selectedCubes.transform.rotation = Quaternion.Euler(0, 0, 0);

        GameObject[] filteredCubes = FilterCubesBy((int)(gameManager.index * offset), gameManager.axis);
        Vector3 posSum = Vector3.zero;
        foreach (GameObject cube in filteredCubes)
            posSum += cube.transform.position;

        selectedCubes.transform.position = posSum / (size * size);
        foreach (GameObject cube in filteredCubes)
            cube.transform.parent = selectedCubes.transform;

        Vector3 axisVector;
        switch (gameManager.axis) {
            case Axis.X: axisVector = new Vector3(1, 0, 0); break;
            case Axis.Y: axisVector = new Vector3(0, 1, 0); break;
            default: axisVector = new Vector3(0, 0, 1); break;
        }

        Quaternion dest = Quaternion.Euler(selectedCubes.transform.rotation.eulerAngles + (int)gameManager.direction * axisVector * 90);
        selectedCubes.transform.rotation = Quaternion.Lerp(selectedCubes.transform.rotation, dest, Time.deltaTime * 1000f);
    }


    public void Rotate2() {
        while (selectedCubes.transform.childCount != 0)
            selectedCubes.transform.GetChild(0).transform.parent = transform;

        selectedCubes.transform.rotation = Quaternion.Euler(0, 0, 0);

        GameObject[] filteredCubes = FilterCubesBy((int)(gameManager.index * offset), gameManager.axis);
        Vector3 posSum = Vector3.zero;
        foreach (GameObject cube in filteredCubes)
            posSum += cube.transform.position;

        selectedCubes.transform.position = posSum / (size * size);
        foreach (GameObject cube in filteredCubes)
            cube.transform.parent = selectedCubes.transform;
 
        Vector3 axisVector;
        switch (gameManager.axis) {
            case Axis.X: axisVector = new Vector3(1, 0, 0); break;
            case Axis.Y: axisVector = new Vector3(0, 1, 0); break;
            default: axisVector = new Vector3(0, 0, 1); break;
        }

    }

    private GameObject[] FilterCubesBy(int index, Axis axis) {
        int pos = (int)Mathf.Clamp(index, 0, size * offset);
        GameObject[] ret = new GameObject[size * size];
        int childsAdded = 0;
        for(int i=1; i < gameObject.transform.childCount; i++) {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            print(i);
            print((int)child.transform.position.x);
            print(pos);
            print(childsAdded);
            switch (axis) {
                case Axis.X: if ((int)child.transform.position.x == pos){ ret[childsAdded] = child; childsAdded++; } break;
                case Axis.Y: if ((int)child.transform.position.y == pos){ ret[childsAdded] = child; childsAdded++; } break;
                default: if ((int)child.transform.position.z == pos){ ret[childsAdded] = child; childsAdded++; } break;
            }
        }
        return ret;
    }
}
