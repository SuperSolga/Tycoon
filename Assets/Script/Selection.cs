using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;
    public Transform highlight;
    public Transform selection;
    private RaycastHit raycastHit;

    private Machine machine;

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            try 
            {
                highlight.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            }
            catch (UnityException)
            {
                highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            }
            highlight = null;
            //Debug.Log("materiel récup");
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;

            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                try
                {
                    if (highlight.GetChild(0).GetComponent<MeshRenderer>().material != highlightMaterial)
                    {
                        originalMaterialHighlight = highlight.GetChild(0).GetComponent<MeshRenderer>().material;
                        highlight.GetChild(0).GetComponent<MeshRenderer>().material = highlightMaterial;
                    }
                }
                catch(UnityException)
                {
                    if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                    {
                        originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                        highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                    }
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (highlight)
            {
                if (selection != null)
                {
                    try
                    {
                        selection.GetChild(0).GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                    catch(UnityException)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                }
                selection = raycastHit.transform;
                if (selection != null)
                {
                    machine = selection.gameObject.transform.parent.parent.GetComponent<Machine>();
                    Debug.Log(machine);
                    machine.upgrade.enabled = true;
                    try
                    {
                        machine.upgradeMenu.GetSelected(machine.machineIndex, machine.machine[machine.machineLvl].timePerCoffee, machine.machine[machine.machineLvl].numberCoffee, machine.machine[machine.machineLvl + 1].upgradePrice);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        machine.upgradeMenu.GetSelected(machine.machineIndex, machine.machine[machine.machineLvl].timePerCoffee, machine.machine[machine.machineLvl].numberCoffee, 0);
                    }
                    
                    Debug.Log(machine.upgrade.enabled);
                }
                try
                {
                    if (selection.GetChild(0).GetComponent<MeshRenderer>().material != selectionMaterial)
                    {
                        originalMaterialSelection = originalMaterialHighlight;
                        selection.GetChild(0).GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                }
                catch (UnityException)
                {
                    if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                    {
                        originalMaterialSelection = originalMaterialHighlight;
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                }
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    try
                    {
                        selection.GetChild(0).GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                    catch (UnityException)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                    selection = null;
                }
            }
            if (selection == null)
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("UpgradeUI"))
                {
                    obj.GetComponent<Canvas>().enabled = false;
                }
            }
        }
    }
}
