using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    [Range(0, 100)]
    public int count;
    public GameObject model;

    [HideInInspector]
    public int initialCount = 0, k = 0;

    [HideInInspector]
    public bool posFoldout, scaleFoldout, rotFoldout, stepFoldout, radiusFoldout;

    public PositionSetting positionSetting;
    public ScaleSetting scaleSetting;
    public RotationSetting rotationSetting;
    public StepRotationSetting stepRotationSetting;
    public RadiusSetting radiusSetting;

    public SetTransform setTransform;

    [HideInInspector]
    public int typeInd;

    [HideInInspector]
    public List<string> clonerType = new List<string>(new string[] { "Linear", "Radial" });
    public void GenerateModel()
    {
        InstanceModel();
        if(typeInd == 0)
        {
            OnPositionUpdate();
            OnScaleUpdate();
            OnRotationUpdate();
            OnStepRotationUpdate();
        }
        else if(typeInd == 1)
            OnRadiusUpdate();
    }

    public void positionChanged()
    {
        InstanceModel();
        OnPositionUpdate();
    }

    public void scaleChanged()
    {
        InstanceModel();
        OnScaleUpdate();
    }

    public void rotationChanged()
    {
        InstanceModel();
        OnRotationUpdate();
    }

    public void stepRotChanged()
    {
        InstanceModel();
        OnStepRotationUpdate();
    }
    public void radiusChanged()
    {
        InstanceModel();
        OnRadiusUpdate();
    }
    public void InstanceModel()
    {
        setTransform = new SetTransform(positionSetting, scaleSetting, rotationSetting, stepRotationSetting, radiusSetting);

        initialCount = count;
        for (int i = k; i < count; i++)
        {
            GameObject newClone = Instantiate(model);
            newClone.transform.parent = this.transform;
            k++;
        }

    }

    public void DeleteModel()
    {
        for (int i = transform.childCount - 1; i >= count; i--)
        {
            Transform Children = gameObject.transform.GetChild(i);
            DestroyImmediate(Children.gameObject);
        }
        k = count;
        initialCount = count;
    }
    public void OnPositionUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform Children = gameObject.transform.GetChild(i);
            setTransform.ShiftPosition(Children, i);
        }
    }

    public void OnScaleUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform Children = gameObject.transform.GetChild(i);
            setTransform.SetScale(Children, i);
        }
    }

    public void OnRotationUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform Children = gameObject.transform.GetChild(i);
            setTransform.SetRotation(Children, i);
        }
    }

    public void OnStepRotationUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform Children = gameObject.transform.GetChild(i);
            setTransform.SetStepRotation(Children, i, stepRotationSetting.StepRotation);
        }
    }

    public void OnRadiusUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform Children = gameObject.transform.GetChild(i);
            setTransform.setRadius(Children, i, count, radiusSetting.Radius);
        }
    }

}
