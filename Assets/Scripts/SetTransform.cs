using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform
{
    public PositionSetting PositionSetting;
    public ScaleSetting scaleSetting;
    public RotationSetting rotationSetting;
    public StepRotationSetting stepRotationSetting;
    public RadiusSetting radiusSetting;

    public SetTransform(PositionSetting PositionSetting, ScaleSetting scaleSetting, RotationSetting rotationSetting, StepRotationSetting stepRotationSetting, RadiusSetting radiusSetting)
    {
        this.PositionSetting = PositionSetting;
        this.scaleSetting = scaleSetting;
        this.rotationSetting = rotationSetting;
        this.stepRotationSetting = stepRotationSetting;
        this.radiusSetting = radiusSetting;
    }

    public void ShiftPosition(Transform obj, int count)
    {
        obj.position = new Vector3(PositionSetting.Position.x * count, PositionSetting.Position.y * count, PositionSetting.Position.z * count);
    }

    public void SetScale(Transform obj, int count)
    {
        obj.localScale = new Vector3(1 + count * (scaleSetting.Scale.x - 1), 1 + count * (scaleSetting.Scale.y - 1), 1 + count * (scaleSetting.Scale.z - 1));
    }

    public void SetRotation(Transform obj, int count)
    {
        obj.localRotation = Quaternion.Euler(new Vector3(count * rotationSetting.Rotation.x, count * rotationSetting.Rotation.y, count * rotationSetting.Rotation.z));
    }

    public void SetStepRotation(Transform obj, int count, Vector3 stepRot)
    {
        if(stepRotationSetting.StepRotation == new Vector3(0, 0, 0))
        {
            ShiftPosition(obj, count);
        }
        obj.RotateAround(new Vector3(0f, 0f, 0f), Vector3.right, stepRotationSetting.StepRotation.x * count);
        obj.RotateAround(new Vector3(0f, 0f, 0f), Vector3.up, stepRotationSetting.StepRotation.y * count);
        obj.RotateAround(new Vector3(0f, 0f, 0f), Vector3.forward, stepRotationSetting.StepRotation.z * count);
    }

    public void setRadius(Transform obj, int count, int items, float r)
    {
        var x = r * Mathf.Cos(2 * Mathf.PI * count / items);
        var y = r * Mathf.Sin(2 * Mathf.PI * count / items);

        obj.position = new Vector3(x, y, obj.position.z);
    }

}
