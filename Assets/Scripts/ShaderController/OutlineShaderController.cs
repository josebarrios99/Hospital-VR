using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineShaderController : MonoBehaviour
{
    [SerializeField] private GameObject Model;
    [SerializeField] private Material OutlineMaterial;
    [SerializeField] private GameObject CharacterName;

    private SkinnedMeshRenderer[] SkinedMeshesArray;
    private MeshRenderer MeshesArray;
    private void Start()
    {
        if (Model == null)
            Model = gameObject;
        
        if (Model.name == "Body")
            MeshesArray = Model.GetComponent<MeshRenderer>();
        else
            SkinedMeshesArray = Model.GetComponentsInChildren<SkinnedMeshRenderer>();
        
        if(CharacterName == null)
            CharacterName = Model.transform.Find("CharacterCanvas").gameObject;
        HideOutlines();
    }

    public void HideOutlines()
    {
        if (Model.name == "Body")
        {
            Material[] Materials = MeshesArray.materials;

            if (Materials.Length > 1)
            {
                MeshesArray.materials = new []{Materials[0]};
            }
        }
        else
            foreach (var Mesh in SkinedMeshesArray)
            {
                Material[] Materials = Mesh.materials;

                if (Materials.Length > 1)
                {
                    Mesh.materials = new []{Materials[0]};
                }
            }
        if(CharacterName)
            CharacterName.SetActive(false);
    }

    public void SetOutlines()
    {
        if (Model.name == "Body")
            {
                Material[] Materials = MeshesArray.materials;
                
                MeshesArray.materials = Materials.Concat(new []{OutlineMaterial}).ToArray();
            }
        else
            foreach (var Mesh in SkinedMeshesArray)
            {
                Material[] Materials = Mesh.materials;
                
                Mesh.materials = Materials.Concat(new []{OutlineMaterial}).ToArray();
            }
        if(CharacterName)
            CharacterName.SetActive(true);
    }
}
